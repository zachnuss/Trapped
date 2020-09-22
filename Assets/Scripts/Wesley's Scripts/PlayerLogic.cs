//Wesley Morrison
//9/11/20

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public int maxHealth; //Max health
    public int currentHealth; //Maybe change to private and use public functions to change instead?
    public float speed; //Float for fine tuning
    public int damage; //Player damage value -> 10% chance to crit should be implemented in the function that applies damage
    static int points = 0; //Should change to private, make only accessable through functions
    public int gold = 0; //Should change to private, make only accessable through functions

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
