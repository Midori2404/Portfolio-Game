using UnityEngine;
using UnityEngine.EventSystems;

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
        EventSystem.current.SetSelectedGameObject(null);

        player.position = target.transform.position;
    }

    public void OpenLink(string url)
    {
        EventSystem.current.SetSelectedGameObject(null);

        Application.OpenURL(url);
    }
}
