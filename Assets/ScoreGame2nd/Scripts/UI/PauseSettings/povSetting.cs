using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class povSetting : MonoBehaviour
{
	private Slider slider;
	[SerializeField]
	private TemporarySetting ts;
	private void Start()
	{
		slider = GetComponent<Slider>();

	}
	private void Update()
	{
		ts.tempoSetting.Pov = (int)slider.value;
	}
}
