using UnityEngine;
using System.Collections;

public class WinManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IWon(string WinnerName)
	{
		Debug.Log (WinnerName + " won!");

		Application.LoadLevel("Lobby");
		
	}
}
