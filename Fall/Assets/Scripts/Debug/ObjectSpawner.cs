using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    public GameObject m_ObjectToSpawn;
    public float m_Delay;

    private bool m_SpawningSomething;

	// Use this for initialization
	void Start () {

        m_SpawningSomething = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!m_SpawningSomething)
        {
            StartCoroutine(SpawnSomething(m_Delay));
        }

	}

    IEnumerator SpawnSomething(float time)
    {
        m_SpawningSomething = true;
        yield return new WaitForSeconds(time);
        GameObject temp = (GameObject)Instantiate(m_ObjectToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        m_SpawningSomething = false;

    }
}
