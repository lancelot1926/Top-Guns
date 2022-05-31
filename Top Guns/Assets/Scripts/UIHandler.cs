using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private GameHandler gmHandler;
    [SerializeField]
    private GameObject buffPickPanel;


    public GameObject healthUi;
    public GameObject ShieldUi;


    public Text enemyCounter;
    public Text pointCounter;
    public Text levelCounter;
    public Text coinCounter;

    public Button BuffHolderOne;
    public Button BuffHolderTwo;
    public Button BuffHolderThree;


    Image buffOne; 
    Image buffTwo;
    Image buffThree;

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

    public void SetBuffS() 
    {
        buffPickPanel.SetActive(true);
        if (buffOne == null)
        {
            buffOne = Instantiate(gmHandler.BuffList[0].logo, BuffHolderOne.transform);
            buffTwo = Instantiate(gmHandler.BuffList[1].logo, BuffHolderTwo.transform);
            buffThree = Instantiate(gmHandler.BuffList[2].logo, BuffHolderThree.transform);
        }
        
        /*if (gmHandler.BuffList.Count>1)
        {
            
        }*/

        /*if (gmHandler.BuffList.Count <1&&buffOne!=null )
        {
            Destroy(buffOne);
            Destroy(buffTwo);
            Destroy(buffThree);
        }*/
    }
    public void RemoveBuffs()
    {
        Destroy(buffOne);
        Destroy(buffTwo);
        Destroy(buffThree);
    }
    public void setButtonsForBuffs()
    {
        BuffHolderOne.onClick.AddListener(() => {
            gmHandler.chosenBuff = gmHandler.BuffList[0];
            gmHandler.ApplyBuff();
            gmHandler.BuffList.Clear();
            RemoveBuffs();
            buffPickPanel.SetActive(false);
            

        });
        BuffHolderTwo.onClick.AddListener(() => {
            gmHandler.chosenBuff = gmHandler.BuffList[1];
            gmHandler.ApplyBuff();
            gmHandler.BuffList.Clear();
            RemoveBuffs();
            buffPickPanel.SetActive(false);

        });
        BuffHolderThree.onClick.AddListener(() => {
            gmHandler.chosenBuff = gmHandler.BuffList[2];
            gmHandler.ApplyBuff();
            gmHandler.BuffList.Clear();
            RemoveBuffs();
            buffPickPanel.SetActive(false);

        });
    }
}
