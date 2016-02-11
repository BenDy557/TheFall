using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timer : MonoBehaviour {
	

	private float timeInLead;
	private GameObject[] players;
	private bool inLead;
	private string timertext;



	// Use this for initialization
	void Start () {
		timeInLead = 0;
		timertext = "";

	}
	
	// Update is called once per frame
	void Update () {



		players = GameObject.FindGameObjectsWithTag ("Player");
		if (players.Length == 0) {
			Debug.Log ("no players found");
		}
		foreach (GameObject player in players) {
			if (player.transform.position.z > transform.position.z) {
				inLead = false;
			} else {
				inLead = true;
			}
		}
		if (inLead) {
			timeInLead += Time.deltaTime;
			timertext = timeInLead.ToString ();
		} 
		else {
			timertext = "";
		}
		GetComponentInChildren<TextMesh> ().text = timertext;
		}
		
	}
	


