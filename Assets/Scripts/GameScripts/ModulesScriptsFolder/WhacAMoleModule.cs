using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhacAMoleModule : Module
{
    #region Fields
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private List<Button> _buttons;
    [SerializeField] private float lifeTime = 0.5f;
    #endregion

    #region Variables
    private int score;
    private int targetScore = 100;
    #endregion

    #region Logic
    public void ButtonAction(Button clickedButton)
    {
        if(!clickedButton.interactable) return;
        clickedButton.interactable = false;
        score += 10;
        UpdateScoreText();
        CheckCompleteCondition();
    }

    private void UpdateScoreText() => _scoreText.text = score.ToString();

    private void Initialization()
    {
        score = 0;
        UpdateScoreText();
        StartCoroutine(ModuleLogic());
    }

    private void CheckCompleteCondition()
    {
        if(score < targetScore) return;
        _scoreText.color = Color.green;

        active = true;
        Bomb.instance.CheckIfBombIsDefused();
    }

    private IEnumerator ModuleLogic()
    {
        while (!active)
        {
            StartCoroutine(PopUpTarget(_buttons.FindAll(b => !b.interactable)));
            yield return new WaitForSeconds(lifeTime);
            yield return new WaitWhile(() => GameManager.instance.stage != GameManager.gamestage.Game);
        }
        yield return null;
    }

    private IEnumerator PopUpTarget(List<Button> buttons)
    {
        if(buttons == null) yield return null;

        Button target = buttons[Random.Range(0, buttons.Count)];
        target.interactable = true;
        yield return new WaitForSeconds(lifeTime);
        yield return new WaitWhile(() => GameManager.instance.stage != GameManager.gamestage.Game);
        target.interactable = false;
        yield return null;
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        Initialization();
    }
    #endregion
}