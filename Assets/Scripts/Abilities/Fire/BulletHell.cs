using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    private float speed = 15;

    public GameObject attackSpawn;

    private Vector3 direction;

    [SerializeField]
    private GameObject core;

    [SerializeField]
    private ParticleSystem fire;

    [SerializeField]
    private ParticleSystem contact;

    void Start()
    {
        attackSpawn = GameObject.FindGameObjectWithTag("attackSpawn");

        fire.Play();
        contact.Stop();

        if (attackSpawn != null)
        {
            // Calculate direction to look at the target
            direction = transform.rotation * attackSpawn.transform.forward;

            direction.y = 0;

            //Destroy(this.gameObject, 3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            fire.Stop();
            core.SetActive(false);
            speed = 0;
            contact.Play();
            Destroy(this.gameObject, 1.2f);

        }
    }

    
    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }
}
