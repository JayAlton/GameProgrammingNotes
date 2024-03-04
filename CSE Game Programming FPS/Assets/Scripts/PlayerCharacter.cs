using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Take damage. Print this damage to the console. 
    public void Hurt(int damage) {
        health -= damage;
        Debug.Log($"Health: {health}");
    }
}
