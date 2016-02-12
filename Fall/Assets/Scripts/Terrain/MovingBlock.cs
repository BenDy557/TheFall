using UnityEngine;
using System.Collections;

public class MovingBlock : MonoBehaviour {

	public float blockspeed;
	public Transform startPos;
	public Transform endPos;	


	private float startTime;
	private bool goingorcoming;
	private float journeyLength;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = startPos.position;
		startTime = Time.time;
		goingorcoming = true;
		journeyLength = Vector3.Distance (startPos.position, endPos.position);
	}
	
	// Update is called once per frame
	void Update () {

		float distCovered = (Time.time - startTime) * blockspeed;
		float fracCovered = distCovered / journeyLength;

		if (goingorcoming) {
			transform.position = Vector3.Lerp (startPos.position, endPos.position, fracCovered);
			if (fracCovered >= 1) {
				startTime = Time.time;
				goingorcoming = false;
			}
		} 
		else {
			gameObject.transform.position = Vector3.Lerp (endPos.position, startPos.position, fracCovered);
			if (fracCovered >= 1) {
				startTime = Time.time;
				goingorcoming = true;
			}
		}                    
	}
}
