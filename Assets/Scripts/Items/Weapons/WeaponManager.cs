using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string weaponName;

    public GameObject staffObject;

    public void EquiptWeapon()
    {
        GameObject staffHand;
        staffHand = GameObject.FindGameObjectWithTag("staffSpawn");

        Transform staffTarget = staffHand.transform;

        if (staffTarget.CompareTag("staffSpawn"))
        {
            GameObject currentStaff = staffTarget.GetChild(0).gameObject;

            if (staffTarget.GetChild(0) != null)
            {
                Destroy(currentStaff);

                InstantiateStaff(staffTarget);
            }
            else
            {
                InstantiateStaff(staffTarget);
            }
        }
        
    }

    private void InstantiateStaff(Transform staffTarget)
    {
        GameObject newWeapon = Instantiate(staffObject, staffTarget);

        newWeapon.transform.localPosition = Vector3.zero;
    }
}
