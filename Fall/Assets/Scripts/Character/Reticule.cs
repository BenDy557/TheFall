using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {
	public string name;
	// Update is called once per frame
	void Update ()
	{
		// We are going to read the input every frame
		Vector3 vNewInput = new Vector3(Input.GetAxis(name+"RightStickX"), Input.GetAxis(name+"RightStickY"), 0.0f);
		
		
		transform.rotation = Quaternion.Euler(0, 0, 0);
		float angle;
		if (vNewInput.sqrMagnitude < 0.1f)
		{
			angle = Mathf.Atan2(-Input.GetAxis(name + "LeftStickX"), Input.GetAxis(name + "LeftStickY")) * Mathf.Rad2Deg;
		}
		else
		{
			angle = Mathf.Atan2(Input.GetAxis(name + "RightStickX"), Input.GetAxis(name + "RightStickY")) * Mathf.Rad2Deg; 
		}
		
		// Apply the transform to the 
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
