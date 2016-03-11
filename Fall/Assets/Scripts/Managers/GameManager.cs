﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState { SplashScreen, Lobby, Game };

public class GameManager : MonoBehaviour {

  
    public GameState m_GameStateStart;
    public GameState m_GameState;
    private GameState m_GameStatePrev;
    private bool m_Transitioning;
	public Transform[] m_LobySpawns;
	public ReadyDisplay[] m_ReadyDisplays;
    [SerializeField] private string[] m_Controllers;
    [SerializeField] private int m_ControllerAmount;

    private static int m_MaxPlayers = 4;

    //SplashScreen

    //Lobby
    //private List<GameObject> m_PlayersMenu = new List<GameObject>();
    private List<GameObject> m_LobbyPlayers = new List<GameObject>();
    private bool[] m_PlayersSet = new bool[m_MaxPlayers];
    private bool[] m_PlayersReady = new bool[m_MaxPlayers];

    //Game
    private List<GameObject> m_Players = new List<GameObject>();
    [SerializeField] private GameObject m_LevelGenerator;
    bool m_GameFinished;

	// Use this for initialization
	void Start ()
    {
        //Scene loading stuff
        DontDestroyOnLoad(gameObject);
        m_GameState = m_GameStateStart;//TODO//get current scene to set this
        
        
        //How many controllers are connected?
        m_Controllers = Input.GetJoystickNames();
        m_ControllerAmount = m_Controllers.GetLength(0);

        //Splash Screen

        //Lobby
        m_PlayersSet[0] = false;
        m_PlayersReady[0] = false;
        m_PlayersSet[1] = false;
        m_PlayersReady[1] = false;
        m_PlayersSet[2] = false;
        m_PlayersReady[2] = false;
        m_PlayersSet[3] = false;
        m_PlayersReady[3] = false;

        //Game
        m_GameFinished = false;
        //if player wants to join add to game list
        //if player joined, instantiate play area for player
        //wait until players ready up
        //when all players ready up, load in level with appropriate amount of players
		if (GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> () != null) {
			m_LobySpawns = GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_LobySpawns;
			m_ReadyDisplays = GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_ReadyDisplays;
		}
		//System.Array.Copy (GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_LobySpawns, m_LobySpawns, 4);
		//GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_LobySpawns.CopyTo (m_LobySpawns, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (TransitionState())
        {
            //Debug.Log("NOTTransitioning");
            m_GameStatePrev = m_GameState;

            switch (m_GameState)
            {
                //SPLASH/////////////////////////////////////////////////
                /////////////////////////////////////////////////SPLASH//
                case GameState.SplashScreen:

                    //listen for all input, if A/X button pressed, exit splash screen
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //Load Lobby scene
                        m_GameState = GameState.Lobby;
                        Application.LoadLevel("Lobby");
                    }



                    break;

                //LOBBY//////////////////////////////////////////////////
                //////////////////////////////////////////////////LOBBY//
                case GameState.Lobby:

                    bool tempAllPlayersReady = false;
                    if (m_PlayersSet[0] || m_PlayersSet[1] || m_PlayersSet[2] || m_PlayersSet[3])
                    {
                        for (int i = 0; i < m_MaxPlayers; i++)
                        {
                            if (m_PlayersReady[i] == m_PlayersSet[i])
                            {
                                tempAllPlayersReady = true;
                            }
                            else
                            {
                                tempAllPlayersReady = false;
                                i = m_MaxPlayers;
                            }
                        }
                    }

                    if (tempAllPlayersReady)
                    {
                        //load game with amout of players

                        m_GameState = GameState.Game;
                        Application.LoadLevel("MainScene");
                    }

                    //listen for all input to see if player wants to join game
                    if (Input.GetButtonDown("Player1Start"))//|| (Input.GetAxis("PlaystationPlayer1ButtonX") > 0))
                    {
                        //Log player as wanting to join game
                        if (m_PlayersSet[0])
                        {
                            m_PlayersReady[0] = true;
							m_ReadyDisplays[0].ToggleReady();
                            
                        }
                        else{
                        m_PlayersSet[0] = true;
				
						GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PlayerDefault"));
						
						tempGameObject.GetComponent<CharacterController>().m_PlayerNumber = (1);
						m_ReadyDisplays[0].ToggleJoined();
						
						tempGameObject.transform.position = m_LobySpawns[0].position;
					}
                    }

				if (Input.GetButtonDown("Player2Start")) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer2
                        if (m_PlayersSet[1])
                        {
                            m_PlayersReady[1] = true;
							m_ReadyDisplays[1].ToggleReady();
                        }
					else{
                        m_PlayersSet[1] = true;
						GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PlayerDefault"));
						m_ReadyDisplays[1].ToggleJoined();
						tempGameObject.GetComponent<CharacterController>().m_PlayerNumber = (2);
						
						
					tempGameObject.transform.position = m_LobySpawns[1].position;
			
					}
                    }

				if (Input.GetButtonDown("Player3Start")) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer3
                        if (m_PlayersSet[2])
                        {
                            m_PlayersReady[2] = true;
							m_ReadyDisplays[2].ToggleReady();
                        }
					else {
                        m_PlayersSet[2] = true;
						GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PlayerDefault"));
						m_ReadyDisplays[2].ToggleJoined();
						tempGameObject.GetComponent<CharacterController>().m_PlayerNumber = (3);
						
						
					tempGameObject.transform.position =m_LobySpawns[2].position;
					}
						
                    }

				if (Input.GetButtonDown("Player4Start")) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer4
                        if (m_PlayersSet[3])
                        {
                            m_PlayersReady[3] = true;
							m_ReadyDisplays[3].ToggleReady();
                        }
					else{
                        m_PlayersSet[3] = true;
						GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PlayerDefault"));
						m_ReadyDisplays[3].ToggleJoined();
						tempGameObject.GetComponent<CharacterController>().m_PlayerNumber = (4);
					tempGameObject.transform.position =m_LobySpawns[3].position;
					}
                    }

                    break;

                //GAME///////////////////////////////////////////////////
                ///////////////////////////////////////////////////GAME//
                case GameState.Game:

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //Debug.Log("Loading Game");
                        m_GameState = GameState.Lobby;
                        Application.LoadLevel("Lobby");
                    }

                    if (m_GameFinished)
                    {
                        //Debug.Log("Switching GameState");
                        m_GameState = GameState.Lobby;
                        Application.LoadLevel("Lobby");
						
                    }

                    break;
            }

        }

        


        if (m_GameStatePrev != m_GameState)
        {
            m_Transitioning = true;
        }

	}

    bool TransitionState()
    {
        if (m_Transitioning)
        {
            //Debug.Log("Transitioning");

            switch (m_GameState)
            {
                case GameState.SplashScreen:
                    //Debug.Log("GameState"+m_GameState);
                    break;

                case GameState.Lobby:
                    //Debug.Log("GameState" + m_GameState);
                    if (m_GameStatePrev == GameState.Game)
                    {
                        m_Players.Clear();
                        for (int i = 0; i < m_MaxPlayers; i++)
                        {
                            m_PlayersReady[i] = false;
							m_PlayersSet[i] = false;
                        }
						m_LobySpawns = GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_LobySpawns;
						m_ReadyDisplays = GameObject.FindGameObjectWithTag ("LobbyRecord").GetComponent<LobbyRecord> ().m_ReadyDisplays;
                        m_GameFinished = false;
                    }
                    break;

                case GameState.Game:
                    //Debug.Log("GameState" + m_GameState);
                    if (m_GameStatePrev == GameState.Lobby)
                    {
                        //Find references
                        m_LevelGenerator = GameObject.FindGameObjectWithTag("Level Generator");

                        if (m_LevelGenerator)
                        {
                            //Create Players
                            for (int i = 0; i < m_MaxPlayers; i++)
                            {
                                if (m_PlayersReady[i])
                                {
                                    GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PlayerDefault"));

                                    tempGameObject.GetComponent<CharacterController>().m_PlayerNumber = (i + 1);


                                    tempGameObject.transform.position = m_LevelGenerator.GetComponent<GenerateLevel>().m_StartPoint.transform.position;
                                    tempGameObject.transform.position = tempGameObject.transform.position + new Vector3(-7.0f + (5.0f * i), tempGameObject.transform.position.y -2.0f, tempGameObject.transform.position.z);
                                    m_Players.Add(tempGameObject);
                                }
                            }
                        }
                    }
                    break;
            }

            m_Transitioning = false;

        }

        return true;
    }


    public void IWon(string WinnerName)
    {
        //Debug.Log(WinnerName + " won! yyyeeeeaa");

        m_GameFinished = true;

    }
}
