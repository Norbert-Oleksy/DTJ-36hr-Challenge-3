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
    #endregion

    #region Variables
    private bool isScanModeOn=false;
    #endregion

    #region Functions

    public void ModulesInfoBtnAction(int id)
    {
        GameObject msg = Instantiate(msgPrefab);
        msg.transform.SetParent(msgContent, false);
        LocalizeStringEvent lseMsg = msg.GetComponent<LocalizeStringEvent>();
        lseMsg.SetEntry(Bomb.instance.ReturnModuleInfoID(id));
        lseMsg.RefreshString();
   
        scrollbar.value = 0;
    }

    public void TipBtnAction()
    {
        GameObject msg = Instantiate(msgPrefab);
        msg.transform.SetParent(msgContent, false);
        LocalizeStringEvent lseMsg = msg.GetComponent<LocalizeStringEvent>();
        lseMsg.SetEntry("");
        lseMsg.RefreshString();
        scrollbar.value = 0;
    }

    public void TurnOnOffScanMode()
    {
        if(isScanModeOn)
        {
            modulesBtnSection.SetActive(false);
        }
        else
        {
            modulesBtnSection.SetActive(true);
        }
        isScanModeOn = !isScanModeOn;
    }

    #endregion

    #region Unity-Api
    private void Update()
    {
        if (GameManager.instance.stage != GameManager.gamestage.Game) return;
        if (isScanModeOn && Input.GetKeyDown(KeyCode.Escape)) TurnOnOffScanMode();
    }
    #endregion
}
