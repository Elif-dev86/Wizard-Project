using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHitCheck : MonoBehaviour
{
    public GameObject fireParticle;

    public bool isActivated;

    public bool isFire, isElectric, isPoison;

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.CompareTag("attack"))
        {
            SpellHolder spellHolder =  other.gameObject.GetComponent<SpellHolder>();

            if (isFire)
            {
                if (spellHolder.spell.spellType == "Fire")
                {
                    DoorManager doorManager = FindObjectOfType<DoorManager>();

                    isActivated = true;

                    fireParticle.SetActive(true);

                    StartCoroutine(ActivateTime(doorManager));
                }
            }
            else if (isElectric)
            {

            }
            else if (isPoison)
            {

            }

        }
        else
        {
            return;
        }
        
    }

    private IEnumerator ActivateTime(DoorManager doorManager)
    {
        yield return new WaitForSeconds(.8f);

        doorManager.pillaActiveCount++;
    }
}
