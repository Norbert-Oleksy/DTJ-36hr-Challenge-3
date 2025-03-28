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
        timerTextS.text = "60";
        timerTextMS.text = "00";
        modules = new List<Module>();
        List<GameObject> slots = new List<GameObject>(moduleSlots);
        int numberOfModules = GetNumberOfModules();

        //Only 1 module for hard mode
        if (numberOfModules >= 5) {
            numberOfModules--;
            PlaceModule(modulesList.hardModules[Random.Range(0, modulesList.hardModules.Count)], slots);
        }

        int index;

        // 1 or 2 for normal and hard
        if (numberOfModules >= 3)
        {
            int variable = (numberOfModules - 3 < 2) ? 1 : Random.Range(1,3);
            numberOfModules -= variable;
            List<GameObject> listImpediments = new List<GameObject>(modulesList.impediments);
            
            for (int i = 0; i < variable; i++)
            {
                index = Random.Range(0, listImpediments.Count);
                PlaceModule(listImpediments[index], slots);
                listImpediments.RemoveAt(index);
            }
        }

        List<GameObject> listBasic = new List<GameObject>(modulesList.basic);
        // 1 or 2 for easy mode, 2 or 4 for normal and hard
        for (int i = 0; i < numberOfModules; i++) {
            index = Random.Range(0, listBasic.Count);
            PlaceModule(listBasic[index], slots);
            listBasic.RemoveAt(index);
        }
        GameManager.instance.stage = GameManager.gamestage.Game;
    }

    private void PlaceModule(GameObject md, List<GameObject> slots)
    {
        var slot = slots[Random.Range(0, slots.Count)];
        slots.Remove(slot);
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
                return Random.Range(1,3);
            case 2:
                return Random.Range(3, 5);
            case 3: 
                return Random.Range(5, 7);
            default:
                return 1;
        }
    }

    public string ReturnModuleInfoID(int position)
    {
        Module module = moduleSlots[position].GetComponentInChildren<Module>();
        return module!=null? module.moduleInfoID : "";
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
