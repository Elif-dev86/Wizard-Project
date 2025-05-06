using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : Door
{
    private Animator anim;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        anim.SetBool("isOpen", isOpen); 
    }
}
