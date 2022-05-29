using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Clan.Data;
public class ClanFinder : MonoBehaviour
{
    [Header("Clan View")]
    [SerializeField] GameObject ClanView;
    [SerializeField] Transform ClanInstantiateParent;

    [Space(20)]
    [Header("Proparties")]
    [SerializeField] ClanStandard options;

    DatabaseReference reference;
    
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        InitClans();
    }

    
    public void InitClans()
    {
        reference.Child("Clans").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Database Hata");
            }
          
                DataSnapshot snapshot = task.Result;
          

                foreach (var childValue in snapshot.Children)
                {
                    Debug.Log("New data getted");
                    var newPacket = new ClanData();
                    newPacket.ClanName =  childValue.Child("clanName").Value.ToString();
                    if (childValue.Child("Visible").Value.ToString() == "true") newPacket.ClanVisible = true;
                    else newPacket.ClanVisible = false;
                    newPacket.MaxPlayers = options.MaxPlayers;
                    newPacket.Players = int.Parse(childValue.Child("Players").Value.ToString());

                    Create_Result(newPacket);

                }

            
        });
    }

    void Create_Result(ClanData packet)
    {
        if (packet.ClanVisible == false) return;
        var view = Instantiate(ClanView,ClanInstantiateParent);
        view.GetComponent<ViewInfo>().clanData = packet;

        Debug.Log("Created New Result");
    }

}
