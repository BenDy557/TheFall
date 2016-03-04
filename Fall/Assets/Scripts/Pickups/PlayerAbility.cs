﻿using UnityEngine;
using System.Collections;

public class PlayerAbility : MonoBehaviour {

    public PickupType m_Type;

    private string m_PlayerNumber;
	private GameObject[] players;

	[SerializeField] private float m_DoubleJumpDuration = 5.0f;

	// Use this for initialization
	void Start () {
        m_Type = PickupType.empty;
        m_PlayerNumber = gameObject.GetComponent<CharacterController>().name;
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
			case PickupType.reverseControls:
				EnableReverseControls(m_DoubleJumpDuration);
				break;
            default:
                Debug.Log("NOTHING");
                break;

        }
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
		gameObject.GetComponent<CharacterFire> ().empoweredType = 2;

		///////////////DM: Below would just apply the debuff to all players other than the powerup user.(Tested and works)
		/*players = new GameObject[0];
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player.GetComponent<CharacterController>().name!= m_PlayerNumber)
			{
				player.GetComponent<CharacterController>().EnableReverseControls(time);
			}
		}*/
		m_Type = PickupType.empty;
	}


}
