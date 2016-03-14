using UnityEngine;
using System.Collections;

public class PlayerAbility : MonoBehaviour {

    public PickupType m_Type;

    private string m_PlayerNumber;
	private GameObject[] players;
	private GameObject m_UIManager;
	int m_ID;
	public float m_PulseRange = 5;
	[SerializeField] private float m_DoubleJumpDuration = 5.0f;
	public ParticleSystem m_PulseEmmiter;
    public float m_SwapChargeUpTime = 2.0f;

	// Use this for initialization
	void Start () {
		m_UIManager = GameObject.FindGameObjectWithTag ("UIManager");
        m_Type = PickupType.empty;
        m_PlayerNumber = gameObject.GetComponent<CharacterController>().name;
		m_ID = gameObject.GetComponent<CharacterController>().m_PlayerNumber;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown(m_PlayerNumber + "Ability"))
        {
            ActivateAbility();
        }

	}

    void ActivateAbility()
    {
        switch (m_Type)
        {
            case PickupType.empty:
                
                break;
            case PickupType.leaderSwap:
                StartCoroutine(SwapChargeUp());
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

    void SwapPlayer()
    {
        players = new GameObject[0];
        players = GameObject.FindGameObjectsWithTag("Player");

        GameObject tempCurrentPlayer;
        GameObject tempSwapPlayer;
        tempCurrentPlayer = new GameObject();
        tempSwapPlayer = new GameObject();
        float tempHighest = 0.0f;

        for(int i = 0;i<players.Length;i++)
        {
            if (players[i].transform.position.y > tempHighest)
            {
                tempHighest = players[i].transform.position.y;
                tempSwapPlayer = players[i];
            }

            if (players[i].GetComponent<CharacterController>().name == m_PlayerNumber)
            {
                tempCurrentPlayer = players[i];
            }
        }

        if (tempCurrentPlayer == tempSwapPlayer)
        {
            //highest is with power up
            //dont use it
        }
        else
        {
            Vector3 tempCurrentPlayerPosition = new Vector3(tempCurrentPlayer.transform.position.x,tempCurrentPlayer.transform.position.y,tempCurrentPlayer.transform.position.z);
            tempCurrentPlayer.transform.position = new Vector3(tempSwapPlayer.transform.position.x,tempSwapPlayer.transform.position.y,tempSwapPlayer.transform.position.z);
            tempSwapPlayer.transform.position = new Vector3(tempCurrentPlayerPosition.x, tempCurrentPlayerPosition.y, tempCurrentPlayerPosition.z);
        }

        m_Type = PickupType.empty;
        
    }


    IEnumerator SwapChargeUp()
    {
        
        yield return new WaitForSeconds(m_SwapChargeUpTime);
        SwapPlayer();
    }
}
