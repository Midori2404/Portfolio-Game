using UnityEngine;

public class Shortcut : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    private void Awake()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    public void TeleportToTarget(GameObject target)
    {
        if (player == null || target == null)
        {
            Debug.LogWarning("ShortcutTeleport: Missing player or target reference.");
            return;
        }

        player.position = target.transform.position;
        Debug.Log($"Teleported player to {target.name} at {target.transform.position}");
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
