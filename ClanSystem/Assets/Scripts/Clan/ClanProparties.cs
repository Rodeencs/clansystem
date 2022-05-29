using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class ClanProparties : MonoBehaviour
{
    public GameObject ClanPage;
    public GameObject SearchClan;
    public GameObject CreateClan;

    DatabaseReference reference;

    public static ClanProparties instance;

    string clanName;

    private void Awake()
    {
        instance = this;//Singleton
    }


    public void Check()
    {
        Player.localPlayer.Initialize();
        clanName = Player.localPlayer.ClanName;
      
    }

    private void Update()
    {
        if (Player.localPlayer.isAvailableClan)
        {

            ClanPage.SetActive(true);
            SearchClan.SetActive(false);
            CreateClan.SetActive(false);

        }
        else
        {
            ClanPage.SetActive(false);
            SearchClan.SetActive(true);
        }
    }

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }
    

    public void LeaveClan()
    {
        reference.Child("Clans").Child(clanName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Database Hata");
            }


            DataSnapshot snapshot = task.Result;


            var current = int.Parse(snapshot.Child("Players").Value.ToString());
          
                Debug.Log("leaved");
                reference.Child("Clans").Child(clanName).Child("PlayersList").Child(Player.localPlayer.playerName).RemoveValueAsync();
                current-=1;
                reference.Child("Clans").Child(clanName).Child("Players").SetValueAsync(current);
                PlayerPrefs.SetString("clan", "false");

            if(current <= 0)
            {
                reference.Child("Clans").Child(clanName).RemoveValueAsync();
            }

                Check();
            
          




        });
    }
}
