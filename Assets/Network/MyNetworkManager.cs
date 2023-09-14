using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);


        //Setting player name
        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.SetDisplayName($"Player {numPlayers}");


        //Setting player teamcolor but doesnt check is there any same color. (it just make random color for now)
        player.SetTeamColorRandomly();


        Debug.Log("Servera bir oyuncu eklendi artýk oyunda " + numPlayers + " tane oyuncu var");

        
    }
}
