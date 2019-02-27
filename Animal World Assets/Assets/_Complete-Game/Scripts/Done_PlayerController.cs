using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	// For Mobile 
	public SimpleTouchAreaButton areaButtonFire;

    // Changing
	//public SimpleTouchMoveButton touchPad;

	// Questionable Code.......................................... 
	private Quaternion calibrationQuaternion;
	void Start () {
		CalibrateAccelerometer ();
	}

	void CalibrateAccelerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}
	//Questionable Code............................................

	void Update ()
	{
		#if UNITY_STANDALONE
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		#endif
		#if UNITY_IOS || UNITY_ANDROID
		if (areaButtonFire.CanFire() && Time.time > nextFire)
		#endif
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play (); // This Audio reference THIS object 
		}
	}

  
	//void FixedUpdate ()
	//{
       
		//#if UNITY_STANDALONE
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//GetComponent<Rigidbody>().velocity = movement * speed;

		////Using Quaternion balancing 
		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 acceleration = FixAcceleration (accelerationRaw);
		//movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		//// End of Quaternion Balancing
		//#endif

		// Getting the direction from he player, then set movement. 
		//#if UNITY_ANDROID || UNITY_IOS	
		//Vector2 direction = touchPad.GetDirection ();
		//Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		//#endif
		//GetComponent<Rigidbody>().velocity = movement * speed;

		//GetComponent<Rigidbody>().position = new Vector3
		//	(
		//		Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
		//		0.0f, 
		//		Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		//	);

		//GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	//}

    void FixedUpdate()
    {

        if (Input.touchCount > 0)
        {
            // The screen has been touched so store the touch
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved && touch.phase != TouchPhase.Stationary)
            {
                // If the finger is on the screen, move the object smoothly to the touch position
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0.0f, 0.0f));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(touchPosition.x, 0.0f , 0.0f), Time.deltaTime * 1000f);

                // Clamp the Axis
                GetComponent<Rigidbody>().position = new Vector3
                  (
                      Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
                      0.0f, 
                      Mathf.Clamp (GetComponent<Rigidbody>().position.z, -1.5f, -1.5f)
                  );
            }
        }
    }
}
