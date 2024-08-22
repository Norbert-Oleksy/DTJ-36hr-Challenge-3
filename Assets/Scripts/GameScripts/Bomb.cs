using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    #region Singleton
    public static Bomb instance;
    #endregion
    #region Fields
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerTextS;
    [SerializeField] private TextMeshProUGUI timerTextMS;
    [SerializeField] private TextMeshProUGUI charText;
    [Space]
    [Header("Bomb")]
    [SerializeField] private List<GameObject> moduleSlots;
    [SerializeField] private Modules modulesList;
    [Space]
    [Header("Test")]
    [SerializeField, Range(0, 6)] private int testDifficulty = 0;
    #endregion

    #region Variables
    private List<Module> modules;
    private float timeLeft=60f;
    #endregion

    #region Logic
    private void TimerLogic()
    {
        timeLeft-=Time.deltaTime;
        if (timeLeft <= 30.0f && timeLeft > 10.0f && timerTextS.color != Color.yellow)
        {
            timerTextS.color = Color.yellow;
            timerTextMS.color = Color.yellow;
            charText.color = Color.yellow;
        }
        else if (timeLeft <= 10.0f && timerTextS.color != Color.red)
        {
            timerTextS.color = Color.red;
            timerTextMS.color = Color.red;
            charText.color = Color.red;
        }

        //timerText.text = string.Format("{0:00}:{1:00}", (int)timeLeft, (int)((timeLeft - (int)timeLeft)*100));
        timerTextS.text = AddZeroIfBelowTen((int)timeLeft);
        timerTextMS.text = AddZeroIfBelowTen((int)((timeLeft - (int)timeLeft) * 100));
        if (timeLeft <= 0f) Detonate();
    }

    private string AddZeroIfBelowTen(int value)
    {
        if (value < 10) return "0" + value.ToString();
        return value.ToString();
    }

    public void CheckIfBombIsDefused()
    {
        if (GameManager.instance.stage != GameManager.gamestage.Game) return;

        if(modules!=null && modules.Find(e => e.active==false) !=null) return;

        GameManager.instance.Victory();
    }

    public void Detonate()
    {
        GameManager.instance.GameOver();
    }

    public void SetABomb()
    {
        if (instance != null && instance != this) return;
        instance = this;
        //timerTextS.text = string.Format("{0:0}:{1:00}", 60,0);
        timerTextS.text = "60";
        timerTextMS.text = "00";
        modules = new List<Module>();
        int numberOfModules = GetNumberOfModules();
        if (numberOfModules >= 5) {
            numberOfModules--;
            PlaceModule(modulesList.hardModules[Random.Range(0, modulesList.hardModules.Count)]);
        }

        if(numberOfModules >= 3)
        {
            int variable = (numberOfModules - 3 < 2) ? 1 : 2;
            numberOfModules -= variable;
            for(int i = 0; i < variable; i++) 
            {
                PlaceModule(modulesList.impediments[Random.Range(0, modulesList.impediments.Count)]);
            }
        }

        for (int i = 0; i < numberOfModules; i++) {
            PlaceModule(modulesList.basic[Random.Range(0, modulesList.basic.Count)]);
        }
        GameManager.instance.stage = GameManager.gamestage.Game;
    }

    private void PlaceModule(GameObject md)
    {
        var slot = moduleSlots[Random.Range(0, moduleSlots.Count - 1)];
        moduleSlots.Remove(slot);
        slot.GetComponent<Image>().enabled = false;

        GameObject newObject = Instantiate(md, Vector3.zero, Quaternion.identity);
        newObject.transform.SetParent(slot.transform);
        newObject.transform.localPosition = Vector3.zero;
        modules.Add(newObject.GetComponent<Module>());
    }

    private int GetNumberOfModules()
    {
        if (testDifficulty > 0) return testDifficulty;

        switch (DataManager.instance.difficulty)
        {
            case 1:
                return Random.Range(1,2);
            case 2:
                return Random.Range(3, 4);
            case 3: 
                return Random.Range(5, 6);
            default:
                return 1;
        }
    }

    public string ReturnModuleInfoID(int position)
    {
        return moduleSlots[position].GetComponent<Module>().moduleInfoID;
    }
    #endregion

    #region Unity-API

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.stage != GameManager.gamestage.Game) return;

        TimerLogic();
    }
    #endregion
}
