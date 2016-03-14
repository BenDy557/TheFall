using UnityEngine;
using System.Collections;

public class CharacterFire : MonoBehaviour {

	private bool fired = false;
	private string m_CharacterName;
    private string m_ControllerType;
	public GameObject bullet;
    public GameObject destructoShot;
	public GameObject reverseControlsShot;
	public GameObject reticule;
	Vector3 offsetReticule;

    [HideInInspector] public AudioSource m_AudioSource;
    public AudioClip m_AudioClipFire;

    public int empoweredType=0;

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
        m_ControllerType = gameObject.GetComponent<CharacterController>().m_ControllerType;
        //Debug.Log("PlayerName CharacterFire" + name);
	}

    
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis(m_ControllerType + m_CharacterName + "Fire");

		offsetReticule = reticule.transform.position;
		offsetReticule.z = 0;
		if (h > 0.1 && fired == false) {
			fired = true;

            GameObject newBullet;
            if (empoweredType==1)
            {
                newBullet = (GameObject)Instantiate(destructoShot, transform.position, Quaternion.identity);
                newBullet.GetComponent<DestructoShot>().SetParentPlayer(gameObject);
                empoweredType = 0;
            }
            else if (empoweredType==2)
            {
                newBullet = (GameObject)Instantiate(reverseControlsShot, transform.position, Quaternion.identity);
                newBullet.GetComponent<ReverseControlsShot>().SetParentPlayer(gameObject);
				empoweredType = 0;
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
