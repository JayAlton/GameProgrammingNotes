using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Private variable to be assigned in the inspector with what enemy prefab to spawn
    [SerializeField] GameObject enemyPrefab;

    // Private variable containing a reference to the enemy instance in the scene
    private GameObject enemy;
    private int activeEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If there isn't an enemy, spawn one
        if (enemy == null) {
            enemy= Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(-96, 27, 40);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0,angle,0);
        }    
    }
    public void enemyKill() {
        activeEnemies--;
    }
}
