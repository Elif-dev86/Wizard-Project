using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float speed = 0;

    private Vector3 direction;

    void Start()
    {
        GameObject target = GameObject.FindWithTag("playerTarget");

        if (target != null)
        {
            // Calculate direction to look at the target
            direction = (target.transform.position - this.gameObject.transform.position).normalized;
            
            this.transform.rotation = Quaternion.LookRotation(-direction);
        }
    }

    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Destroy(this.gameObject);
        }
    }
    
}
