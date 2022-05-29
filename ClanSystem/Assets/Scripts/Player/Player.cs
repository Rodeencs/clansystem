using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player localPlayer;

    public bool isAvailableClan;

    public string ClanName;


    public string playerName = "LocalPlayer";
    private void Awake()
    {
        localPlayer = this;
    }

    private void Start()
    {
        ClanProparties.instance.Check();
    }

    public void Initialize()
    {
        
        ClanName = PlayerPrefs.GetString("clanName");
        if (PlayerPrefs.GetString("clan") == "true") isAvailableClan = true;
        else isAvailableClan = false;

        
    }
}
