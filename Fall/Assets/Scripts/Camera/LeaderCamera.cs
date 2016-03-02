using UnityEngine;
using System.Collections;

public class LeaderCamera : MonoBehaviour 
{
	public float speed;
	public float speedScaler;
	private GameObject[] players; 
	
	private Rigidbody rb;
	private GameObject leader;
	private GameObject last;
	private float highest;
	private float lowest;
	private float hiLoSpread;
	private float scaler;
	private float screenHeight;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0.0f, 1.0f, 0.0f);								//camera movement defaults to "speed"
        screenHeight = 2 * Mathf.Atan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2);		//calculates screen height from field of view 

        players = new GameObject[0];
        players = GameObject.FindGameObjectsWithTag("Player");							//creates array of game objects and fills it with objects tagged "Player"
        if (players.Length == 0)
        {
            Debug.Log("No game objects found with tag Player");
        }																			//sends message if array is empty
        else
        {
            highest = 0;
            foreach (GameObject player in players)											//loops through array and assigns leader to the highest one
            {
                if (player.transform.position.y > highest)
                {
                    leader = player;
                    highest = leader.transform.position.y;
                }
            }

			lowest = leader.transform.position.y;

			foreach (GameObject player in players)
			{
				if (player.transform.position.y <leader.transform.position.y && player.transform.position.y < lowest)
				{
					last = player;
					lowest = last.transform.position.y;
				}
			}

			hiLoSpread = highest - lowest -8;

			if (hiLoSpread<0)
			{
				transform.position = new Vector3 (0.0f,transform.position.y,-10f);
			}
			else if (hiLoSpread>0 && hiLoSpread < 3)
			{
				transform.position = new Vector3 (0.0f,transform.position.y,Mathf.Lerp(-10,-15, hiLoSpread/3));
			}
			else if (hiLoSpread>3)
			{
				transform.position = new Vector3 (0.0f,transform.position.y, -15f);
			}

            if (leader.transform.position.y > transform.position.y)
            {
                scaler = 1 + (speedScaler * Mathf.Abs((transform.position.y - leader.transform.position.y) /
                                                  (transform.position.z - leader.transform.position.z)) / (screenHeight / 2));
                // calculates the value that represents how far above the middle of the camera's view the leader is
                // (0 for at the middle, 1 for at the top) and multiplies it by public float speed scaler. We add 1 so camera
                // speed doesn't drop to zero at transition.
                movement = movement * scaler;
            }

            rb.velocity = (movement * speed);
        }
    }
	

}


// by balancing the the ratio of speed to speed scaler I think the right feel can be acheived, its nice and smooth anyway. Note: max camera speed =~ speed*speedscaler.