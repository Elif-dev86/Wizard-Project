using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private LevelInstace instance;

    [SerializeField]
    private string targetSceneName;

    [SerializeField]
    private Transform spawnPoint;

    //[SerializeField] 
    //private float levelLoadDelay = 1f;

    [SerializeField]
    GameObject[] pathPoints;

    [SerializeField]
    float teleportTimeStairs;

    [SerializeField]
    float teleportTimeDirect;

    [SerializeField]
    float pathSpeed;

    [SerializeField]
    bool canFollowPath;

     [SerializeField]
    bool moveToNextPoint;

    private bool[] transitionType;

    public bool isStairs, isStraight;

    GameObject player;

    PlayerMovement rotPlayer;

    private void Start() 
    {
      player = GameObject.FindGameObjectWithTag("Player");

      rotPlayer = FindObjectOfType<PlayerMovement>();

      transitionType = new bool[] {isStairs, isStraight};

      if(instance == LevelInstace.currentInstance)
      {
        Debug.Log("I made it here");

        FindObjectOfType<PlayerMovement>().transform.position = spawnPoint.position;

        StartCoroutine(WaitToMove());
      }
    }

    private void LateUpdate() 
    {
      if (canFollowPath)
      {
        FollowPath(player);
      }
    }

   private void OnTriggerEnter(Collider other) 
   {

      if (transitionType[0])
      {
        if (other.CompareTag("Player"))
        {
          rotPlayer.canMove = false;
          rotPlayer.canGravity = false;

          rotPlayer.enabled = false;

          moveToNextPoint = false;
          canFollowPath = true;

          StartCoroutine(TimeToTeleport(teleportTimeStairs));
        }
      }
      else if (transitionType[1])
      {
        if (other.CompareTag("Player"))
        {
          rotPlayer.canMove = false;
          rotPlayer.canGravity = false;

          rotPlayer.enabled = false;

          StartCoroutine(TimeToTeleport(teleportTimeDirect));
        }

      }

   }

   void FollowPath(GameObject player)
   {
     
        float step = pathSpeed * Time.deltaTime; 

        float point1Distance = Vector3.Distance(player.transform.position, pathPoints[0].transform.position);

        if (!moveToNextPoint)
        {
          rotPlayer.RotateToTarget(pathPoints[0]);

          player.transform.position = Vector3.MoveTowards(player.transform.position, pathPoints[0].transform.position, step);
        }
        else
        {
          rotPlayer.RotateToTarget(pathPoints[1]);

          player.transform.position = Vector3.MoveTowards(player.transform.position, pathPoints[1].transform.position, step);
        }

        if (point1Distance < 1)
        {
          moveToNextPoint = true;
        }
   }

   private IEnumerator TimeToTeleport(float levelLoadDelay)
   {

    yield return new WaitForSeconds(levelLoadDelay);

    LevelInstace.currentInstance = instance;
    SceneManager.LoadScene(targetSceneName);
   }

   private IEnumerator WaitToMove()
   {
      Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

      playerPosition.transform.rotation = spawnPoint.transform.rotation;

      Debug.Log("I made it here");

      yield return new WaitForSeconds(1f);

      player = GameObject.FindGameObjectWithTag("Player");

      rotPlayer.enabled = true;
      rotPlayer.canMove = true;
      rotPlayer.canGravity = true;
   }
}
