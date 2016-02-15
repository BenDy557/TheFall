using UnityEngine;
using System.Collections;

public class CircularMovingBlock : MonoBehaviour {
	
	public float radspersecond;
	public Transform centre;
	public float radius;
	
	private float x, y;
	private float startTime;
	private Vector3 relPos;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		float distCovered = (Time.time - startTime) * radspersecond;
		float fracCovered = distCovered / (2*Mathf.PI);

		x = radius * Mathf.Cos (distCovered);
		y = radius * Mathf.Sin (distCovered);

		relPos = new Vector3 (x, y, 0.0f);

		gameObject.transform.position = centre.position + relPos;
		
		if (fracCovered >= 1) {
			startTime = Time.time;
		}
		
	}
}
