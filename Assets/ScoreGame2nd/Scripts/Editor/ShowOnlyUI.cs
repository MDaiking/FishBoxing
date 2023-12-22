using UnityEngine;
using System.Collections;
using UnityEditor;

public class UISceneView : SceneView
{
	[MenuItem("Window/UISceneView")]
	static void Init()
	{
		var window = ScriptableObject.CreateInstance<UISceneView>();

		window.in2DMode = true;
		window.sceneLighting = false;
		window.Show();
	}

	int glovalLayer;

	void OnFocus()
	{
		glovalLayer = Tools.visibleLayers;
	}

	void OnLostFocus()
	{
		Tools.visibleLayers = glovalLayer;
	}

	void OnGUI()
	{
		var layers = Tools.visibleLayers;
		Tools.visibleLayers = 32;
		var type = typeof(SceneView);
		var methodOnGUI = type.GetMethod("OnGUI",
										  System.Reflection.BindingFlags.NonPublic |
										  System.Reflection.BindingFlags.Instance);
		methodOnGUI.Invoke(this, null);
		Tools.visibleLayers = layers;
	}
}