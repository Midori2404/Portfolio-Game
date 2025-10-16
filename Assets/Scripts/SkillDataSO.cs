using UnityEngine;


[CreateAssetMenu(fileName = "SkillDataSO", menuName = "Portfolio/Skill Data")]
public class SkillDataSO : ScriptableObject
{
    [Range(0, 100)] public int percentage;
    public SkillType skillType;
}

public enum SkillType
{
    GameDevelopment,
    ProgrammingAndTechnical,
    SoftAndProfessional
}
