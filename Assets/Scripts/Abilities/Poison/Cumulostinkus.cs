using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumulostinkus : MonoBehaviour
{

    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 3;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 27);
    }
}
