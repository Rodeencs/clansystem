using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clan.Data;
using Clan;
using TMPro;
using Firebase.Database;
using Firebase.Extensions;
public class ViewInfo : MonoBehaviour
{
    [HideInInspector]
    public ClanData clanData;

    [SerializeField] ClanStandard standards;

    [SerializeField] TMP_Text clanName;
    [SerializeField] TMP_Text clanPlayers;

    DatabaseReference reference;

    private void Start()
    {
        
        clanName.text = clanData.ClanName;
        clanPlayers.text = clanData.Players + "/" + standards.MaxPlayers;

        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    

   
    public void JoinClan()
    {
        


        reference.Child("Clans").Child(clanData.ClanName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Database Hata");
            }


            DataSnapshot snapshot = task.Result;

            
            var current = int.Parse(snapshot.Child("Players").Value.ToString());
            if(current < 50)
            {
                Debug.Log("Joined");
                reference.Child("Clans").Child(clanData.ClanName).Child("PlayersList").Child(Player.localPlayer.playerName).SetValueAsync(Player.localPlayer.playerName);
                current++;
                reference.Child("Clans").Child(clanData.ClanName).Child("Players").SetValueAsync(current);
                PlayerPrefs.SetString("clan", "true");
                PlayerPrefs.SetString("clanName",clanData.ClanName);

                ClanProparties.instance.Check();
            }
            else
            {
                Debug.Log("Not joined" + current);
                return;
            }
            



        });

    }
}
