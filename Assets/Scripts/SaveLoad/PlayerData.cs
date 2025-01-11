using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    
    public int level;
    public float health;
    public string[] items;
    public int[] itemStackIndex;
    public float[] position;

    public PlayerData (GameManager manager)
    {
        //health = player.healtBar.value;

        items = manager.inventoryItemData;
        
        itemStackIndex = manager.potionStackIndex;

        //position = new float[3];
        //position[0] = player.transform.position.x;
        //position[1] = player.transform.position.y;
        //position[2] = player.transform.position.z;
    }

}
