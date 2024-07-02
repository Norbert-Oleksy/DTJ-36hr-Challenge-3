using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireScript : MonoBehaviour
{
    private WiresModule parent;
    [SerializeField] private Image img;
    [SerializeField] private Button btn;
    public void Initialization(WiresModule p)
    {
        parent = p;
    }

    public void Logic()
    {
        img.color = Color.green;
        btn.interactable = false;
        parent.CheckWires();
    }
}
