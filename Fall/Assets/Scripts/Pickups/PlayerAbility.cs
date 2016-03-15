using UnityEngine;
using System.Collections;

public class PlayerAbility : MonoBehaviour {

    public PickupType m_Type;

    private string m_PlayerNumber;
    private string m_ControllerType;
	private GameObject[] players;
	private GameObject m_UIManager;
	int m_ID;
	public float m_PulseRange = 5;
	[SerializeField] private float m_DoubleJumpDuration = 5.0f;
	public ParticleSystem m_PulseEmmiter;
    public float m_SwapChargeUpTime = 2.0f;
    public float m_SwapTimer;
    private bool m_CurrentlySwapping;
    public AnimationCurve m_SwapXFactor;
    public AnimationCurve m_SwapYFactor;
    private GameObject m_CurrentPlayer;
    private GameObject m_SwapPlayer;
    public GameObject m_SwapParticles;
    public GameObject m_SwapParticlesEnd;
    private GameObject m_PlayerOneParticles;
    private GameObject m_PlayerTwoParticles;

	// Use this for initialization
	void Start () {
        m_SwapTimer = m_SwapChargeUpTime;

		m_UIManager = GameObject.FindGameObjectWithTag ("UIManager");
        m_Type = PickupType.empty;
        m_PlayerNumber = gameObject.GetComponent<CharacterController>().name;
		m_ID = gameObject.GetComponent<CharacterController>().m_PlayerNumber;
        m_ControllerType = gameObject.GetComponent<CharacterController>().m_ControllerType;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetButtonDown(m_ControllerType + m_PlayerNumber + "Ability"))
        {
            ActivateAbility();
        }

        if (m_CurrentlySwapping)
        {
            float tempX = m_CurrentPlayer.transform.position.x - m_SwapPlayer.transform.position.x;
            float tempY = m_CurrentPlayer.transform.position.y - m_SwapPlayer.transform.position.y;

            float tempPlayerOneFactorX = m_SwapXFactor.Evaluate(((float)m_SwapTimer / (float)m_SwapChargeUpTime));
            float tempPlayerOneFactorY = m_SwapYFactor.Evaluate(((float)m_SwapTimer / (float)m_SwapChargeUpTime));

            m_PlayerOneParticles.transform.position = new Vector3((tempPlayerOneFactorX * -tempX) + m_CurrentPlayer.transform.position.x
                                                                 , (tempPlayerOneFactorY * -tempY) + m_CurrentPlayer.transform.position.y);

            m_PlayerTwoParticles.transform.position = new Vector3((tempPlayerOneFactorX * tempX) + m_SwapPlayer.transform.position.x
                                                                 , (tempPlayerOneFactorY * tempY) + m_SwapPlayer.transform.position.y);

            m_SwapTimer -= Time.deltaTime;
        }
	}

    void ActivateAbility()
    {
        switch (m_Type)
        {
            case PickupType.empty:
                
                break;
            case PickupType.leaderSwap:
                if (SetSwapPlayers())
                {
                    m_SwapTimer = m_SwapChargeUpTime;
                    m_PlayerOneParticles = (GameObject)Instantiate(m_SwapParticles, m_CurrentPlayer.transform.position, m_CurrentPlayer.transform.rotation);
                    m_PlayerTwoParticles = (GameObject)Instantiate(m_SwapParticles, m_SwapPlayer.transform.position, m_SwapPlayer.transform.rotation);
                    StartCoroutine(SwapChargeUp());
                }
                
                
                break;
            case PickupType.phaseBlock:
                //Debug.Log("Phase");
                EmpowerBullet();
                break;
            case PickupType.rotate:
                RotateAll();
                break;
			case PickupType.doubleJump:
				EnableDoubleJump(m_DoubleJumpDuration);
				break;
			case PickupType.timeSlow:
				EnableTimeSlow(m_DoubleJumpDuration);
				break;
			case PickupType.reverseControls:
				EnableReverseControls(m_DoubleJumpDuration);
				break;
            default:
                Debug.Log("NOTHING");
                break;

        }
		ClearPickup ();
    }

    void RotateAll()
    {
        Rotate[] tempArray = GameObject.FindObjectsOfType<Rotate>();
        foreach (Rotate tempRotate in tempArray)
        {
            tempRotate.MakeRotate(1 * Mathf.Sign(Random.Range(-1, 1)));
        }
        m_Type = PickupType.empty;
    }


	void EnableDoubleJump(float time)
	{
		gameObject.GetComponent<CharacterController>().EnableDoubleJump(time);
		m_Type = PickupType.empty;
	}

    void EmpowerBullet()
    {
        gameObject.GetComponent<CharacterFire>().empoweredType = 1;
        m_Type = PickupType.empty;
    }

	void EnableTimeSlow(float time)
	{
		gameObject.GetComponent<CharacterController>().EnableTimeSlow(time);
		m_Type = PickupType.empty;
	}


	void EnableReverseControls(float time)
	{
		//gameObject.GetComponent<CharacterFire> ().empoweredType = 2;

		///////////////DM: Below would just apply the debuff to all players other than the powerup user.(Tested and works)
		players = new GameObject[0];
		players = GameObject.FindGameObjectsWithTag("Player");
		m_PulseEmmiter.Play ();
		foreach (GameObject player in players)
		{
			if (player.GetComponent<CharacterController>().name!= m_PlayerNumber && Vector3.Distance(transform.position, player.transform.position)<m_PulseRange)
			{
				player.GetComponent<CharacterController>().EnableReverseControls(time);
			}
		}
		m_Type = PickupType.empty;
	}

	public void ChangePickup(PickupType type)
	{
		m_Type = type;
		m_UIManager.GetComponent<UIPlayerDisplay> ().ChangePower (m_ID,type);
	}

	public void ClearPickup()
	{
		m_UIManager.GetComponent<UIPlayerDisplay> ().ClearPower (m_ID);
	}

    bool SetSwapPlayers()
    {
        players = new GameObject[0];
        players = GameObject.FindGameObjectsWithTag("Player");


        m_CurrentPlayer = new GameObject();
        m_SwapPlayer = new GameObject();
        float tempHighest = 0.0f;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.position.y > tempHighest)
            {
                tempHighest = players[i].transform.position.y;
                m_SwapPlayer = players[i];
            }

            if (players[i].GetComponent<CharacterController>().name == m_PlayerNumber)
            {
                m_CurrentPlayer = players[i];
            }
        }

        if (m_CurrentPlayer == m_SwapPlayer)//Cant swap withself
        {
            return false;
        }
        else
        {
            m_Type = PickupType.empty;
            return true;
        }

    }

    void SwapPlayer()
    {

        Vector3 tempCurrentPlayerPosition = new Vector3(m_CurrentPlayer.transform.position.x,m_CurrentPlayer.transform.position.y,m_CurrentPlayer.transform.position.z);
        m_CurrentPlayer.transform.position = new Vector3(m_SwapPlayer.transform.position.x,m_SwapPlayer.transform.position.y,m_SwapPlayer.transform.position.z);
        m_SwapPlayer.transform.position = new Vector3(tempCurrentPlayerPosition.x, tempCurrentPlayerPosition.y, tempCurrentPlayerPosition.z);

        
        
    }


    IEnumerator SwapChargeUp()
    {
        m_CurrentlySwapping = true;
        yield return new WaitForSeconds(m_SwapChargeUpTime);
        m_CurrentlySwapping = false;
        Destroy(m_PlayerOneParticles);
        Destroy(m_PlayerTwoParticles);

        

        if (m_CurrentPlayer.GetComponent<Player>().m_IsAlive && m_SwapPlayer.GetComponent<Player>().m_IsAlive)
        {
            GameObject tempGameObject = (GameObject)Instantiate(m_SwapParticlesEnd, m_CurrentPlayer.transform.position, m_CurrentPlayer.transform.rotation);
            GameObject tempGameObjectTwo = (GameObject)Instantiate(m_SwapParticlesEnd, m_SwapPlayer.transform.position, m_SwapPlayer.transform.rotation);
            Destroy(tempGameObject, 2.0f);
            Destroy(tempGameObjectTwo, 2.0f); 
            
            SwapPlayer();


        }
    }
}
