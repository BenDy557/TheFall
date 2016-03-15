using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float hDrag = 3;
	public float lDrag = 1;
	bool highDrag = false;
	public bool m_Immune = false;
    public bool m_IsAlive = true;

	private RespawnManager m_RespawnManager;
	private LobbySpawn m_LobbySpawn;
	// Use this for initialization
	void Start () {
		m_RespawnManager = GameObject.FindGameObjectWithTag ("RespawnManager").GetComponent<RespawnManager> ();
		if (m_RespawnManager == null) {
			m_LobbySpawn = GameObject.FindGameObjectWithTag ("RespawnManager").GetComponent<LobbySpawn> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.D))
		   {
			Kill ();
		}
	
		
	}

	void FixedUpdate()
	{
	
	}
	public void toggleDrag()
	{
		if (highDrag == false) {
			
			gameObject.GetComponent<Rigidbody> ().drag = hDrag;
			highDrag = true;
		} else {
			gameObject.GetComponent<Rigidbody> ().drag = lDrag;
			highDrag = false;
		}
	}

	public void lowerDrag()
	{

			gameObject.GetComponent<Rigidbody> ().drag = lDrag;
			highDrag = false;
	}

	public void increaseDrag()
	{
		
		gameObject.GetComponent<Rigidbody> ().drag = hDrag;
		highDrag = true;
	}

	public void Kill()
	{
        GameObject temp = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/DeathEffect"));
        temp.transform.position = gameObject.transform.position;
                
		//DestroyObject (gameObject);
		//TODO fix where the player goes 29/01/2016 GlennCullen
		gameObject.transform.position = new Vector3 (0, -20, 0);
        m_IsAlive = false;
		if (m_RespawnManager != null) {
			m_RespawnManager.StartRespawnTimer (gameObject);
		} else if (m_LobbySpawn != null) {
			m_LobbySpawn.StartRespawnTimer (gameObject);
		}

        

	}

	public void ActivateImmunity(float time)
	{
        m_IsAlive = true;
		StartCoroutine (BecomeImmune (time));
	}

	IEnumerator BecomeImmune(float time)
	{
		m_Immune = true;
		Color tempColor = GetComponent<MeshColour> ().m_CurrentMaterial.color;
		tempColor.a = 0.5f;
		tempColor = GetComponent<MeshColour> ().m_CurrentMaterial.color = tempColor;
		yield return new WaitForSeconds (time);
		tempColor.a = 1f;
		tempColor = GetComponent<MeshColour> ().m_CurrentMaterial.color = tempColor;
		m_Immune = false;
	}
}
