using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject playerUI;
    public GameObject gameMan;

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
    }

    void Update()
    {
        
    }
}
