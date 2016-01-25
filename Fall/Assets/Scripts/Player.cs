using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float hDrag = 3;
	public float lDrag = 1;
	bool highDrag = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape))
		   {
			Application.Quit();
		}
	}

	void FixedUpdate()
	{
	
	}
	public void toggleDrag()
	{
		if (highDrag == false) {
			
			gameObject.GetComponent<Rigidbody> ().drag = hDrag;
			highDrag = true;
		} else {
			gameObject.GetComponent<Rigidbody> ().drag = lDrag;
			highDrag = false;
		}
	}

	public void lowerDrag()
	{

			gameObject.GetComponent<Rigidbody> ().drag = lDrag;
			highDrag = false;
	}

	public void increaseDrag()
	{
		
		gameObject.GetComponent<Rigidbody> ().drag = hDrag;
		highDrag = true;
	}

}
