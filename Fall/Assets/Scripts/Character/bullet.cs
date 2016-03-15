using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public float speed = 10.0f;
	public float range = 20.0f;
	public float pushForce = 500.0f;
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
		if (other.tag == "Ground"&& other.gameObject.GetComponent<Rotate>()) {
			float dir = gameObject.transform.position.x-other.transform.position.x ;
			other.GetComponent<Rotate>().MakeRotate(dir);
			Destroy(gameObject);
		}
		if (other.tag == "Player" && other.gameObject != parentPlayer) {
			if(!other.GetComponent<Player>().m_Immune)
			{
				Vector3 pushVector = other.gameObject.transform.position - transform.position;
				other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*pushForce);
				//Debug.DrawLine(transform.position,other.gameObject.transform.position,Color.red,10.0f);
				//Debug.Log("start:"+transform.forward+". End:"+ transform.position);
				Destroy(gameObject);
			}
		}
		if (other.tag == "Pendulum") {
			if (other.gameObject.GetComponentInParent<Rigidbody>().useGravity==false){
				other.gameObject.GetComponentInParent<Rigidbody>().useGravity = true;
				other.gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
				if(transform.forward.y>0){
					other.gameObject.GetComponentInParent<Rigidbody>().AddForce (new Vector3(0,1000,0));}
				Destroy (gameObject);
			}
			else{
				other.gameObject.GetComponentInParent<Rigidbody>().AddForce (transform.forward*pushForce);
				Destroy (gameObject);
			}

		}
		if (other.tag == "Bucket") {
			float dir = gameObject.transform.position.x - other.transform.position.x;
			other.gameObject.GetComponentInParent<Rotate> ().MakeRotate (dir);
			Destroy (gameObject);
		}
	}

	public void SetParentPlayer(GameObject parent)
	{
		parentPlayer = parent;
	}
}
