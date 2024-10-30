using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyAttack", menuName = "Enemies/EnemyAttack")]
public class EnemyAttack : ScriptableObject
{
    public string enemyName;

    public int enemyDamage;
}
