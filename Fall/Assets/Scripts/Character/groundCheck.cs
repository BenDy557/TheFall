﻿using UnityEngine;
using System.Collections;

public class groundCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.GetComponent<Rotate>())
		{
		other.gameObject.GetComponent<Rotate>().addObject(transform.parent.gameObject);
		}
	}
	void OnTriggerExit(Collider other) {

		if(other.gameObject.GetComponent<Rotate>())
		{
		other.gameObject.GetComponent<Rotate>().removeObject(transform.parent.gameObject);				}
		}
}
