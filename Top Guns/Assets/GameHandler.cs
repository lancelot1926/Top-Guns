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
    
    
    public List<GameObject> SpwanedSoldierList;
    
    public int LevelCounter;
    public int PointCounter;
    public int EnemyCounter;

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
        LevelCounter = 1;
        state = State.Wait;
    }

    
    void Update()
    {
        Debug.Log(state);
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


    IEnumerator EventDelayer(float delay, Action onActionComplete)
    {
        yield return new WaitForSeconds(delay);
        onActionComplete();
    }
}
