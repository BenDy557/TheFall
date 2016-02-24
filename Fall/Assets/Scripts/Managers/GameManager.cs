using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour {

    enum GameState { SplashScreen, Lobby, Game };
    private GameState m_GameState;
    private GameState m_GameStatePrev;
    private bool m_Transitioning;
    
    //SplashScreen

    //Lobby
    private List<GameObject> m_PlayersMenu = new List<GameObject>();
    private List<GameObject> m_PlayersLobby = new List<GameObject>();
    private bool[] m_PlayersSet = new bool[4];

    //Game

    [SerializeField] private string[] controllers;
    [SerializeField] private int controllerAmount;

    private List<GameObject> players = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        //Scene loading stuff
        DontDestroyOnLoad(gameObject);
        m_GameState = GameState.Lobby;
        
        
        //How many controllers are connected?
        controllers = Input.GetJoystickNames();
        controllerAmount = controllers.GetLength(0);

        //Splash Screen

        //Lobby
        m_PlayersSet[0] = false;
        m_PlayersSet[1] = false;
        m_PlayersSet[2] = false;
        m_PlayersSet[3] = false;

        //Game
        
        

        
        


        //if player wants to join add to game list
        //if player joined, instantiate play area for player
        //wait until players ready up
        //when all players ready up, load in level with appropriate amount of players


	}
	
	// Update is called once per frame
	void Update () 
    {
        m_GameStatePrev = m_GameState;

        if (TransitionState())
        {
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


                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Debug.Log("Loading Game");

                        m_GameState = GameState.Game;
                        Application.LoadLevel(0);
                    }


                    //listen for all input to see if player wants to join game

                    if ((Input.GetAxis("Player1Jump") > 0) && (!m_PlayersSet[0]))//|| (Input.GetAxis("PlaystationPlayer1ButtonX") > 0))
                    {
                        //CreatePlayer1
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
                    }

                    if ((Input.GetAxis("Player2Jump") > 0)) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer2
                        Debug.Log("CreatePlayer2");
                    }

                    if ((Input.GetAxis("Player3Jump") > 0)) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer2
                        Debug.Log("CreatePlayer3");
                    }

                    if ((Input.GetAxis("Player4Jump") > 0)) //|| (Input.GetAxis("PlaystationPlayer2ButtonX") > 0))
                    {
                        //CreatePlayer2
                        Debug.Log("CreatePlayer4");
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

            switch (m_GameState)
            {
                case GameState.SplashScreen:

                    break;

                case GameState.Lobby:

                    //GetReferences in new scene

                    if (m_GameStatePrev == GameState.SplashScreen)
                    {
                        m_PlayersMenu.AddRange(GameObject.FindGameObjectsWithTag("LobbyPlayerMenu"));

                        if (m_PlayersMenu.Count == 0)
                        {
                            return false;
                        }


                    }


                    break;

                case GameState.Game:


                    break;
            }

            m_Transitioning = false;

        }

        return true;
    }
}
