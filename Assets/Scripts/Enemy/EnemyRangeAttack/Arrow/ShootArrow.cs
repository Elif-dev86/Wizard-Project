using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{

    public GameObject arrowPrefab;

    public GameObject spawner;

    public void InstantiateArrow()
    {
        GameObject arrowObj = Instantiate(arrowPrefab);

        arrowObj.transform.position = spawner.transform.position;
    }

}
