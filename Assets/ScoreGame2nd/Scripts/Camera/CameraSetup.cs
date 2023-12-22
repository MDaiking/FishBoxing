using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[System.Serializable]
public enum CameraType
{
    first,
    third,
}
public class CameraSetup : MonoBehaviour
{
    [SerializeField]
    private CameraType cameraType; 
    public GameObject Instantiate(GameObject player, GameObject cameraRoot)
	{
        var I = Instantiate(gameObject);
        var cvc = I.GetComponent<CinemachineVirtualCamera>();
        var cameraChange = player.GetComponent<CameraChange>();
        cvc.Follow = cameraRoot.transform;

        //設定にてPOV変更用
        PlayerSetting playerSetting = GameObject.FindWithTag("GameManager").GetComponent<PlayerSetting>();
        playerSetting.SetCamera(cvc,cameraType);

		switch (cameraType)//カメラの種類で優先度の初期値を決定
		{
            case CameraType.first:
                cameraChange.FpsCamera = cvc;
                cvc.Priority = cameraChange.ActivePriority;
                break;
            case CameraType.third:
                cameraChange.TpsCamera = cvc;
                cvc.Priority = cameraChange.InactivePriority;
                cvc.LookAt = cameraRoot.transform;
                break;
		}

        return I;
	}
}
