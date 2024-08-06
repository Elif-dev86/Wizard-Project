using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class EnemyAttack : ScriptableObject
{
    public string enemyName;

    public int enemyDamage;
}
