using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{

    [SerializeField] private TMP_Text displayText = null;
    [SerializeField] private Renderer displayColorRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField] private string displayName = "Missing_Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField] private Color teamColor = Color.gray;

    #region Server
    [Server]
    public void SetDisplayName(string newname)
    {
        if (newname.Length > 20 || newname.Contains(" ") || newname.Length < 5)
        {
            return;
        }
        displayName = newname;
    }

    [Server]
    public void SetTeamColorRandomly()
    {
        Color color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        teamColor = color;
    }

    [Client]
    private void CmdSetDisplayName(string newname)
    {
        if (newname == null)
        {
            return;
        }
        RpcMethodForAllClient(newname);
        SetDisplayName(newname);
    }
    #endregion

    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayText.text = newName;
    }

    [ContextMenu("Set My Name")]
    public void SetMyName()
    {
        CmdSetDisplayName("MyN");
    }

    [ClientRpc]
    private void RpcMethodForAllClient(string newname)
    {
        Debug.Log(newname);
    }
    #endregion
}
