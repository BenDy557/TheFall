using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    private Ray m_LaserTrajectory;
    public float m_LaserRange = 15.0f;

    public GameObject m_LaserObject;
    private GameObject m_LaserInstance;

    public LayerMask m_LayerMask;

	// Use this for initialization
	void Start ()
    {
        m_LaserInstance = Instantiate<GameObject>(m_LaserObject);
        m_LaserInstance.transform.parent = gameObject.transform;

        m_LaserTrajectory.direction = m_LaserInstance.transform.up;
        m_LaserTrajectory.origin = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        m_LaserInstance.transform.localScale = new Vector3(m_LaserInstance.transform.localScale.x, m_LaserRange, m_LaserInstance.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
       

        m_LaserTrajectory.origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        m_LaserTrajectory.direction = transform.up;


        Debug.DrawRay(m_LaserTrajectory.origin, m_LaserTrajectory.direction * m_LaserRange, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(m_LaserTrajectory, out hit, m_LaserRange, m_LayerMask))
        {
            //if(hit.transform.gameObject.CompareTag("Ground")
            {
                Vector3 laserDifference = transform.position - hit.point;
                Debug.DrawRay(m_LaserTrajectory.origin, m_LaserTrajectory.direction * Vector3.Distance(hit.point, m_LaserTrajectory.origin), Color.red);

                m_LaserInstance.transform.localScale = new Vector3(m_LaserInstance.transform.localScale.x, Vector3.Distance(hit.point, m_LaserTrajectory.origin) / 2, m_LaserInstance.transform.localScale.z);
            }
        }
        else
        {
            m_LaserInstance.transform.localScale = new Vector3(m_LaserInstance.transform.localScale.x, m_LaserRange, m_LaserInstance.transform.localScale.z);
        }


        m_LaserInstance.transform.localPosition = new Vector3(0.0f, (m_LaserInstance.transform.localScale.y), 0.0f);




	}
}
