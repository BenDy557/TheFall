using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshColour : MonoBehaviour {

    public List<GameObject> m_Meshes = new List<GameObject>();

    public enum PlayerColour {Default, Red, Blue, Green, Yellow };
    public PlayerColour m_PlayerColour = PlayerColour.Default;
    public Material m_default; 
    public Material m_Red;
    public Material m_Blue;
    public Material m_Green;
    public Material m_Yellow;
    public Material m_CurrentMaterial;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ColourizeMesh(PlayerColour playerColourIn)
    {
        switch (playerColourIn)
        {
            case PlayerColour.Red:
                m_CurrentMaterial = m_Red;
                break;
            case PlayerColour.Blue:
                m_CurrentMaterial = m_Blue;
                break;
            case PlayerColour.Green:
                m_CurrentMaterial = m_Green;
                break;
            case PlayerColour.Yellow:
                m_CurrentMaterial = m_Yellow;
                break;
            case PlayerColour.Default:
                break;
            default:
                break;
        }

        for (int i = 0; i < m_Meshes.Count; i++)
        {
            m_Meshes[i].GetComponent<SkinnedMeshRenderer>().material = new Material(m_CurrentMaterial);
        }

    }
}
