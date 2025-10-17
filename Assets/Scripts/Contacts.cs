using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Contacts : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float bounceAmplitude = 0.3f;
    [SerializeField] private float bounceSpeed = 5f;

    private Vector3 basePosition;

    [Header("Recipient Info")]
    [SerializeField] private string recipientEmail = "giovandriwiryanli2404@gmail.com";
    [SerializeField] private string subject = "Inquiry from your portfolio";

    private void Start()
    {
        basePosition = gameObject.transform.localPosition;
    }

    private void Update()
    {
        float newY = basePosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceAmplitude;
        gameObject.transform.localPosition = new Vector3(basePosition.x, newY, basePosition.z);
    }

    public void OpenLink(string url)
    {
        EventSystem.current.SetSelectedGameObject(null);

        Application.OpenURL(url);
    }

    public void ComposeEmail()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (string.IsNullOrEmpty(recipientEmail))
        {
            Debug.LogWarning("Recipient email not set.");
            return;
        }

        string escapedSubject = Uri.EscapeDataString(subject ?? "");
        string escapedBody = Uri.EscapeDataString("");

        // mailto link
        string mailtoLink = $"mailto:{recipientEmail}?subject={escapedSubject}&body={escapedBody}";

        Application.OpenURL(mailtoLink);
    }
}
