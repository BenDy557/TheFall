using UnityEngine;
using System.Collections;

public class FakeGravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	


	}

    void FixedUpdate()
    {

        GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, -7.0f, 0.0f));

    }
}
