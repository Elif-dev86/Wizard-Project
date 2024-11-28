using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPushCheck : MonoBehaviour
{
    public Animator anim;

    public bool isPressed;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

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

        anim.SetBool("isPushed", true);

        yield return new WaitForSeconds(2.5f);

        isPressed = true;
    }
}
