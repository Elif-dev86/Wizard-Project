using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject pathPoint;

    [SerializeField]
    float pathSpeed;

    float distance;

    GameObject player;

    PlayerMovement rotPlayer;

    [SerializeField]
    Skeleton_Boss bossObject;

    [SerializeField]
    Animator camAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            rotPlayer = other.GetComponent<PlayerMovement>();
            bossObject = GameObject.FindObjectOfType<Skeleton_Boss>();

            distance = Vector3.Distance(player.transform.position, pathPoint.transform.position);

            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath()
    {
        rotPlayer.enabled = false;

        while(distance > 0)
        {

            distance = Vector3.Distance(player.transform.position, pathPoint.transform.position);

            float step = pathSpeed * Time.deltaTime; 

            rotPlayer.RotateToTarget(pathPoint);

            player.transform.position = Vector3.MoveTowards(player.transform.position, pathPoint.transform.position, step);

            yield return null;

        }

        StartCoroutine(ActivateBoss());
    }

    private IEnumerator ActivateBoss()
    {
        yield return null;

        camAnim.SetBool("zoomOut", true);

        bossObject.ActivateBattle();

        yield return new WaitForSeconds(3);

        bossObject.bossHealthSlider.gameObject.SetActive(true);

        rotPlayer.enabled = true;
    }

}
