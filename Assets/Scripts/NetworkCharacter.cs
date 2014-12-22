using UnityEngine;
using System.Collections;

public class NetworkCharacter : GameBehaviour {

		public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.isWriting)
			{
				// We own this player: send the others our data
				stream.SendNext(transform.position);
				stream.SendNext(transform.rotation);
				//stream.SendNext(this.rigidbody2D.velocity);
				stream.SendNext(this.rigidbody2D.angularVelocity);
			}
			else
			{
				// Network player, receive data
				this.transform.position = (Vector3) stream.ReceiveNext();
				this.transform.rotation = (Quaternion) stream.ReceiveNext();
				//Vector2 velocity = (Vector2) stream.ReceiveNext();
				float avelocity = (float) stream.ReceiveNext();
				//this.rigidbody2D.velocity = velocity;
				this.rigidbody2D.angularVelocity = avelocity;
				//Debug.Log("received velocity "+velocity);
				Debug.Log("received angluar velocity "+avelocity);
				}
		}	

}