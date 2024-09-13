using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseBlood : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 0.01f;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 20.13f);
    }
}
