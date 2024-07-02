using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoBallsModule : Module
{
    [SerializeField] private HorizontalLayoutGroup hlg;
    [SerializeField] private float speed;

    private int direction = -1;

    private void Awake()
    {
        active=true;
    }

    private void Update()
    {
        if (GameManager.instance.stage != GameManager.gamestage.Game) return;
        if (direction == -1)
        {
            hlg.spacing = hlg.spacing + (speed * direction * Time.deltaTime);
        }else if (hlg.spacing<=150) hlg.spacing = hlg.spacing + (speed* 5 * direction * Time.deltaTime);
        if(hlg.spacing <-160) Bomb.instance.Detonate();
    }

    public void BtnPress(bool value)
    {
        if(value)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }
}
