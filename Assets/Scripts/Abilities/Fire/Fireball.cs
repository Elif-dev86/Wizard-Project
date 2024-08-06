using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 40;

    public float attackInstanceTime = 0;

    public GameObject target;

    public GameObject contact;

    public ParticleSystem particle;

    private Light glow;

    [SerializeField]
    private MeshRenderer meshRenderer;

    private Vector3 direction;


    void Start()
    {
        target = TargetManager.GetLastTarget();

        glow = GetComponent<Light>();

        if (target != null)
        {
            // Calculate direction to look at the target
            direction = (target.transform.position - this.gameObject.transform.position).normalized;
        }

        particle = GetComponent<ParticleSystem>();

        meshRenderer = GetComponent<MeshRenderer>();

        contact.SetActive(false);
        
    }

    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            var main = particle.main;

            direction = new Vector3(0, 0, 0);

            glow.enabled = false;

            main.startLifetime = 0f;

            meshRenderer.enabled = false;

            contact.SetActive(true);

            Destroy(this.gameObject, attackInstanceTime);
        }
    }
}
