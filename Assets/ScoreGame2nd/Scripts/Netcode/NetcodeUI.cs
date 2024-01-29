using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetcodeUI : MonoBehaviour
{
	private string ipAddress;
	private ushort port = 7777;

	public void StartHost()
	{
		NetworkManager.Singleton.StartHost();
	}
	public void StartClient()
	{
		var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport;
		if (transport is Unity.Netcode.Transports.UTP.UnityTransport unityTransport)
		{
			// 接続先のIPアドレスとポートを指定
			unityTransport.ConnectionData.ServerListenAddress = ipAddress;
		}
		Debug.Log($"client connect : ip={ipAddress}, port={port}");

		bool a = NetworkManager.Singleton.StartClient();
		Debug.Log(a);
	}

	public void ChangeIpAddress(string newIpAddress)
	{
		ipAddress = newIpAddress;
	}

	void Start()
	{
		UnityTransport unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();

		Debug.Log("UnityTransport: " + unityTransport);

		unityTransport.OnTransportEvent += OnTransportEvent;
	}

	private void OnTransportEvent(Unity.Netcode.NetworkEvent eventType, ulong clientId, ArraySegment<byte> payload, float receiveTime)
	{
		Debug.Log("OnTransportEvent: " + eventType);
	}
}