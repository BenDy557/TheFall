using UnityEngine;
using System.Collections;

public class DestructoShot : MonoBehaviour {


	public float speed = 10.0f;
	public float range = 80.0f;
	public float pushForce = 3000.0f;
	private GameObject parentPlayer;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, range / speed);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.forward *speed* Time.deltaTime,Space.World);
		//Debug.Log (transform.forward);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ground" && other.gameObject.GetComponent<DestructoShotEffect>())
        {
            other.GetComponent<DestructoShotEffect>().Enable();
			Destroy(gameObject);
        }


		if (other.tag == "Player" && other.gameObject != parentPlayer)
        {
			Vector3 pushVector = other.gameObject.transform.position - transform.position;
			other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*pushForce);
			//Debug.DrawLine(transform.position,other.gameObject.transform.position,Color.red,10.0f);
			//Debug.Log("start:"+transform.forward+". End:"+ transform.position);
			Destroy(gameObject);
		}
	}

	public void SetParentPlayer(GameObject parent)
	{
		parentPlayer = parent;
	}
}
