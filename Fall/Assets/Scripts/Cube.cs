using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	public float viewportTop = 6.0f;
	public float viewportBot = -6.0f;
	public float speedDown = -0.2f;
	// Use this for initialization
	void Start () {
		setTop ();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(new Vector3(0,speedDown*Time.deltaTime,0));

		if (transform.position.y < viewportBot) {
			setTop ();
		}
	}

	void setTop()
	{
		gameObject.transform.position = new Vector3 (Random.Range (viewportBot, viewportTop), viewportTop);
	}
}
