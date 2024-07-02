using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestModule : Module
{
    [SerializeField] private Image img;
    public void ActivateModule()
    {
        active = true;
        img.color = Color.green;
        Bomb.instance.CheckIfBombIsDefused();
    }
}
