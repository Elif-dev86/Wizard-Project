using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impalement : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        //targetPosition.y = 0;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 4.1f);
    }
}
