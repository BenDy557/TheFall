using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timer : MonoBehaviour {
	
	public float timeneeded;
	//private float timeInLead;
	private GameObject[] players;
	private bool inLead;
	private TextMesh timertext;
	GameObject manager;


	// Use this for initialization
	void Start () {
		//timeInLead = 0;
//		timertext = "";
		timertext = GetComponentInChildren<TextMesh> ();
		manager = GameObject.FindGameObjectWithTag ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {



		players = GameObject.FindGameObjectsWithTag ("Player");

		if (players.Length == 0) {
			Debug.Log ("no players found");
		}
			//foreach (GameObject player in players) {
			/*if (player.transform.position.y > transform.position.y) {
				inLead = false;
			} else {
				inLead = true;
			}*/

			inLead = true;
			foreach (GameObject player in players) {
				if (player.transform.position.y > transform.position.y) {
					inLead = false;
				}
			}


		if (inLead) {
			Color tempcolor = timertext.color;
			tempcolor.a = 1;
			timertext.color = tempcolor;
			//timeInLead += Time.deltaTime;
			timeneeded -=Time.deltaTime;

		} 
		else {
			Color tempcolor = timertext.color;
			tempcolor.a = 0.5f;
			timertext.color = tempcolor;
			//timertext = "";
		}
		timertext.text = timeneeded.ToString ("F2");
		if (timeneeded <= 0) {
			manager.GetComponent<WinManager>().IWon(transform.name);
		}
	}
		
}
	


