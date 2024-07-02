using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PushUPModule : Module
{
    [SerializeField] private TextMeshProUGUI display;

    private int procentage;

    private float bestValue;
    public void Logic(float value)
    {
        if(active) return;

        if(value == 0 && bestValue > 0)
        {
            procentage = procentage + (int)(bestValue * 5);
            bestValue = 0;

            if(procentage >= 100)
            {
                display.text = "100%";
                display.color = Color.green;
                active = true;
                Bomb.instance.CheckIfBombIsDefused();
            }
            else
            {
                display.text = procentage+"%";
            }
        }
        else if(value > bestValue)
        {
            bestValue = value;
        } 
    }

    private void Awake()
    {
        procentage = 0;
        bestValue = 0;
        display.text = "0%";
    }
}
