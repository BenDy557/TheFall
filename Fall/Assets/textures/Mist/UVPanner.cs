using UnityEngine;
using System.Collections;

public class UVPanner : MonoBehaviour {
	
	public float scrollSpeed = 0.01F;

	Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float offset = Time.time * scrollSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
	