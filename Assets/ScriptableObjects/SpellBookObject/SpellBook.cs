using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book", menuName = "Books/book")]
public class SpellBook : ScriptableObject
{
    public string bookID;

    public string bookName;

    public string spellType;
}
