using UnityEngine;
using System.Collections;

public class ReverseControlsShot : MonoBehaviour {

	public float speed = 10.0f;
	public float range = 20.0f;
	private GameObject parentPlayer;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, range / speed);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.forward *speed* Time.deltaTime,Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && other.gameObject != parentPlayer)
		{
			if(!other.GetComponent<Player>().m_Immune)
			{
				other.GetComponent<CharacterController>().EnableReverseControls(5.0f);
				Destroy(gameObject);
			}
		}
	}
	public void SetParentPlayer(GameObject parent)
	{
		parentPlayer = parent;
	}
}
