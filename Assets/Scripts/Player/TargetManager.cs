using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static List<GameObject> targets = new List<GameObject>();

    public static void AddTarget(GameObject target)
    {
        targets.Add(target);
    }

    public static GameObject GetLastTarget()
    {
        if (targets.Count > 0)
        {
            return targets[targets.Count - 1];
        }
        return null;
    }
}
