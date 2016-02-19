using UnityEngine;
using System.Collections;

public class DeathEffect : MonoBehaviour {

    [HideInInspector]  public AudioSource m_AudioSource;
    public AudioClip m_AudioClipDeath;

	// Use this for initialization
	void Start () {

        m_AudioSource = GetComponent<AudioSource>();
        StartCoroutine(DeathEffectTimer());
	}
	
	// Update is called once per frame
	void Update () {

        



	}

    IEnumerator DeathEffectTimer()
    {
        m_AudioSource.PlayOneShot(m_AudioClipDeath);
        yield return new WaitForSeconds(m_AudioClipDeath.length);
        Destroy(gameObject);
    }
}

