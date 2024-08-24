using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderClap : MonoBehaviour
{
    public GameObject target;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 40;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 3);
    }
}
