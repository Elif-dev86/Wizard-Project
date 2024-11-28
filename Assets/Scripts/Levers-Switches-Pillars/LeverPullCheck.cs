using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPullCheck : MonoBehaviour
{
    private Animator anim;

    public bool isPulled;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (!isPulled)
        {
            anim.SetBool("isDown", false);
        }
        else
        {
            anim.SetBool("isDown", true);
        }
    }
}
