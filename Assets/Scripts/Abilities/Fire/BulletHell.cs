using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    public GameObject target;

    
    void Start()
    {
        target = TargetManager.GetLastTarget();
    }

    
    void Update()
    {
        
    }
}
