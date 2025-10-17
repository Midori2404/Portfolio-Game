using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIPanel[] panels;
    [SerializeField] private UIPanel currentPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (var panel in panels)
        {
            panel.Close();
            panel.CloseImmediate();
        }
    }

    public void OpenPanel(string panelName)
    {
        if (currentPanel != null)
            currentPanel.Close();

        UIPanel nextPanel = System.Array.Find(panels, p => p.PanelName == panelName);
        if (nextPanel != null)
        {
            nextPanel.Open();
            currentPanel = nextPanel;
        }
        else
        {
            Debug.LogWarning($"UIManager: No panel found with name {panelName}");
        }

        EventSystem.current.SetSelectedGameObject(null);
        GameStateManager.Instance.SetState(GameState.UI);
    }

    public void ClosePanel()
    {
        currentPanel.Close();
        currentPanel = null;

        EventSystem.current.SetSelectedGameObject(null);
        GameStateManager.Instance.SetState(GameState.Playing);
    }
}
