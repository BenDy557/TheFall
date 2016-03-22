 using UnityEngine;
using System.Collections;

public class DestructoShot : MonoBehaviour {


	public float speed = 10.0f;
	public float range = 80.0f;
	public float pushForce = 3000.0f;
	private GameObject parentPlayer;
	public AudioClip m_AudioClipOnAwake;
	public AudioClip m_AudioClipOnDestoy;
	private AudioSource m_AudioClip;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, range / speed);
		m_AudioClip = GetComponent<AudioSource> ();
		m_AudioClip.PlayOneShot (m_AudioClipOnAwake);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.forward *speed* Time.deltaTime,Space.World);
		//Debug.Log (transform.forward);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Ground" && other.gameObject.GetComponent<DestructoShotEffect>())
        {
            other.GetComponent<DestructoShotEffect>().Enable();
			GameObject emmiter = new GameObject("Emmiter");
			emmiter.AddComponent<AudioSource>();
			Instantiate(emmiter,transform.position,Quaternion.identity);
			emmiter.GetComponent<AudioSource>().PlayOneShot(m_AudioClipOnDestoy);
			Destroy(emmiter,m_AudioClipOnDestoy.length);
			Destroy(gameObject);
        }


		if (other.tag == "Player" && other.gameObject != parentPlayer)
        {
			if (other.tag == "Player" && other.gameObject != parentPlayer) {
				Vector3 pushVector = other.gameObject.transform.position - transform.position;
				other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*pushForce);
				//Debug.DrawLine(transform.position,other.gameObject.transform.position,Color.red,10.0f);
				//Debug.Log("start:"+transform.forward+". End:"+ transform.position);
				Destroy(gameObject);
			}
		}
	}

	public void SetParentPlayer(GameObject parent)
	{
		parentPlayer = parent;
	}
}
