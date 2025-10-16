using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class SignInteraction : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject arrowUI;
    [SerializeField] private GameObject interactUI;

    [Header("Animation Settings")]
    [SerializeField] private float bounceAmplitude = 0.3f;
    [SerializeField] private float bounceSpeed = 5f;

    private Vector3 basePosition;

    [Header("Panel Settings")]
    [SerializeField] private UnityEvent onInteract;

    private PlayerController playerController;

    private bool playerInRange = false;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;

        if (uiCanvas != null)
        {
            basePosition = uiCanvas.transform.localPosition;
        }

        // Default state
        arrowUI.SetActive(true);
        interactUI.SetActive(false);
    }

    private void Update()
    {
        // bounce the UI up and down
        if (uiCanvas != null)
        {
            float newY = basePosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceAmplitude;
            uiCanvas.transform.localPosition = new Vector3(basePosition.x, newY, basePosition.z);
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E) && playerController.canAttack)
        {
            playerController.Attack();
            onInteract?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            interactUI.SetActive(true);
            arrowUI.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactUI.SetActive(false);
            arrowUI.SetActive(true);
        }
    }
}
