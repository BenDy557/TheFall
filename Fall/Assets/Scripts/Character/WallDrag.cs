using UnityEngine;
using System.Collections;

public class WallDrag : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ground") {
			gameObject.transform.parent.gameObject.GetComponent<Player> ().IncreaseDrag ();
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "Ground") {
			gameObject.transform.parent.gameObject.GetComponent<Player> ().LowerDrag ();
		}
	}
}
