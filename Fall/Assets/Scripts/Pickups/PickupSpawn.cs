using UnityEngine;
using System.Collections;

public class PickupSpawn : MonoBehaviour {

    public GameObject m_Pickup;
    public float m_Probability;

	// Use this for initialization
	void Start () {

        float tempProbability = Random.Range(0.0f, 1.0f);

        if (tempProbability < m_Probability)
        {
            Instantiate(m_Pickup, transform.position, new Quaternion());
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
