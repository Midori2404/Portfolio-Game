using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SkillSliderAnimation : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI percentageText;

    private float targetValue;
    private Coroutine animationCoroutine;

    public void Initialize(float percentage)
    {
        targetValue = percentage / 100f;
        slider.value = 0;
        percentageText.text = "0";
    }

    public void PlayAnimation(float duration = 1.2f)
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(AnimateSlider(duration));
    }

    private IEnumerator AnimateSlider(float duration)
    {
        float elapsed = 0f;
        float startValue = slider.value;
        float endValue = targetValue;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float value = Mathf.Lerp(startValue, endValue, t);
            slider.value = value;

            percentageText.text = Mathf.RoundToInt(value * 100f).ToString();
            yield return null;
        }

        slider.value = endValue;
        percentageText.text = Mathf.RoundToInt(endValue * 100f).ToString();
    }
}
