using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int lvl;
    public int damage;
    public float moveSpeed;
    public float fireRate;
    public int CurrentHealth;
    public int MaxHealth;


    public Stats stats;
    private GameHandler gameHandler;
    [SerializeField]
    private HealthBar hpBar;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        hpBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();   
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        hpBar.SetHealth(CurrentHealth);
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
            if (gameObject.tag == "Enemy")
            {
                gameHandler.SpwanedSoldierList.Remove(gameObject);
                gameHandler.PointCounter += 50;
                Destroy(gameObject);
            }
        }
    }
}
