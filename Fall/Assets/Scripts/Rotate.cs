using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Rotate : MonoBehaviour {

	// Use this for initialization
	public float degPerSec = 40;
	bool rotateBool = false;
	bool highDrag = false;
	List<GameObject> Children;
	void Start () {
		Children = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Rotate"))
		{
			makeRotate ();
		}
	}

	public void makeRotate()
	{
		if(rotateBool == false)
		{
			StartCoroutine(rotate(new Vector3(0,0,0)));
		}
	}
	IEnumerator rotate(Vector3 ImpulseDirection)
	{
		foreach (GameObject obj in Children) {
			obj.GetComponent<CharacterController>().Shunt(-1);
		}

		rotateBool = true;
		float rotSoFar = 0;
		while (rotSoFar < 90) {
			float rotDef = degPerSec * Time.deltaTime;
			if (rotSoFar+rotDef >90)
			{
				rotDef = 90 - rotSoFar;
			}
			transform.Rotate (0, 0, rotDef);
			rotSoFar += rotDef;
			yield return 0;
		}
		rotateBool = false;
	}

	public void addObject(GameObject obj)
	{
		Children.Add (obj);
	}

	public void removeObject(GameObject obj)
	{
		Children.Remove (obj);
	}

}
