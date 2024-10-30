using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 10;

    public float yAmount;

    public float smooth;

    public float yHeight = 0;

    private float yRot;

    public GameObject target;

    private Vector3 direction;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        target = TargetManager.GetLastTarget();

        Vector3 yPos = this.transform.position;

        yPos.y = yHeight;

        this.transform.position = yPos;

        if (target != null)
        {
            // Calculate direction to look at the target
            direction = (target.transform.position - this.gameObject.transform.position).normalized;
        }

        direction.y = 0;

        Destroy(this.gameObject, 14f);
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void FixedUpdate() 
    {

        yRot -= yAmount;

        Quaternion startRotation = Quaternion.Euler(0, yRot, 0);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, startRotation, Time.deltaTime * smooth);
        
    }
}
