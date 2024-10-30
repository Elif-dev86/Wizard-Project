using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperX : MonoBehaviour
{
    public GameObject attackSpawn;

    private Vector3 direction;

    void Start()
    {
        attackSpawn = GameObject.FindGameObjectWithTag("attackSpawn");

        if (attackSpawn != null)
        {
            // Calculate direction to look at the target
            direction = transform.rotation * attackSpawn.transform.forward;

            this.transform.rotation = Quaternion.LookRotation(-direction);

            direction.y = 0;

            Destroy(this.gameObject, 7f);
        }

    }
}
