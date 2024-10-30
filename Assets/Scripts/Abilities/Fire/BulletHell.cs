using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    private float speed = 15;

    public GameObject attackSpawn;

    private Vector3 direction;

    void Start()
    {
        attackSpawn = GameObject.FindGameObjectWithTag("attackSpawn");

        if (attackSpawn != null)
        {
            // Calculate direction to look at the target
            direction = transform.rotation * attackSpawn.transform.forward;

            direction.y = 0;

            Destroy(this.gameObject, 3f);
        }
    }

    
    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }
}
