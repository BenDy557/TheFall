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
		if (timer > m_BlinkTimer) {
			if(m_Message.enabled)
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
}
