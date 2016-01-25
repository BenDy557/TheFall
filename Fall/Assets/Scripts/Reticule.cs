using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {
	public string name;
	// Update is called once per frame
	void Update ()
	{
		// We are going to read the input every frame
		Vector3 vNewInput = new Vector3(Input.GetAxis(name+"RightStickX"), Input.GetAxis(name+"RightStickY"), 0.0f);
		
		// Only do work if meaningful
		if(vNewInput.sqrMagnitude < 0.1f)
		{
			return;
		}
		
		// Apply the transform to the object  
		transform.rotation = Quaternion.Euler(0, 0, 0);
		var angle = Mathf.Atan2(Input.GetAxis(name+"RightStickX"), Input.GetAxis(name+"RightStickY")) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
