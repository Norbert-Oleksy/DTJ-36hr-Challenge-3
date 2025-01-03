using UnityEngine;
using UnityEngine.UI;

public class PushUPModule : Module
{
    [SerializeField] private Image progresBar;
    [SerializeField] private Slider slider;
    [SerializeField] private Color orangeColor;

    [Header("Audio")]
    [SerializeField] private AudioSource audioEmiterUp;
    [SerializeField] private AudioSource audioEmiterDown;

    private float procentage;
    private float bestValue;

    private Color progresColor;
    public void Logic(float value)
    {
        if(active) return;

        if (value == 0 && bestValue > 0)
        {
            procentage = procentage + (bestValue / 20);
            bestValue = 0;

            if (audioEmiterUp.isPlaying) audioEmiterUp.Stop();
            if (!audioEmiterDown.isPlaying) audioEmiterDown.Play();

            if (procentage >= 1.0f)
            {
                progresBar.fillAmount = 1.0f;
                progresBar.color = Color.green;
                active = true;
                slider.interactable = false;
                slider.value = 0;
                Bomb.instance.CheckIfBombIsDefused();
            }
            else
            {
                progresBar.fillAmount = procentage;
                ColorController();
            }
        }
        else if(value > bestValue)
        {
            bestValue = value;

            if (audioEmiterDown.isPlaying) audioEmiterDown.Stop();
            if (!audioEmiterUp.isPlaying) audioEmiterUp.Play();
        } 
    }

    private void ColorController()
    {
        if(procentage <= 0.5f)
        {
            progresColor = Color.Lerp(Color.white, orangeColor, procentage * 2);
            progresColor.a = 1;
        }
        else
        {
            progresColor = Color.Lerp(orangeColor, Color.green, procentage / 2);
            progresColor.a = 1;
        }
        progresBar.color = progresColor;
    }

    private void Awake()
    {
        procentage = 0f;
        bestValue = 0f;
        progresBar.fillAmount = 0;
    }
}