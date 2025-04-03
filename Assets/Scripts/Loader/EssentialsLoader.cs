using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject gameMan;
    public GameObject Player;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameMan);
        }

        if (InventoryManagement.instance == null)
        {
            Instantiate(playerUI);
        }
        
        if (PlayerMovement.instance == null)
        {
            Instantiate(Player);
        }
    }

    void Update()
    {
        
    }
}
