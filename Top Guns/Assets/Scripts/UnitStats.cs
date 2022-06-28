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
    public int currentShield;
    public int maxShield;

    [SerializeField]
    private GameObject drop;

    public Stats stats;
    private GameHandler gameHandler;
    [SerializeField]
    public HealthBar hpBar;
    [SerializeField]
    public HealthBar shieldBar;

    public bool hasShield;
    public bool hasBloodLust;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        hpBar.SetMaxPoints(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();   
    }

    public void TakeDamage(int damage)
    {
        if (gameObject.tag == "Player")
        {
            if (hasShield == false)
            {
                CurrentHealth -= damage;
                hpBar.SetPoints(CurrentHealth);
            }
            if (hasShield == true)
            {
                currentShield -= damage;
                shieldBar.SetPoints(currentShield);
                if (currentShield <= 0)
                {
                    gameHandler.uI.ShieldUi.SetActive(false);
                    gameHandler.uI.healthUi.SetActive(true);
                    hasShield = false;
                }
            }
        }
        if (gameObject.tag == "Enemy")
        {
            CurrentHealth -= damage;
            hpBar.SetPoints(CurrentHealth);
        }
        
    }

    public void RecieveHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth>MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        hpBar.SetPoints(CurrentHealth);
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
                Instantiate(drop, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
