using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToCombat : MonoBehaviour
{
    //public OverworldEnemyManager _manager;
    public OverworldEnemy overworldEnemy;
    public SceneLoader sceneLoader;
    
    //public Vector2 distance;
    public Vector2 position;
    public Transform player;
    public float speed;

    void Awake()
    {
        speed = overworldEnemy.speed;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        float  distance = Vector2.Distance(transform.position, player.position);

        if (other.tag == "Player")
        {

            Debug.Log("Chase player");
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
            if(distance < 0.2)
            {
                Debug.Log("init combat scene");
                sceneLoader.LoadNextLevel();
            }

        }
    }

  
}
