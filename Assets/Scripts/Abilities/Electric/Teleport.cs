using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("attackTarget");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 40;

        this.transform.position = targetPosition;

        StartCoroutine(TeleportTime(player, targetPosition));

        Destroy(this.gameObject, 4);
    }

    private IEnumerator TeleportTime(GameObject player, Vector3 targetPosition)
    {
        targetPosition.y = 0;

        player.GetComponent<PlayerMovement>().canGravity = false;

        yield return new WaitForSeconds(1.3f);

        player.transform.position = targetPosition;

        yield return new WaitForSeconds(.5f);

        player.GetComponent<PlayerMovement>().canGravity = true;

    }
}
