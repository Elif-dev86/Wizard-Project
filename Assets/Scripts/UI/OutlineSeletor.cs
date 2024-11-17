using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelector : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;

    public bool canSelectEnemy;

    private string thisTag = "book";

    public string[] highlightType = { "book", "potion", "weapon" };

    void FixedUpdate()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;

            if (TagsToCompare(highlight))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                }
                
            }
            else
            {
                highlight = null;
            }

        }

        

    }

    private bool TagsToCompare(Transform highlight)
    {
        if (highlight != null)
        {
            for (int i = 0; i < highlightType.Length; i++)
            {
                if (highlight.tag == highlightType[i])
                {
                    thisTag = highlightType[i];
                }

                /*if (highlight.tag == highlightType[5])
                {
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.red;
                }*/
            }
        }
        
        return highlight.CompareTag(thisTag);
    }

}

