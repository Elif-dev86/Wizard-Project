using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RottenSpit : MonoBehaviour
{
    private GameObject attackSpawn;

    private GameObject target;

    private Vector3 direction;

    public ParticleSystem particle;

    public GameObject contact;

    public float attackInstanceTime = 0;

    [SerializeField]
    private MeshRenderer meshRenderer;

    public AnimationCurve curve;

    [SerializeField] private float duration = 1.0f;

    [SerializeField] private float maxHeightY = 3.0f;

    void Start()
    {
        attackSpawn = GameObject.FindGameObjectWithTag("attackSpawn");

        target = GameObject.FindGameObjectWithTag("attackTarget");

        particle = GetComponent<ParticleSystem>();

        meshRenderer = GetComponentInChildren<MeshRenderer>();

        contact.SetActive(false);

        if (attackSpawn != null)
        {
            // Calculate direction to look at the target
            direction = transform.rotation * attackSpawn.transform.forward;

            this.transform.rotation = Quaternion.LookRotation(-direction);

            Destroy(this.gameObject, 7f);
        }


        StartCoroutine(Curve());

    }

    public IEnumerator Curve()
    {
        var timePast = 0f;

        Vector3 targetStart = attackSpawn.transform.position;

        Vector3 targetEnd = target.transform.position;

        //temp vars
        while (timePast < duration)
        {
            timePast += Time.deltaTime;

            var linearTime = timePast / duration; //0 to 1 time
            var heightTime = curve.Evaluate(linearTime); //value from curve

            var height = Mathf.Lerp(0f, maxHeightY, heightTime); //clamped between the max height and 0

            transform.position =
                Vector3.Lerp(targetStart, targetEnd, linearTime) + new Vector3(0f, height, 0f); //adding values on y axis

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision != null)
        {
            var main = particle.main;

            contact.SetActive(true);

            meshRenderer.enabled = false;

            main.startLifetime = 0f;

            Destroy(this.gameObject, attackInstanceTime);

        }
    }
}

