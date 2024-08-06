using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamFollow : MonoBehaviour
{
    private GameObject player;
    private Transform followTarget;
    private CinemachineVirtualCamera vCam;

    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        player = SpawnPlayers.playerInstance;
    }

    private void Update()
    {
        if (player != null)
        {
            followTarget = player.transform;

            vCam.Follow = followTarget;
            vCam.LookAt = followTarget;
        }
        else
        {
            player = SpawnPlayers.playerInstance;
        }
    }
}
