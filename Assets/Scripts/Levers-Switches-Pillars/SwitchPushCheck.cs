using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPushCheck : MonoBehaviour
{
    public bool isPressed;

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivateTime());
        }    
        else
        {
            isPressed = false;
        }
    }

    private IEnumerator ActivateTime()
    {
        yield return new WaitForSeconds(.8f);

        isPressed = true;
    }
}
