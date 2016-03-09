using UnityEngine;
using System.Collections;

public class DeathEffect : MonoBehaviour {

    [HideInInspector]  public AudioSource m_AudioSource;
    
    public AudioClip m_AudioClipDeath;
    public float m_Duration;
    private ParticleSystem m_Emitter;


	// Use this for initialization
	void Start () {

        m_AudioSource = GetComponent<AudioSource>();
        m_Emitter = GetComponent<ParticleSystem>();
        StartCoroutine(DeathEffectTimer());
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator DeathEffectTimer()
    {
        m_AudioSource.PlayOneShot(m_AudioClipDeath);

        m_Emitter.Emit(100);
        yield return new WaitForSeconds(m_Duration);
        Destroy(gameObject);
    }
}

