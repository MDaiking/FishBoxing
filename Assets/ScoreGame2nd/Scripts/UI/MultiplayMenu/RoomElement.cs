using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomElement : ButtonInUI
{
    private TMP_InputField serverNameInput;
    private TextMeshProUGUI tmpro;
    private string serverName;
    public string ServerName
	{
        get{ return serverName; }
	}

    public bool Instantiate(string _serverName)
	{
        serverNameInput = GameObject.FindWithTag("ServerNameInput").GetComponent<TMP_InputField>();
        if(serverNameInput == null)
		{
            return false;
		}

        Instantiate(this);
        tmpro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        serverName = _serverName;
        tmpro.text = serverName;
        AddEventTrigger(
            new Action(() =>
            {
            }),
            new Action(() =>
            {
            }),
            new Action(() =>
            {
                SetServerName();
            }));
        return true;
	}

    private void SetServerName()
	{
        serverNameInput.textComponent.text = serverName;
	}
}
