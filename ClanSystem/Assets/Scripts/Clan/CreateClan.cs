using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.UI;
public class CreateClan : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] TMP_InputField ClanName;
    [SerializeField] Toggle ClanVisible;
    DatabaseReference reference;

    bool iCan;
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    public void Create()
    {
        if (Createble())
        {
            Debug.Log("Createble");


            reference.Child("Clans").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Database Hata");
                }

                DataSnapshot snapshot = task.Result;
              
                

                    if (snapshot.Child(ClanName.text).Value == null)
                    {
                        Debug.Log("Succesfly Created");
                        reference.Child("Clans").Child(ClanName.text).SetValueAsync(ClanName.text);
                        reference.Child("Clans").Child(ClanName.text).Child("clanName").SetValueAsync(ClanName.text);
                        if (ClanVisible.isOn) reference.Child("Clans").Child(ClanName.text).Child("Visible").SetValueAsync("true");
                        else reference.Child("Clans").Child(ClanName.text).Child("Visible").SetValueAsync("false");
                        reference.Child("Clans").Child(ClanName.text).Child("Players").SetValueAsync(1);
                    reference.Child("Clans").Child(ClanName.text).Child("PlayersList").Child(Player.localPlayer.playerName).SetValueAsync(Player.localPlayer.playerName);

                    //For localPlayer
                    PlayerPrefs.SetString("clan", "true");
                    PlayerPrefs.SetString("clanName", ClanName.text);

                    ClanProparties.instance.Check();
                }

                
            });





        }
        
    }


   

    public bool Createble()
    {
        var clanName = ClanName.text;
        if(clanName != string.Empty && clanName != null && clanName.Length > 4)
        {
            return true;
        }

        return false;
       
    }
}
