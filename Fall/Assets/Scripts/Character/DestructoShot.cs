 using UnityEngine;
using System.Collections;

public class DestructoShot : MonoBehaviour 
{


	public float speed = 10.0f;
	public float range = 80.0f;
	public float pushForce = 3000.0f;
	private GameObject parentPlayer;
	// Use this for initialization
	void Start () 
    {
		Destroy (gameObject, range / speed);
	}
	
	// Update is called once per frame
	void Update () 
    {
		transform.Translate (transform.forward *speed* Time.deltaTime,Space.World);
		//Debug.Log (transform.forward);
	}

	void OnTriggerEnter(Collider other) 
    {
		if (other.tag == "Ground" && other.gameObject.GetComponent<DestructoShotEffect>())
        {
            other.GetComponent<DestructoShotEffect>().Enable();
            GameObject tempGameObject = (GameObject)Instantiate(Resources.Load("Prefabs/DestructoBlockEffect"));
            tempGameObject.transform.position = other.gameObject.transform.position;
            tempGameObject.transform.rotation = other.gameObject.transform.rotation;
            tempGameObject.transform.parent = other.transform;
            Destroy(tempGameObject, 3.0f);
			Destroy(gameObject,3.0f);
            StopBullet();
        }


		if (other.tag == "Player" && other.gameObject != parentPlayer)
        {
			if (other.tag == "Player" && other.gameObject != parentPlayer)
            {
				Vector3 pushVector = other.gameObject.transform.position - transform.position;
				other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*pushForce);
			}
		}
	}

	public void SetParentPlayer(GameObject parent)
	{
		parentPlayer = parent;
	}

    private void StopBullet()
    {
        speed = 0;
        transform.FindChild("projectileSprite").GetComponent<SpriteRenderer>().enabled = false;
    }
}
