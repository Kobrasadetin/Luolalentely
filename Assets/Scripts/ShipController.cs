using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{

	public float engineForce = 20.0f;
	public float thrusterForce = 15.0f;
	public float turningTorque = 25;
	public float maxTurningRate = 150;
	//public GameObject engineFlame;
	//public GameObject engineThrusterFlame;
	//public ShipWeapon weapon1;
	//public ShipWeapon weapon2;
	//public ShipWeapon weapon3;
	public GameObject weaponsTarget = null;
	private bool engineActive;
	private Vector2 thruster = Vector2.zero;
	bool thrusterActive = false;
	public bool isAIControlled = false;
	private bool isControlled = false;
	//public GameObject engineSound;
	//public GameObject thrusterSound;

	public void setThrusterVector (Vector2 thrusterVector)
	{

		thruster = thrusterVector;
		thrusterActive = true;
	}

	public void killThruster ()
	{
		thrusterActive = false;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	public void setAsControlled()
	{
		isControlled = true;
	}

	void FixedUpdate ()
	{
		if (engineActive) {
			//	rigidbody2D.AddForce(new Vector2(this.rigidbody2D.transform.rotation[0]*engineForce, this.rigidbody2D.transform.rotation[1]*engineForce));
			//		rigidbody2D.AddForce(new Vector2(0,direction.y*engineForce));
			rigidbody2D.AddForce (transform.up * engineForce);
			
		}
		if (thrusterActive) {
			rigidbody2D.AddForce (Vector2.ClampMagnitude (thruster, 1) * thrusterForce);
			
			//Debug.Log("THRUSTING: vector"+Vector2.ClampMagnitude(thruster,1)*thrusterForce);
		}

		
	}

	// Update is called once per frame
	void Update ()
	{
		if (isControlled)
		{
			//bool engineActive = Input.GetButton("Fire1");
			engineActive = false;
			float turningInput = 0 - Input.GetAxis ("Horizontal");
			if (!isAIControlled) {
				if (Input.GetAxis ("Vertical") > 0.1) {
					engineActive = true;
				} else {
	
				}
				//engineFlame.particleSystem.enableEmission = engineActive;
				
			}
				
			Vector2 thrustAngleWS = Vector2.ClampMagnitude (thruster, 1);
			/*engineThrusterFlame.transform.eulerAngles = new Vector3 (Mathf.Atan2 (thrustAngleWS.x, thrustAngleWS.y) * Mathf.Rad2Deg + 90, 90, 90);
			engineThrusterFlame.particleSystem.enableEmission = thrusterActive;
			*/	
	
	
			float ourAngularVelocity = rigidbody2D.angularVelocity;
			if ((turningInput < -0.1) || (turningInput > 0.1)) {
				if (turningInput > 0) {
					if (ourAngularVelocity < maxTurningRate) {
						rigidbody2D.AddTorque (turningInput * turningTorque);
					}
				}
				if (turningInput < 0) {
					if (ourAngularVelocity > 0 - maxTurningRate) {
						rigidbody2D.AddTorque (turningInput * turningTorque);
					}
				}
			} else {
				if (ourAngularVelocity < -1) {
					rigidbody2D.AddTorque (turningTorque / 5);
				}
			}
			if (ourAngularVelocity > 1) {
				rigidbody2D.AddTorque (-turningTorque / 5);
			}
	
			/*
			if (Input.GetButton ("Fire1")) {
				weapon1.lockTarget (weaponsTarget);
				weapon1.fire ();
				weapon2.lockTarget (weaponsTarget);
				weapon2.fire ();
	
	
			}
			if (Input.GetButton ("Fire2")) {
	
				weapon3.lockTarget (weaponsTarget);
				weapon3.fire ();
	
			}
			handleSounds ();
			*/
		}
	}

	void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info )
	{
		if( stream.isWriting == true )
		{

				stream.SendNext (engineActive);


		}
		else
		{

			this.engineActive = (bool)stream.ReceiveNext();

		}
	}
		/*
	void handleSounds ()
	{
		if (engineActive) {
			if (engineSound) {
				if (!engineSound.audio.isPlaying) {
					engineSound.audio.Play ();
				}
			}
		} else {
			if (engineSound.audio.isPlaying) {
				engineSound.audio.Stop ();
			}
		}
		
		if (thrusterActive) {
			if (thrusterSound) {
				if (!thrusterSound.audio.isPlaying) {
					thrusterSound.audio.Play ();
				}
			}
		} else {
			if (thrusterSound.audio.isPlaying) {
				thrusterSound.audio.Stop ();
			}
		}
	}
	*/
}


