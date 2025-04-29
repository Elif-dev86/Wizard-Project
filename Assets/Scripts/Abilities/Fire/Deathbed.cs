using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathbed : MonoBehaviour
{
    public GameObject target;

    public Light glow;

    void Start()
    {
        glow.range = 0;

        target = GameObject.FindGameObjectWithTag("attackTarget");

        Vector3 targetPosition = target.transform.position;

        this.transform.position = targetPosition;

        targetPosition.y = 0.1f;

        Destroy(this.gameObject, 14);
    }

    
    void Update()
    {
        StartCoroutine(GlowTime());
    }

    IEnumerator GlowTime()
    {
        glow.range += 4 * Time.deltaTime;

        yield return new WaitForSeconds(7.5f);

        glow.range -= 9 * Time.deltaTime;
    }
}
