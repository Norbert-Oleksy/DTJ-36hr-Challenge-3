using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WiresModule : Module
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private GameObject wiresSection;
    private List<WireScript> wireList = new List<WireScript>();
    private Color[] colorsList = new Color[6] { Color.red, new Color(1.0f, 0.64f, 0.0f), Color.yellow, Color.green, Color.blue, new Color(143, 0, 254) };
    private void RenderWires()
    {
        for (int i = 0; i < DataManager.instance.difficulty * 2; i++)
        {
            GameObject newObject = Instantiate(wirePrefab, Vector3.zero, Quaternion.identity);
            newObject.transform.SetParent(wiresSection.transform);
            newObject.transform.localPosition = Vector3.zero;
            wireList.Add(newObject.GetComponent<WireScript>());
        }
        wireList = ShuffleList(wireList);

        for (int y = 0; y < wireList.Count; y++)
        {
            wireList[y].Initialization(this, colorsList[y]);
        }
    }

    public void CheckWires(WireScript ws)
    {
        if(active)return;
        if (wireList[0] != ws)Bomb.instance.Detonate();

        wireList.Remove(ws);
        if (wireList.Count<=0)
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
