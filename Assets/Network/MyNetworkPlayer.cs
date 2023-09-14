using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private string displayName = "Missing Name";

    [SyncVar]
    [SerializeField] private Color teamColor = Color.gray;

    [Server]
    public void SetDisplayName(string newname)
    {
        displayName = newname;
    }

    [Server]
    public void SetTeamColorRandomly()
    {
        Color color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        teamColor = color;
        GetComponent<Material>().color = teamColor;
    }

}
