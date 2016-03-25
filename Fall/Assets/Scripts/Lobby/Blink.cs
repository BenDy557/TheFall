using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	public SpriteRenderer m_Message;
	public float m_BlinkTimer;
	float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
        if (m_Message != null)
        {
            if (timer > m_BlinkTimer)
            {
                if (m_Message.enabled)
                {
                    m_Message.enabled = false;
                }
                else
                {
                    m_Message.enabled = true;
                }
                timer = 0;
            }
        }
        else
        {
            if (timer > m_BlinkTimer)
            {
                if (transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                timer = 0;
            }
        }
	}
}
