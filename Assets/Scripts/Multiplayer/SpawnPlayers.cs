using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab;

    public static GameObject playerInstance;

    public Transform[] playerSpawn;

    private void Start()
    {
        // Get a random index for the spawn point
        int randomIndex = Random.Range(0, playerSpawn.Length);
        // Instantiate the GameObject at the selected spawn point
        playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawn[randomIndex].position, Quaternion.identity);
    }
}
