using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    public Camera mainCamera;


    #region Server

    [Command]
    private void CmdMovePlayer(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }


        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client


    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    [ClientCallback] private void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if (!Input.GetMouseButtonDown(1))
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity))
        {
            return; 
        }

        CmdMovePlayer(hit.point);
    }



    #endregion
}
