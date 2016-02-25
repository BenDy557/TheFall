using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour {

    enum GameState { SplashScreen, Lobby, Game };
    private GameState m_GameState;
    private GameState m_GameStatePrev;
    private bool m_Transitioning;

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

	// Use this for initialization
	void Start ()
    {
        //Scene loading stuff
        DontDestroyOnLoad(gameObject);
        m_GameState = GameState.SplashScreen;//TODO//get current scene to set this
        
        
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
        //if player wants to join add to game list
        //if player joined, instantiate play area for player
        //wait until players ready up
        //when all players ready up, load in level with appropriate amount of players
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (TransitionState())
        {
            m_GameStatePrev = m_GameState;

            switch (m_GameState)
            {
                case GameState.SplashScreen:

                    //listen for all input, if A/X button pressed, exit splash screen
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        //Load Lobby scene
                        Debug.Log("Loading Lobby");
                        m_GameState = GameState.Lobby;
                        Application.LoadLevel("Lobby");
                    }



                    break;

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
                        Debug.Log("Loading Game");

                        m_GameState = GameState.Game;
                        Application.LoadLevel("GameManagerLevel");
                    }

                    Debug.Log("Players ready? " + tempAllPlayersReady);



                    //listen for all input to see if player wants to join game

                    if (Input.GetButtonDown("Player1Jump"))//|| (Input.GetAxis("PlaystationPlayer1ButtonX") > 0))
                    {
                        //Log player as wanting to join game
                        Debug.Log("button pressed");
                        if (m_PlayersSet[0])
                        {
                            m_PlayersReady[0] = true;
                            
                        }
                        
                        m_PlayersSet[0] = true;


                        //CreatePlayer1
                        /*
                        Debug.Log("CreatePlayer1");
                        GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Lobby/LobbyPlayground"));

                        Debug.Log("m_PlayersMenu.Count" + m_PlayersMenu.Count);
                        for (int i = 0; i < 4; i++)
                        {
                            if (m_PlayersMenu[i].GetComponent<PlayerLobbyMenu>().m_PlayerNumber == 1)
                            {
                                tempGameObject.transform.position = m_PlayersMenu[i].transform.GetChild(0).transform.position;

                                i = 4;
                            }
                        }
                        
                        

                        GameObject tempGameObjectPlayer = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Lobby/PlayerLobby"));
                        tempGameObjectPlayer.GetComponent<CharacterController>().name = "Player1";
                        GameObject tempGameObjectPlayerSpawn = GameObject.FindGameObjectWithTag("LobbyPlayerSpawn");
                        tempGameObjectPlayer.transform.position = tempGameObjectPlayerSpawn.transform.position;
                        Destroy(tempGameObjectPlayerSpawn);

                        m_PlayersLobby.Add(tempGameObjectPlayer);
                        m_PlayersSet[0] = true;
                        */
                    }

                    if (Input.GetButtonDown("Player2Jump")) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer2
                        Debug.Log("button pressed");
                        if (m_PlayersSet[1])
                        {
                            m_PlayersReady[1] = true;
                        }

                        m_PlayersSet[1] = true;
                    }

                    if (Input.GetButtonDown("Player3Jump")) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer3
                        Debug.Log("button pressed");
                        if (m_PlayersSet[2])
                        {
                            m_PlayersReady[2] = true;
                        }

                        m_PlayersSet[2] = true;
                    }

                    if (Input.GetButtonDown("Player4Jump")) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer4
                        Debug.Log("button pressed");
                        if (m_PlayersSet[3])
                        {
                            m_PlayersReady[3] = true;
                        }

                        m_PlayersSet[3] = true;
                    }

                    break;

                case GameState.Game:


                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Debug.Log("Loading Game");
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
            Debug.Log("banter");

            switch (m_GameState)
            {
                case GameState.SplashScreen:
                    break;

                case GameState.Lobby:
                    if (m_GameStatePrev == GameState.Game)
                    {
                        for (int i = 0; i < m_MaxPlayers; i++)
                        {
                            m_PlayersReady[i] = false;
                        }
                    }
                    break;

                case GameState.Game:
                    if (m_GameStatePrev == GameState.Lobby)
                    {
                        //Find references
                        m_LevelGenerator = GameObject.FindGameObjectWithTag("Level Generator");

                        if (m_LevelGenerator)
                        {
                            //Create Players
                            for (int i = 0; i < m_MaxPlayers; i++)
                            {
                                GameObject tempGameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/PlayerDefault"));

                                tempGameObject.GetComponent<CharacterController>().m_PlayerNumber = (i + 1);
                                
                                Debug.Log("Incrementnumber" + i);
                                Debug.Log("PlayerNumber" + tempGameObject.GetComponent<CharacterController>().m_PlayerNumber);
                                Debug.Log("PlayerName" + tempGameObject.GetComponent<CharacterController>().name);

                                tempGameObject.transform.position = m_LevelGenerator.GetComponent<GenerateLevel>().m_StartPoint.transform.position;
                                tempGameObject.transform.position = tempGameObject.transform.position + new Vector3(-10.0f + (5.0f * i), tempGameObject.transform.position.y, tempGameObject.transform.position.z);
                                m_Players.Add(tempGameObject);
                            }
                        }
                    }
                    break;
            }

            m_Transitioning = false;

        }

        return true;
    }
}
