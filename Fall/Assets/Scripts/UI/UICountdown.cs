using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UICountdown : MonoBehaviour {

	public Text m_Text;
	public MoveCamera m_CameraScript;
	// Use this for initialization
	void Start () {
		m_CameraScript =Camera.main.GetComponent<MoveCamera>();
		m_CameraScript.enabled = false;
		StartCoroutine (CountDown ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator CountDown()
	{
		m_Text.text = "3...";
		yield return new WaitForSeconds(0.5f);
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = m_Text.color;
			c.a = f;
			m_Text.color = c;
			yield return new WaitForSeconds(0.05f);;
		}
		Color col = m_Text.color;
		col.a = 1f;
		m_Text.color = col;
		m_Text.text = "2...";

		yield return new WaitForSeconds(1);
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = m_Text.color;
			c.a = f;
			m_Text.color = c;
			yield return new WaitForSeconds(0.05f);;
		}
		col = m_Text.color;
		col.a = 1f;
		m_Text.color = col;
		m_Text.text = "1...";
		yield return new WaitForSeconds(1);
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = m_Text.color;
			c.a = f;
			m_Text.color = c;
			yield return new WaitForSeconds(0.05f);;
		}
		col = m_Text.color;
		col.a = 1f;
		m_Text.color = col;
		m_Text.text = "Go!";
		yield return new WaitForSeconds(1);
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = m_Text.color;
			c.a = f;
			m_Text.color = c;
			yield return new WaitForSeconds(0.05f);;
		}
		m_CameraScript.enabled = true;
		m_Text.enabled = false;
	}

	public void UIWin(Color PlayerColor, string name)
	{
		StartCoroutine (Winner (PlayerColor, name));
	}

	private IEnumerator Winner(Color PlayerColor, string name)
	{
		m_Text.enabled = true;
		m_Text.text = name + " wins!";
		m_Text.color = PlayerColor;
		yield return new WaitForSeconds(3.5f);
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().m_GameFinished = true;
		//Application.LoadLevel ("Lobby");
	}
}
