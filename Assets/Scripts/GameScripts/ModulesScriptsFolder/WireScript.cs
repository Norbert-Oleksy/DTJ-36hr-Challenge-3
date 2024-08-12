using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireScript : MonoBehaviour
{
    private WiresModule parent;
    [SerializeField] private Image img;
    [SerializeField] private Button btn;
    public void Initialization(WiresModule p, Color c)
    {
        parent = p;
        img.color = c;
    }

    public void Logic()
    {
        if (GameManager.instance.stage != GameManager.gamestage.Game) return;
        btn.interactable = false;
        parent.CheckWires(this);
    }
}
