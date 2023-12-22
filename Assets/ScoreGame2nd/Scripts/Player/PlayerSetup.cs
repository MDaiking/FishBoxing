using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSetup : Unity.Netcode.NetworkBehaviour
{
	private GameObject I;
	[SerializeField]
	private GameObject fpsCameraPrefab;
	[SerializeField]
	private GameObject tpsCameraPrefab;
	[SerializeField]
	private PlayerSetting playerSetting;

	[SerializeField]
	private GameObject cameraRoot;
	[SerializeField]
	private CameraChange cameraChange;
	public override void OnNetworkSpawn()
	{
		I = this.gameObject;
		CurrentLayerSetup();
		if (IsOwner)
		{
			SettingSetup();
			CameraSetup();
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
