using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectDetailsDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image projectImage;

    [Header("Buttons")]
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject buttonPrefab;

    public void ShowProject(ProjectDataSO project)
    {
        titleText.text = project.name;
        descriptionText.text = project.description;
        projectImage.sprite = project.sprite;

        // clear buttons
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        // github button
        if (!string.IsNullOrEmpty(project.githubLink))
            CreateButton("GITHUB", project.githubLink);

        // trailer button
        if (!string.IsNullOrEmpty(project.trailerLink))
            CreateButton("TRAILER", project.trailerLink);
    }

    private void CreateButton(string label, string url)
    {
        GameObject newBtn = Instantiate(buttonPrefab, buttonContainer);
        newBtn.GetComponentInChildren<TextMeshProUGUI>().text = label;
        newBtn.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }
}
