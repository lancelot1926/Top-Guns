using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private GameHandler gmHandler;

    public Text enemyCounter;
    public Text pointCounter;
    public Text levelCounter;
    public Text coinCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointCounter.text = "" + gmHandler.PointCounter;
        enemyCounter.text = "" + gmHandler.EnemyCounter;
        levelCounter.text = "" + gmHandler.LevelCounter;
    }
}
