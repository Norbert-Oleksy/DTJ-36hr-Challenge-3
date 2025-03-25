using UnityEngine;

public class ShockModule : Module
{
    #region Fields
    [SerializeField] private AnimationCurve _alphaCurve;
    #endregion

    #region Variables
    private CanvasGroup _canvasGroup;
    private float _elapsedTime = 0f;
    private float _duration = 5f;
    #endregion

    #region Unity-API
    private void Awake()
    {
        active = true;
        _canvasGroup = GameObject.FindGameObjectWithTag("Szum").GetComponent<CanvasGroup>();
        _duration = _alphaCurve[_alphaCurve.length - 1].time;
    }

    private void Update()
    {
        if (GameManager.instance.stage == GameManager.gamestage.Win ||
            GameManager.instance.stage == GameManager.gamestage.Lose)
        {
            _canvasGroup.alpha = 0;
            enabled = false;
            return;
        }

        if (GameManager.instance.stage != GameManager.gamestage.Game) return;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _duration) _elapsedTime -= _duration;

        _canvasGroup.alpha = _alphaCurve.Evaluate(_elapsedTime);
    }
    #endregion
}