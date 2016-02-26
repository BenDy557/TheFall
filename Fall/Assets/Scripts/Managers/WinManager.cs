using UnityEngine;
using System.Collections;

public class WinManager : MonoBehaviour {

    private GameManager m_GameManager;

	// Use this for initialization
	void Start () {

        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    public void IWon(string WinnerName)
    {
        Debug.Log(WinnerName + " won!");

        //Application.LoadLevel("Lobby");

    }
    */
}
