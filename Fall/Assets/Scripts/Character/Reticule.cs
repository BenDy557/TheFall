using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {
	public string name;
    public string m_ControllerType;
	// Update is called once per frame
    void Start()
    {
        name =  transform.parent.gameObject.GetComponent<CharacterController>().name;
        m_ControllerType = transform.parent.gameObject.GetComponent<CharacterController>().m_ControllerType;
    }

	void Update ()
	{
		// We are going to read the input every frame
		Vector3 vNewInput = new Vector3(Input.GetAxis(m_ControllerType + name+"RightStickX"), Input.GetAxis(m_ControllerType + name+"RightStickY"), 0.0f);
		
		
		transform.rotation = Quaternion.Euler(0, 0, 0);
		float angle;
		if (vNewInput.sqrMagnitude < 0.1f)
		{
			angle = Mathf.Atan2(-Input.GetAxis(m_ControllerType + name + "LeftStickX"), Input.GetAxis(m_ControllerType + name + "LeftStickY")) * Mathf.Rad2Deg;
		}
		else
		{
			angle = Mathf.Atan2(Input.GetAxis(m_ControllerType + name + "RightStickX"), Input.GetAxis(m_ControllerType + name + "RightStickY")) * Mathf.Rad2Deg; 
		}
		
		// Apply the transform to the 
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
