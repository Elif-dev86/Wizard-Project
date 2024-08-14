using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEvent : MonoBehaviour
{
    public string attackSelected;

    public GameObject attackSpawn;

    public GameObject[] attackPrefabs;

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

                    case "bullethell":

                        int[] rotationAngles = {-45, -25, 0, 25, 45 };

                        for (int j = 0; j < 5; j++)
                        {

                            GameObject bulletHellObj = Instantiate(attackPrefabs[3]);

                            AttackSpawn(bulletHellObj);

                            Quaternion attackRotation = Quaternion.Euler(0, rotationAngles[j], 0);

                            bulletHellObj.transform.rotation = attackRotation;
                        }

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
