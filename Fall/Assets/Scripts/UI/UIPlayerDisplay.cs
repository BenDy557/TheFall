using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerDisplay : MonoBehaviour {

	public GameObject[] m_PlayerObjects;
	public Text[] m_UIOutput;
	public Image[] m_UIImage;
	public Sprite[] m_PowerUpSprites;
	public Sprite m_ClearSprite;
	// Use this for initialization
	void Awake()
	{
		m_PlayerObjects = new GameObject[4];
	//	m_UIOutput = new Text[4];
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		for(int cnt =0 ; cnt < 1; cnt++)
		{
			if(m_PlayerObjects[cnt]!=null)
			{
			m_UIOutput[cnt].text = m_PlayerObjects[cnt].GetComponent<timer>().timeneeded.ToString("F2");
			}
		}
	}

	public bool RegisterPlayer(int ID, GameObject PlayerObject)
	{
		if (ID > 4 || m_PlayerObjects [ID - 1] != null) {
			Debug.Log("Error, failed to register player. Either ID is incorrect or player is already registered");
			return false;
		}

		m_PlayerObjects [ID - 1] = PlayerObject;
		return true;
	}

	public void ChangePower(int ID, PickupType type)
	{
		m_UIImage [ID - 1].sprite = m_PowerUpSprites [(int)type];
	}

	public void ClearPower(int ID)
	{
		m_UIImage [ID - 1].sprite =  m_PowerUpSprites [0];
	}
}
