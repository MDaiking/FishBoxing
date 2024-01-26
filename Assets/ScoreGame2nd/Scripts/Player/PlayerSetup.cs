using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Networking;

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
		
		private Vector3 GetSpawnPosition()
		{
			return new Vector3(0.0f, 5.0f, 0.0f);
		}
	
		public override void OnNetworkSpawn()
		{
			base.OnNetworkSpawn();
			I = this.gameObject;
			transform.position = GetSpawnPosition();
			if (IsOwner)
			{
				SettingSetup();
				CameraSetup();
			}
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				transform.position = new Vector3(0.0f, 1000.0f, 0.0f);
				Debug.Log("unchi!!!!!!!!!!!");
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