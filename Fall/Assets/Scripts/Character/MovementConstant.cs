using UnityEngine;
using System.Collections;

public class MovementConstant : MonoBehaviour {

    public Vector3 m_MovementAmount;//Per Second

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate( m_MovementAmount * Time.deltaTime,Space.World);

	}
}
