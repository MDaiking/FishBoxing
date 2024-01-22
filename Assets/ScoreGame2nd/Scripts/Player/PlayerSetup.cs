using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Unity.Netcode.Samples.APIDiorama.RPC
{

	public class PlayerSetup : Unity.Netcode.NetworkBehaviour
	{
		private GameObject I;
		[SerializeField]
		private GameObject fpsCameraPrefab;
		[SerializeField]
		private GameObject tpsCameraPrefab;
		private PlayerSetting playerSetting;

		[SerializeField]
		private GameObject cameraRoot;

		void Start()
		{
			NetworkManager.ConnectionApprovalCallback = ConnectionApprovalCallback;
		}

		void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
		{
			/* you can use this method in your project to customize one of more aspects of the player
             * (I.E: its start position, its character) and to perform additional validation checks. */
			response.Approved = true;
			response.CreatePlayerObject = true;
			response.Position = GetPlayerSpawnPosition();
		}

		Vector3 GetPlayerSpawnPosition()
		{
			/*
             * this is just an example, and you change this implementation to make players spawn on specific spawn points
             * depending on other factors (I.E: player's team)
             */
			return new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
		}
		public override void OnNetworkSpawn()
		{
			I = this.gameObject;
			CurrentLayerSetup();
			if (IsOwner)
			{
				SettingSetup();
				CameraSetup();
				transform.position = new Vector3(0.0f, 6.0f, 30.0f);
			}
		}
		private void CameraSetup()
		{
			fpsCameraPrefab.GetComponent<CameraSetup>().Instantiate(this.gameObject, cameraRoot);
			tpsCameraPrefab.GetComponent<CameraSetup>().Instantiate(this.gameObject, cameraRoot);

			var playerInvisible = GetComponent<PlayerInvisible>();
			playerInvisible.SetInvisible(true);

		}
		private void SettingSetup()
		{
			playerSetting = GameObject.FindWithTag("GameManager").GetComponent<PlayerSetting>();
			playerSetting.SetPlayerGO(I);
		}
		private void CurrentLayerSetup()//レイヤー設定
		{
			if (IsOwner)
			{
				SetTag(I, "Player");
				SetAllLayer(I, 6);//6:Player
			}
			else
			{
				SetTag(I, "Enemy");
				SetAllLayer(I, 7);//7:Enemy
			}
		}
		private void SetTag(GameObject go, string tag)
		{
			go.tag = tag;
		}
		private void SetAllLayer(GameObject go, int layer)//子オブジェクトを含めたすべてのレイヤーを変更
		{
			go.layer = layer;
			foreach (Transform n in go.transform)
			{
				SetAllLayer(n.gameObject, layer);
			}
		}
	}
}