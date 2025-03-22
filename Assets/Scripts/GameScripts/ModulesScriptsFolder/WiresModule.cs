using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WiresModule : Module
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private GameObject wiresSection;
    [SerializeField] private AudioEmiter audioEmiter;
    private List<WireScript> wireList = new List<WireScript>();
    private Color[] colorsList = new Color[4] { Color.red, Color.yellow, Color.green, Color.blue};
    private void RenderWires()
    {
        for (int i = 0; i < 4; i++)
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
        if (wireList[0] != ws)
        {
            Bomb.instance.Detonate();
            return;
        }

        wireList.Remove(ws);
        audioEmiter.PlayRandomClip();
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
