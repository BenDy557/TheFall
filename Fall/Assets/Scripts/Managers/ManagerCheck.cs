using UnityEngine;
using System.Collections;

public class ManagerCheck : MonoBehaviour {

    public GameManager.GameState m_CurrentGameState;
	// Use this for initialization
	void Awake () {

        if (!FindObjectOfType<GameManager>())
        {
            GameObject tempGameManager = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/GameManager"));
            tempGameManager.GetComponent<GameManager>().m_GameStateStart = m_CurrentGameState;


        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
