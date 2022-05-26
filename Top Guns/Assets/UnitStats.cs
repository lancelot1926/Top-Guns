using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public GameObject HealthBar;
    private GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void RecieveHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth>MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
    private void HealthCheck()
    {
        if (CurrentHealth <= 0)
        {
            //destroy the unit and remove it from the lists
        }
    }
}
