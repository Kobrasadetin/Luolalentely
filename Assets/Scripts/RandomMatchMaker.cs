using UnityEngine;
using System.Collections;


public class RandomMatchMaker : GameBehaviour
{
	// Use this for initialization
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		//PhotonNetwork.logLevel = PhotonLogLevel.Full;
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
	
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom(null);
	}
	
	void OnJoinedRoom()
	{
		GameObject monster = PhotonNetwork.Instantiate("Ship_default", Vector3.zero, Quaternion.identity, 0);
		monster.GetComponent<ShipController>().setAsControlled();
	}
}