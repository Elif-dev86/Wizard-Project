using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class QuaternionsSpell : MonoBehaviour
{

    Transform spellObject;

    public GameObject objectToMove;

    public GameObject objectToRotate;

    private GameObject target;

    private Vector3 direction;

    public float speed = 10f;

    public float height = 5f;

    public float progress = 0f;
    
    private Vector3 startPoint;

    private Vector3 endPoint;

    public float distance;

    public float secondsToDelete;

    public float secondsToReset;

    public bool canLevitate;

    public int xAmount = 0;
    public int yAmount = 0;
    public int zAmount = 0;

    private int xRot;
    private int yRot;
    private int zRot;

    public int smooth = 5;
    
    void Start()
    {

        target = TargetManager.GetLastTarget();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        objectToMove = player;

        objectToRotate = player.transform.GetChild(0).gameObject;

        spellObject = this.transform;

        spellObject.SetParent(objectToMove.transform);

        xRot = 0;
        yRot = 0;
        zRot = 0;

        if (target != null)
        {
            // Calculate direction to look at the target
            direction = (target.transform.position - this.gameObject.transform.position).normalized;

            startPoint = objectToMove.transform.position;

            endPoint = target.transform.position;

            distance = Vector3.Distance(startPoint, endPoint);
        }

        StartCoroutine(WaitUntilLevitation());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (progress < 1f && canLevitate == true)
        {
            // Update progress based on speed and distance
            progress += Time.deltaTime * speed / distance;

            // Move object in a straight line between start and target points
            Vector3 horizontalPosition = Vector3.Lerp(startPoint, endPoint, progress);

            // Calculate height for the parabola
            float parabolicHeight = height * Mathf.Sin(Mathf.PI * progress);

            // Apply the parabola to the vertical position
            Vector3 currentPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + parabolicHeight, horizontalPosition.z);

            // Move the object to the calculated position
            objectToMove.transform.position = currentPosition;

        }

        if (progress > 0.85f)
        {

            objectToRotate.transform.rotation = Quaternion.Slerp(objectToRotate.transform.rotation, objectToMove.transform.rotation, Time.deltaTime * secondsToReset);

        }
        else if (canLevitate == true)
        {

            xRot += xAmount;
            yRot += yAmount;
            zRot += zAmount;

            Quaternion startRotation = Quaternion.Euler(xRot, yRot, zRot);

            objectToRotate.transform.rotation = Quaternion.Slerp(objectToRotate.transform.rotation, startRotation, Time.deltaTime * smooth);
        
        }

        if (progress >= 1f)
        {
            StartCoroutine(WaitUntilDestroy());

            canLevitate = false;
        }
        
    }

    private IEnumerator WaitUntilLevitation()
    {
        canLevitate = false;

        yield return new WaitForSeconds(1);

        PlayerMovement pMovement = objectToMove.GetComponent<PlayerMovement>();

        pMovement.canMove = false;

        canLevitate = true;
    }

    private IEnumerator WaitUntilDestroy()
    {

        yield return new WaitForSeconds(secondsToDelete);

        PlayerMovement pMovement = objectToMove.GetComponent<PlayerMovement>();

        pMovement.canMove = true;

        objectToRotate.transform.rotation = Quaternion.Slerp(objectToRotate.transform.rotation, objectToMove.transform.rotation, 1);

        Destroy(this.gameObject);
    }
}
