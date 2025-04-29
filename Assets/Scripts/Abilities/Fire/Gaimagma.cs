using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaimagma : MonoBehaviour
{
    public GameObject target;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        this.transform.position = targetPosition;

        targetPosition.y = 0.1f;

        Destroy(this.gameObject, 10);
    }
}
