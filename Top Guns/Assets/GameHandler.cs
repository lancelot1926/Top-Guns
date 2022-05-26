using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnLoc;
    [SerializeField]
    private GameObject NormalSoldier;
    
    [SerializeField]
    private List<GameObject> SpwanedSoldierList;
    
    public int LevelCounter;
    public int PointCounter;
    public int EnemyCounter;
    private enum State
    {
        StartOfTheLevel,
        EndOfTheLevel,
        InLevel,
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        EnemyCounter = SpwanedSoldierList.Count;


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
}
