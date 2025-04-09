using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
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

    public bool isCode;

    public int[] leverCode = {0, 0, 0, 0};

    public int[] leversPulled = {0, 0, 0, 0};
    
    private bool[] mechanismType;

    [Space(20)]
    public int CheckPullCount;

    public int pillaActiveCount;

    public List<int> code = new List<int>();

    //public int CheckSwitchCount;

    void Start()
    {
        mechanismType = new bool[] {isOnlyLevers, isOnlySwitches, isBoth, isPillarOnly, isCode};
    }

    void LateUpdate()
    {
        if (mechanismType[0])
        {
            //Debug.Log(levers.Length);

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
        else if (mechanismType[4])
        {
            for (int i = 0; i < levers.Length; i++)
            {
                LeverPullCheck pullCheck = levers[i].GetComponent<LeverPullCheck>();

                if (pullCheck.isPulled == true)
                {
                    leversPulled[i] = 1;
                }
                else
                {
                    leversPulled[i] = 0;
                }

                if (IsCodeCorrect())
                {
                    door.isOpen = true;
                }
                
            }

            
        }
        else
        {
            return;
        }

    }

    bool IsCodeCorrect()
    {
        for (int i = 0; i < leverCode.Length; i++)
        {
            if (leversPulled[i] != leverCode[i])
            {
                return false;
            }
        }
        return true;
    }
}
