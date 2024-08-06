using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelector : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;

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
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
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
            }
        }
        
        return highlight.CompareTag(thisTag);
    }

}

