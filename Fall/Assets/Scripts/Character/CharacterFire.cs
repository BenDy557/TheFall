using UnityEngine;
using System.Collections;

public class CharacterFire : MonoBehaviour {

	private bool fired = false;
	private string m_CharacterName;
	public GameObject bullet;
    public GameObject destructoShot;
	public GameObject reticule;
	Vector3 offsetReticule;

    [HideInInspector] public AudioSource m_AudioSource;
    public AudioClip m_AudioClipFire;

    public bool empowered;

	// Use this for initialization
	void Awake()
    {
        offsetReticule = reticule.transform.position;
        offsetReticule.z = 0;
        
    }
    
    void Start () 
    {
        m_AudioSource = GetComponent<AudioSource>();
		m_CharacterName = gameObject.GetComponent<CharacterController>().name;
        Debug.Log("PlayerName CharacterFire" + name);
	}

    
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis(m_CharacterName+"Fire");

		offsetReticule = reticule.transform.position;
		offsetReticule.z = 0;
		if (h > 0.1 && fired == false) {
			fired = true;

            GameObject newBullet;
            if (empowered)
            {
                newBullet = (GameObject)Instantiate(destructoShot, transform.position, Quaternion.identity);
                newBullet.GetComponent<DestructoShot>().SetParentPlayer(gameObject);
                empowered = false;
            }
            else
            {
                newBullet = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.GetComponent<bullet>().SetParentPlayer(gameObject);
            }

			
			newBullet.transform.LookAt(offsetReticule);


            m_AudioSource.PlayOneShot(m_AudioClipFire);
			
		}
		if (h <= 0.1 && fired == true) {
			fired = false;
		}
	}
}
