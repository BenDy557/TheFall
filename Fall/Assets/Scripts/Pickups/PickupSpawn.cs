using UnityEngine;
using System.Collections;

public class PickupSpawn : MonoBehaviour {

    public GameObject[] m_Pickup;
    public float m_Probability;
	public bool m_HasSpawned = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HasSpawned()
	{
		m_HasSpawned = true;
	}
}
