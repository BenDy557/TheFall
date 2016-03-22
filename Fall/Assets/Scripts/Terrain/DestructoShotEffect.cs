using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructoShotEffect : MonoBehaviour {

    public List<GameObject> m_Meshes = new List<GameObject>();
    public List<Material> m_DefaultMaterials = new List<Material>();

    public Material m_PhaseMaterial;
    float m_Duration = 3.0f;
    bool m_Enabled = false;
	// Use this for initialization
	void Start ()
    {

        for (int i = 0; i < m_Meshes.Count; i++)
        {
            m_DefaultMaterials.Add(m_Meshes[i].GetComponent<Renderer>().material);
        }

        gameObject.GetComponent<Collider>().isTrigger = false;
	}

    // Update is called once per frame
    void Update()
    {

        //gameObject.GetComponent<Collider>().isTrigger = false;
        //gameObject.GetComponent<MeshRenderer>().material = m_DefaultMaterial;
        //gameObject.tag = "Ground";

        if (m_Enabled)
        {
            //start animation
            //disable collider
                        
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

        for (int i = 0; i < m_Meshes.Count; i++)
        {
            m_Meshes[i].GetComponent<Renderer>().material = m_PhaseMaterial;
        }
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.tag = "PhasedOut";

        yield return new WaitForSeconds(m_Duration);

        for (int i = 0; i < m_Meshes.Count; i++)
        {
            m_Meshes[i].GetComponent<Renderer>().material = m_DefaultMaterials[i];
        }
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.tag = "Ground";

        m_Enabled = false;
    }
}

