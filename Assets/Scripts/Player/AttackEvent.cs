using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEvent : MonoBehaviour
{
    public string attackSelected;

    public GameObject attackSpawn;

    public GameObject[] attackPrefabs;

    private void Start()
    {
        
    }

    public void AttackCall()
    {
        for (int i = 0; i < attackPrefabs.Length; i++)
        {

            if (attackPrefabs[i].name.ToLower() == attackSelected.ToLower())
            {


                switch (attackSelected.ToLower())
                {
                    case "fireball":

                        GameObject fireBallObj = Instantiate(attackPrefabs[0]);

                        AttackSpawn(fireBallObj);

                        break;

                    case "thinspark":

                        Instantiate(attackPrefabs[1]);

                        break;

                    case "deathbed":

                        Instantiate(attackPrefabs[2]);

                        break;
                }


            }


        }

    }

    public void AttackSpawn(GameObject gameObj)
    {
        gameObj.transform.position = attackSpawn.transform.position;
    }
}
