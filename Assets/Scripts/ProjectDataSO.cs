using UnityEngine;

[CreateAssetMenu(fileName = "NewProject", menuName = "Portfolio/Project")]
public class ProjectDataSO : ScriptableObject
{
    [TextArea] public string description;
    public Sprite sprite;

    [Header("Links")]
    public string trailerLink;
    public string githubLink;
}
