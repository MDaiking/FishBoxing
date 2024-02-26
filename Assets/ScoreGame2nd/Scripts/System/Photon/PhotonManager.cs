using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings();

	}

	private void Update()
	{
		RoomOptions roomOption = new RoomOptions();
		roomOption.MaxPlayers = 2;
	}
	
	public void CreateRoom()
	{

	}
}
