using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new  Enemy Class", menuName = "Overworld Enemy")]
public class OverworldEnemy : ScriptableObject
{

    public string EnemyName;
    public int health;
    public int speed;
    public bool struckFirst;
}
