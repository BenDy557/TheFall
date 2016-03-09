using UnityEngine;
using System.Collections;

public class RespawnEffect : MonoBehaviour {



    [HideInInspector]  public AudioSource m_AudioSource;
    
    public AudioClip m_AudioClipDeath;
    public float m_Duration;

    public ParticleSystem m_ParticleSystemOne;
    public ParticleSystem m_ParticleSystemTwo;
    public ParticleSystem m_ParticleSystemThree;
    public RotateConstant m_Rotater;
    //private ParticleSystem m_Emitter;


	// Use this for initialization
	void Start () {

        m_AudioSource = GetComponent<AudioSource>();
        
        StartCoroutine(RespawnEffectTimer());
	}
	
	// Update is called once per frame
	void Update () {

        

	}

    IEnumerator RespawnEffectTimer()
    {
        m_AudioSource.PlayOneShot(m_AudioClipDeath);

        yield return new WaitForSeconds(m_Duration / 2);
        m_ParticleSystemOne.enableEmission = false;
        m_ParticleSystemTwo.enableEmission = false;
        m_Rotater.speed = 0;
        //m_Emitter.Emit(100);
        yield return new WaitForSeconds(m_Duration/2);
        Destroy(gameObject);
    }
}

