using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{
    
    [SerializeField]
    private List<Transform> SpawnLoc;
    [SerializeField]
    private GameObject NormalSoldier;
    [SerializeField]
    public UIHandler uI;

    private GameObject player;
    
    public List<GameObject> SpwanedSoldierList;
    
    public int LevelCounter;
    public int PointCounter;
    public int EnemyCounter;
    public int CoinCounter;


    public List<Buff> BuffList;
    public List<Buff> buffPool;
    public List<Buff> nonRepeatingBuffs;
    public Buff chosenBuff;

    private bool funcCheck1;
    private State state;
    private enum State
    {
        Wait,
        StartOfTheLevel,
        EndOfTheLevel,
        InLevel,
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LevelCounter = 1;
        state = State.Wait;
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            BuffRandomizer();
            uI.SetBuffS();
            uI.setButtonsForBuffs();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            BuffList.Clear();
            uI.RemoveBuffs();
        }
        //Debug.Log(state);
        EnemyCounter = SpwanedSoldierList.Count;
        switch (state)
        {
            case State.Wait:
                StartCoroutine(EventDelayer(5f, () => {
                    state = State.StartOfTheLevel;
                }));
                break;
            case State.StartOfTheLevel:
                if (funcCheck1 == false)
                {
                    SpawnEnemy();
                    funcCheck1 = true;
                }
                
                state = State.InLevel;
                break;
            case State.InLevel:
                if (EnemyCounter == 0)
                {
                    LevelCounter++;
                    state = State.EndOfTheLevel;
                }
                
                break;
            case State.EndOfTheLevel:
                funcCheck1 = false;
                state = State.Wait;
                break;
        }

    }

    private void SpawnEnemy() 
    {
        switch (LevelCounter)
        {
            case 1:
                //2
                for(int x = 0; x< 2; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 2:
                //5
                for (int x = 0; x < 5; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 3:
                //8
                for (int x = 0; x < 8; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 4:
                //11
                for (int x = 0; x < 11; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 5:
                //15
                for (int x = 0; x < 15; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 6:
                //20
                for (int x = 0; x < 20; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 7:
                //25
                for (int x = 0; x < 25; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
            case 8:
                //30
                for (int x = 0; x < 30; x++)
                {
                    GameObject spawnedSoldier = Instantiate(NormalSoldier, SpawnLoc[x]);
                    SpwanedSoldierList.Add(spawnedSoldier);
                }
                break;
        }
    }

    void BuffRandomizer()
    {
        while (BuffList.Count<3)
        {
            Buff buff = buffPool[UnityEngine.Random.Range(0, buffPool.Count)];
            if (BuffList.Contains(buff) == false && nonRepeatingBuffs.Contains(buff) == false && BuffList.Count < 3)
            {
                BuffList.Add(buff);
            }
            else if (BuffList.Contains(buff) == true || nonRepeatingBuffs.Contains(buff) == true)
            {
                buff = buffPool[UnityEngine.Random.Range(0, buffPool.Count)];
            }
        }
        
        
    }

    public void ApplyBuff()
    {
        switch (chosenBuff.BuffName)
        {
            case "Automatic Rifle":
                player.GetComponent<ShootingCombat>().shootingStyleList.Add("Automatic");
                nonRepeatingBuffs.Add(chosenBuff);
                chosenBuff = null;
                break;

            case "Burst Rifle":
                player.GetComponent<ShootingCombat>().shootingStyleList.Add("Burst");
                nonRepeatingBuffs.Add(chosenBuff);
                chosenBuff = null;
                break;

            case "Shotgun Rifle":
                player.GetComponent<ShootingCombat>().shootingStyleList.Add("Shotgun");
                nonRepeatingBuffs.Add(chosenBuff);
                chosenBuff = null;
                break;

            case "Sniper Rifle":
                player.GetComponent<ShootingCombat>().shootingStyleList.Add("Sniper");
                nonRepeatingBuffs.Add(chosenBuff);
                chosenBuff = null;
                break;

            case "Fire Rate Upgrade":
                
                player.GetComponent<ShootingCombat>().fireRateCdr += 0.1f;
                chosenBuff.buffLevel += 1;
                if (chosenBuff.buffLevel==3)
                {
                    nonRepeatingBuffs.Add(chosenBuff);
                    
                }
                chosenBuff = null;
                break;
            case "Coin Bag":

                break;
            case "Move Speed Buff":
                player.GetComponent<PlayerMovement>().moveSpeed += 1;
                chosenBuff = null;
                break;
            case "Max HP Upgrade":
                player.GetComponent<UnitStats>().MaxHealth += 50;
                player.GetComponent<UnitStats>().CurrentHealth = player.GetComponent<UnitStats>().MaxHealth;
                player.GetComponent<UnitStats>().hpBar.SetMaxPoints(player.GetComponent<UnitStats>().MaxHealth);
                chosenBuff = null;
                break;
            case "Temporary Shield":
                uI.healthUi.SetActive(false);
                uI.ShieldUi.SetActive(true);
                player.GetComponent<UnitStats>().hasShield = true;
                player.GetComponent<UnitStats>().maxShield = 100;
                player.GetComponent<UnitStats>().currentShield = player.GetComponent<UnitStats>().maxShield;
                player.GetComponent<UnitStats>().shieldBar.SetMaxPoints(player.GetComponent<UnitStats>().maxShield);
                break;
        }
    }

    IEnumerator EventDelayer(float delay, Action onActionComplete)
    {
        yield return new WaitForSeconds(delay);
        onActionComplete();
    }
}
