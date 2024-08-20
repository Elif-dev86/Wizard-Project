using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnedKnuckles : MonoBehaviour
{
    public GameObject target;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 9.96f;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 4);
    }
}
