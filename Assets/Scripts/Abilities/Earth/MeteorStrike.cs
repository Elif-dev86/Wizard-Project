using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStrike : MonoBehaviour
{
    private float speed = 25;

    public float attackInstanceTime = 0;

    public GameObject target;

    public GameObject contact;

    public GameObject hitBox;

    public GameObject debri;

    public Animator anim;

    public ParticleSystem particle;

    private Light glow;

    [SerializeField]
    private MeshRenderer meshRenderer;

    private Vector3 direction;


    void Start()
    {
        target = TargetManager.GetLastTarget();

        Vector3 targetPosition = target.transform.position;

        targetPosition.y = 50;

        float xRndomAngle = Random.Range(-65f, 65f);

        targetPosition.x = target.transform.position.x + xRndomAngle;

        this.transform.position = targetPosition;

        if (target != null)
        {
            // Calculate direction to look at the target
            direction = (target.transform.position - this.gameObject.transform.position).normalized;
        }

        particle = GetComponent<ParticleSystem>();

        meshRenderer = GetComponentInChildren<MeshRenderer>();

        contact.SetActive(false);

        debri.SetActive(false);

        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            direction = new Vector3(0, 0, 0);
            
            var main = particle.main;

            contact.SetActive(true);

            debri.SetActive(true);

            meshRenderer.enabled = false;

            main.startLifetime = 0f;

            anim.SetTrigger("isHit");

            Destroy(this.gameObject, attackInstanceTime);

        }
    }
}
