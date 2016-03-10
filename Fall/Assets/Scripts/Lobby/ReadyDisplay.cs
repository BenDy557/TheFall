using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ReadyDisplay : MonoBehaviour {

	public SpriteRenderer m_Display;
	public SpriteRenderer m_Message;
	public Sprite[] m_Images;
	// Use this for initialization
	void Start () {
		m_Display.gameObject.active = false;
		m_Message.gameObject.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleJoined()
	{
		m_Display.gameObject.active = true;
		m_Display.sprite = m_Images [0];
		m_Message.gameObject.active = true;
	}

	public void ToggleReady()
	{
		m_Display.gameObject.active= true;
		m_Display.sprite = m_Images [1];
		m_Message.gameObject.active = false;;
	}
}
