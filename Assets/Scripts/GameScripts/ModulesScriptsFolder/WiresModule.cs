using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresModule : Module
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private GameObject wiresSection;
    private int tymczasowe = 0;
    private void RenderWires()
    {
        for (int i = 0; i < DataManager.instance.difficulty * 2; i++)
        {
            GameObject newObject = Instantiate(wirePrefab, Vector3.zero, Quaternion.identity);
            newObject.transform.SetParent(wiresSection.transform);
            newObject.transform.localPosition = Vector3.zero;
            newObject.GetComponent<WireScript>().Initialization(this);
        }
    }

    public void CheckWires()
    {
        if(active)return;
        tymczasowe++;
        if (tymczasowe >= DataManager.instance.difficulty * 2)
        {
            active = true;
            Bomb.instance.CheckIfBombIsDefused();
        }
    }


    private void Awake()
    {
        RenderWires();
    }
}
