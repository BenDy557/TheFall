using UnityEngine;
using System.Collections;

public class PlayerAbility : MonoBehaviour {

    public PickupType m_Type;

    private string m_PlayerNumber;
	private GameObject m_UIManager;
	int m_ID;

	[SerializeField] private float m_DoubleJumpDuration = 5.0f;

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
            case PickupType.invertControls:
                Debug.Log("Invert");
                break;
            case PickupType.leaderSwap:
                Debug.Log("Swap");
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
        gameObject.GetComponent<CharacterFire>().empowered = true;
        m_Type = PickupType.empty;
    }

	void EnableTimeSlow(float time)
	{
		gameObject.GetComponent<CharacterController>().EnableTimeSlow(time);
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


}
