using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private string panelName;
    [SerializeField] private float animationDuration = 0.3f; // how fast it scales
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private ScrollRect scrollRect;

    private Coroutine scaleRoutine;

    public string PanelName => panelName;

    private void Start()
    {
        panelName = gameObject.name;
        scrollRect = GetComponentInChildren<ScrollRect>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(ScaleTo(Vector3.one));
    }

    public void Close()
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        scaleRoutine = StartCoroutine(ScaleTo(Vector3.zero, () =>
        {
            gameObject.SetActive(false);
        }));
    }

    public void CloseImmediate()
    {
        if (scaleRoutine != null) StopCoroutine(scaleRoutine);
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    private IEnumerator ScaleTo(Vector3 target, System.Action onComplete = null)
    {
        Vector3 start = transform.localScale;
        float time = 0f;

        while (time < animationDuration)
        {
            if (scrollRect != null)
            {
                Canvas.ForceUpdateCanvases();

                if (target == Vector3.one)
                {
                    scrollRect.verticalNormalizedPosition = 1f;
                }
            }

            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / animationDuration);
            float curveT = scaleCurve.Evaluate(t);
            transform.localScale = Vector3.LerpUnclamped(start, target, curveT);
            yield return null;
        }

        transform.localScale = target;
        onComplete?.Invoke();
    }
}
