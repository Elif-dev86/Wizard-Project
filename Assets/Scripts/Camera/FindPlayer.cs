using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform followTarget;
    private CinemachineVirtualCamera vCam;

    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            followTarget = player.transform;

            vCam.Follow = followTarget;
            vCam.LookAt = followTarget;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
