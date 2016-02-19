using UnityEngine;
using System.Collections;

public class DestructoShotEffect : MonoBehaviour {

    private Material m_DefaultMaterial;
    public Material m_PhaseMaterial;
    float m_Duration = 3.0f;
    bool m_Enabled = false;
	// Use this for initialization
	void Start () {

        m_DefaultMaterial = new Material(gameObject.GetComponent<MeshRenderer>().material);

	}
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<MeshRenderer>().material = m_DefaultMaterial;
        gameObject.tag = "Ground";

        if (m_Enabled)
        {
            //start animation
            //disable collider
            gameObject.GetComponent<Collider>().isTrigger = true; ;
            gameObject.tag = "PhasedOut";
            gameObject.GetComponent<MeshRenderer>().material = m_PhaseMaterial;
            //gameObject.GetComponent<MeshRenderer>().material.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0.0f);

        }
        
	}

    public void Enable()
    {
        StartCoroutine(WaitDuration());
    }

    IEnumerator WaitDuration()
    {
        m_Enabled = true;
        yield return new WaitForSeconds(m_Duration);
        m_Enabled = false;
    }
}
