using UnityEngine;
using System.Collections;

public class CharacterFire : MonoBehaviour {

	private bool fired = false;
	public string m_CharacterName;
	public GameObject bullet;
	public GameObject reticule;
	Vector3 offsetReticule;
	// Use this for initialization
	void Start () {
		offsetReticule = reticule.transform.position;
		offsetReticule.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis(m_CharacterName+"Fire");

		offsetReticule = reticule.transform.position;
		offsetReticule.z = 0;
		if (h > 0.1 && fired == false) {
			fired = true;

			GameObject newBullet = (GameObject)Instantiate(bullet,transform.position,Quaternion.identity);
			newBullet.GetComponent<bullet>().SetParentPlayer(gameObject);
			newBullet.transform.LookAt(offsetReticule);
			Debug.Log(offsetReticule);
		}
		if (h <= 0.1 && fired == true) {
			fired = false;
		}
	}
}
