using UnityEngine;
using System.Collections;

public class CharacterFire : MonoBehaviour {

	private bool fired = false;
	public string m_CharacterName;
	public GameObject bullet;
    public GameObject destructoShot;
	public GameObject reticule;
	Vector3 offsetReticule;

    [HideInInspector] public AudioSource m_AudioSource;
    public AudioClip m_AudioClipFire;

    public bool empowered;

	// Use this for initialization
	void Start () {
		offsetReticule = reticule.transform.position;
		offsetReticule.z = 0;


        m_AudioSource = GetComponent<AudioSource>();
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
