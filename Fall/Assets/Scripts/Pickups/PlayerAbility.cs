﻿using UnityEngine;
using System.Collections;

public class PlayerAbility : MonoBehaviour {

    public PickupType m_Type;

    private string m_PlayerNumber;


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
            case PickupType.invertControls:
                Debug.Log("Invert");
                break;
            case PickupType.leaderSwap:
                Debug.Log("Swap");
                break;
            case PickupType.phaseBlock:
                Debug.Log("Phase");
                break;
            case PickupType.rotate:
                RotateAll();
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
}