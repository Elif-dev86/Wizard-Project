using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartThorns : MonoBehaviour
{
     private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = -0.2f;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 11.02f);
    }
}
