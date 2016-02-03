using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

   
    public PickupType m_Type;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

	void OnTriggerEnter(Collider collider)
	{
        if (collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //PowerUpPlayer
            //StartPickupAnimation
            collider.gameObject.GetComponent<PlayerAbility>().m_Type = m_Type;
        }
	}
}

public enum PickupType { empty, rotate, invertControls, leaderSwap, phaseBlock };
