using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float hDrag = 3;
	public float lDrag = 1;
	bool highDrag = false;

    

	private RespawnManager m_RespawnManager;
	// Use this for initialization
	void Start () {
		m_RespawnManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<RespawnManager> ();
       
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.D))
		   {
			Kill ();
		}
	
		if(Input.GetKeyDown(KeyCode.Escape))
		   {
			Application.Quit();
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
        Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/DeathEffect"));
        
		//DestroyObject (gameObject);
		//TODO fix where the player goes 29/01/2016 GlennCullen
		gameObject.transform.position = new Vector3 (0, -20, 0);
		m_RespawnManager.StartRespawnTimer (gameObject);


	}
}
