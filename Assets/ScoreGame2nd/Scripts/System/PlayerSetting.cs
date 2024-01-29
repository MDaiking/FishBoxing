using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

//プレイヤーの設定に関するクラス
public class PlayerSettingData
{

	private int sensitivity;//動作感度
	public static readonly int initSensitivity = 100;
	public int Sensitibity
	{
		get { return sensitivity; }
	}

	private int pov;//視野角
	public static readonly int initPov = 60;
	public int Pov
	{
		get { return pov; }
		set { pov = value; }
	}

	private Vector2Int resolution;//解像度
	public static readonly Vector2Int initResolution = new Vector2Int(1920, 1080);
	public Vector2Int Resolution
	{
		get { return resolution; }
		set { resolution = value; }
	}

	private FullScreenMode screenMode;//スクリーン形態
	public static readonly FullScreenMode initScreenMode = FullScreenMode.FullScreenWindow;
	public FullScreenMode ScreenMode
	{
		get { return screenMode; }
		set { screenMode = value; }
	}

	private int shadowResolution;//影解像度
	public static readonly int initShadowResolution = 1024;
	public int ShadowResolution
	{
		get { return shadowResolution; }
		set { shadowResolution = value; }
	}

	public PlayerSettingData()
	{
		sensitivity = initSensitivity;
		pov = initPov;
		resolution = initResolution;
		screenMode = initScreenMode;
		shadowResolution = initShadowResolution;
	}

	public void Save(PlayerSettingData psd)
	{
		sensitivity = psd.Sensitibity;
		pov = psd.Pov;
		resolution = psd.Resolution;
		screenMode = psd.ScreenMode;
		shadowResolution = psd.ShadowResolution;
	}
	public void Initialize()
	{
		sensitivity = initSensitivity;
		pov = initPov;
		resolution = initResolution;
		screenMode = initScreenMode;
		shadowResolution = initShadowResolution;
	}
}
public class PlayerSetting : MonoBehaviour
{
	public PlayerSettingData settingData;
	private GameObject player;
	private CinemachineVirtualCamera fpsCamera;
	private CinemachineVirtualCamera tpsCamera;
	[SerializeField]
	private Light directionalLight;
	private void Start()
	{
		settingData = new PlayerSettingData();
		//fpsCameraPrefab = 
	}
	public void SetPlayerGO(GameObject _player)
	{
		player = _player;
	}
	public void ChangeSettings()
	{
		ChangeSensitivity();
		ChangePov();
		ChangeScreen();
		ChangeShadowResolution();
	}

	private void ChangeSensitivity()
	{
		player.GetComponent<CameraRotate>().RotateSpeed = (float)settingData.Sensitibity / 20.0f;
	}
	private void ChangePov()
	{
		fpsCamera.m_Lens.FieldOfView = settingData.Pov;
		tpsCamera.m_Lens.FieldOfView = settingData.Pov;
	}
	private void ChangeScreen()
	{
		int width = settingData.Resolution.x;
		int height = settingData.Resolution.y;
		Screen.SetResolution(width, height, settingData.ScreenMode);
	}
	private void ChangeShadowResolution()
	{
		int sr = settingData.ShadowResolution;
		switch (sr)
		{
			case (256):
				directionalLight.shadowResolution = LightShadowResolution.Low;
				break;
			case (512):
				directionalLight.shadowResolution = LightShadowResolution.Medium;
				break;
			case (1024):
				directionalLight.shadowResolution = LightShadowResolution.High;
				break;
			case (2048):
				directionalLight.shadowResolution = LightShadowResolution.VeryHigh;
				break;
			default:
				Debug.LogError("影の解像度は256,512,1024,2048内の数字のみ使用可能");
				break;
		}
	}
	public void SetCamera(CinemachineVirtualCamera camera, CameraType type)
	{
		switch (type)
		{
			case CameraType.first:
				fpsCamera = camera;
				break;
			case CameraType.third:
				tpsCamera = camera;
				break;
		}
	}
}
