using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public NormalDoor door;

    public GameObject[] levers;

    public GameObject[] pillars;

    public GameObject switche;

    public bool isKeyOnly;

    public bool isOnlyLevers;

    public bool isOnlySwitches;

    public bool isBoth;

    public bool isPillarOnly;
    
    private bool[] mechanismType;
    public int CheckPullCount;

    public int pillaActiveCount;

    //public int CheckSwitchCount;

    void Start()
    {
        mechanismType = new bool[] {isOnlyLevers, isOnlySwitches, isBoth, isPillarOnly};
    }

    void FixedUpdate()
    {
        if (mechanismType[0])
        {
            if (CheckPullCount == levers.Length)
            {
                door.isOpen = true;
            }
        }
        else if (mechanismType[1])
        {
            SwitchPushCheck pressCheck = switche.GetComponent<SwitchPushCheck>();

            if (pressCheck.isPressed == true)
            {
                door.isOpen = true;
            }
        }
        else if (mechanismType[2])
        {
            // TODO
        }
        else if (mechanismType[3])
        {
            if (pillaActiveCount == pillars.Length)
            {
                door.isOpen = true;
            }
        }
        else
        {
            return;
        }

    }
}
