using UnityEngine;
using System.Collections;

public class PendulumHammer : MonoBehaviour {
	public float hammerForce;

	private Vector3 playerPos;
	private Vector3 pendulumVel;
	private Vector3 toPlayer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		pendulumVel = gameObject.GetComponent<Rigidbody> ().velocity;
		if (other.tag == "Player") {
			playerPos = other.transform.position;
			toPlayer = (playerPos - gameObject.transform.position);
			if (Vector3.Dot(pendulumVel, toPlayer)>0)
			{
				other.GetComponent<Rigidbody>().AddForce(pendulumVel*hammerForce);
			}

		}
	}

}
