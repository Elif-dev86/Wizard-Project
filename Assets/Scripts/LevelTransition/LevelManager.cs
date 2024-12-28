using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] pathPoints;

    [SerializeField]
    float pathSpeed;

    [SerializeField]
    bool canFollowPath;

     [SerializeField]
    bool moveToNextPoint;

    GameObject player;

    PlayerMovement rotPlayer;

    private void Start() 
    {
          player = GameObject.FindGameObjectWithTag("Player");

          rotPlayer = FindObjectOfType<PlayerMovement>();
    }

    private void FixedUpdate() 
    {
          if (canFollowPath)
          {
               FollowPath(player);
          }
    }

   private void OnTriggerEnter(Collider other) 
   {

     if (other.CompareTag("Player"))
     {
          rotPlayer.canMove = false;
          rotPlayer.canGravity = false;

          moveToNextPoint = false;
          canFollowPath = true;

          StartCoroutine(TimeToTeleport());
     }

   }

   void FollowPath(GameObject player)
   {
     
        float step = pathSpeed * Time.deltaTime; 

        float point1Distance = Vector3.Distance(player.transform.position, pathPoints[0].transform.position);

        if (!moveToNextPoint)
        {
          rotPlayer.RotateToTarget(pathPoints[0]);

          player.transform.position = Vector3.MoveTowards(player.transform.transform.position, pathPoints[0].transform.position, step);
        }
        else
        {
          rotPlayer.RotateToTarget(pathPoints[1]);

          player.transform.position = Vector3.MoveTowards(player.transform.transform.position, pathPoints[1].transform.position, step);
        }

        if (point1Distance < 1)
        {
          moveToNextPoint = true;
        }
   }

   private IEnumerator TimeToTeleport()
   {


    yield return new WaitForSeconds(2.5f);

    SceneManager.LoadScene("test_scene");
   }
}
