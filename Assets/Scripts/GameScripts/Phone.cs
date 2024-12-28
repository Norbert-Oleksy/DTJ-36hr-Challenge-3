using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

public class Phone : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform msgContent;
    [SerializeField] GameObject msgPrefab;
    [SerializeField] GameObject modulesBtnSection;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Image scanBtnImage;
    #endregion

    #region Variables
    private bool isScanModeOn=false;
    private Color defaultScanBtnColor;
    #endregion

    #region Functions

    public void ModulesInfoBtnAction(int id)
    {
        GameObject msg = Instantiate(msgPrefab);
        msg.transform.SetParent(msgContent, false);
        LocalizeStringEvent lseMsg = msg.GetComponentInChildren<LocalizeStringEvent>();
        string stringID = Bomb.instance.ReturnModuleInfoID(id);
        if (stringID == "") stringID = "MIID_Empty";
        lseMsg.SetEntry(stringID);
        lseMsg.RefreshString();
   
        scrollbar.value = 0;
    }

    public void TipBtnAction()
    {
        GameObject msg = Instantiate(msgPrefab);
        msg.transform.SetParent(msgContent, false);
        LocalizeStringEvent lseMsg = msg.GetComponentInChildren<LocalizeStringEvent>();
        lseMsg.SetEntry("MSG_FF-" + Random.Range(1, 11));
        lseMsg.RefreshString();
        scrollbar.value = 0;
    }

    public void TurnOnOffScanMode()
    {
        isScanModeOn = !isScanModeOn;
        modulesBtnSection.SetActive(isScanModeOn);
        scanBtnImage.color = isScanModeOn? Color.red : defaultScanBtnColor;
    }

    #endregion

    #region Unity-Api
    private void Awake()
    {
        defaultScanBtnColor = scanBtnImage.color;
    }
    #endregion
}