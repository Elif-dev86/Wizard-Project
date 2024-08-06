using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ThinSpark : MonoBehaviour
{

    public GameObject target;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 37;

        this.transform.position = targetPosition;

        Destroy(this.gameObject, 5);
    }
}
