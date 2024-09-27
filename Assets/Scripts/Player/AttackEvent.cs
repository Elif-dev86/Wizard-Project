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

                    case "gaimagma":

                        Instantiate(attackPrefabs[4]);

                        break;

                    case "burnedknuckles":

                        Instantiate(attackPrefabs[5]);

                        break;

                    case "teleport":

                        Instantiate(attackPrefabs[6]);

                        break;

                    case "spark":

                        Instantiate(attackPrefabs[7]);

                        break;

                    case "thunderclap":

                        Instantiate(attackPrefabs[8]);

                        break;

                    case "hyperx":

                        GameObject hyperXObj = Instantiate(attackPrefabs[9]);

                        AttackSpawn(hyperXObj);

                        break;

                    case "rottenspit":

                        GameObject spitObj = Instantiate(attackPrefabs[10]);

                        AttackSpawn(spitObj);

                        break;

                    case "cumulostinkus":

                        GameObject stinkObj = Instantiate(attackPrefabs[11]);

                        AttackSpawn(stinkObj);

                        break;

                    case "stingray":

                        Instantiate(attackPrefabs[12]);

                        break;

                    case "heartthorns":

                        Instantiate(attackPrefabs[13]);

                        break;

                    case "roseblood":

                        Instantiate(attackPrefabs[14]);

                        break;

                    case "impalement":

                        Instantiate(attackPrefabs[15]);

                        break;

                    case "thousandniddles":

                        Instantiate(attackPrefabs[16]);

                        break;

                    case "meteorstrike":

                        Instantiate(attackPrefabs[17]);

                        break;

                    case "quaternions":

                        Instantiate(attackPrefabs[18]);

                        break;

                    case "twister":

                        GameObject twisterObj = Instantiate(attackPrefabs[19]);

                        AttackSpawn(twisterObj);

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
