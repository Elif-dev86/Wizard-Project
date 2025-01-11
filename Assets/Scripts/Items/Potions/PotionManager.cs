using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public string potionName;

    public int healAmount;

    //[HideInInspector]
    public int itemStack = 1;

    PlayerMovement pMovement;

    InventoryManagement iManagement;

    [HideInInspector]
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        pMovement = FindObjectOfType<PlayerMovement>();
        iManagement = FindObjectOfType<InventoryManagement>();
        textMeshPro = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void TakeHeal()
    {
        if (pMovement.healtBar.value < pMovement.maxHealth)
        {

            if (itemStack > 1)
            {
                pMovement.healtBar.value += healAmount;
                itemStack -= 1;
                textMeshPro.text = itemStack.ToString();
            }
            else
            {
                textMeshPro.enabled = false;
                iManagement.canStack = false;
                Destroy(this.gameObject);
                return;
            }

        }
    }
}
