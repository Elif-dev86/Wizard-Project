using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/LevelInstance")]
public class LevelInstace : ScriptableObject
{
    public static LevelInstace currentInstance { get; set; }
}
