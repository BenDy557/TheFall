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
			makeRotate (1*Mathf.Sign(Random.Range(-1,1)));
		}
	}

	public void makeRotate(float dir)
	{
		if(rotateBool == false)
		{
			StartCoroutine(rotate(dir));
		}
	}
	IEnumerator rotate(float dir)
	{
		foreach (GameObject obj in Children) {
			obj.GetComponent<CharacterController>().Shunt(dir);
		}

		rotateBool = true;
		float rotSoFar = 0;
		while (rotSoFar < 90) {
			float rotDef = degPerSec * Time.deltaTime;
			if (rotSoFar+rotDef >90)
			{
				rotDef = 90 - rotSoFar;
			}
			transform.Rotate (0, 0, Mathf.Sign(dir)*rotDef);
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
