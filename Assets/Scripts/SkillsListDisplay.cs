using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SkillsListDisplay : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject skillDetailPrefab;
    [SerializeField] private GameObject sectionHeaderPrefab;

    [Header("Parent Container")]
    [SerializeField] private Transform contentParent;

    [Header("Skill Data")]
    [SerializeField] private SkillDataSO[] allSkills;

    [Header("Section Header Names (editable)")]
    public string gameDevelopmentHeader = "Game Development Skills";
    public string programmingHeader = "Programming & Technical Skills";
    public string softSkillsHeader = "Soft & Professional Skills";

    private void Start()
    {
        GenerateSkillList();
    }

    public void GenerateSkillList()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // group by skill type
        var groupedSkills = allSkills
            .OrderBy(s => s.skillType)
            .GroupBy(s => s.skillType);

        foreach (var group in groupedSkills)
        {
            // section header
            var headerGO = Instantiate(sectionHeaderPrefab, contentParent);
            var headerText = headerGO.GetComponentInChildren<TextMeshProUGUI>();
            headerText.text = GetHeaderName(group.Key);

            // skills
            var sortedSkills = group.OrderByDescending(s => s.percentage);
            foreach (var skill in sortedSkills)
            {
                var skillGO = Instantiate(skillDetailPrefab, contentParent);
                GenerateSkillDetails(skillGO, skill);
            }
        }

        // recalculate layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent.GetComponent<RectTransform>());
    }

    private void GenerateSkillDetails(GameObject skillGO, SkillDataSO skill)
    {
        var nameText = skillGO.transform.Find("SkillName")?.GetComponent<TextMeshProUGUI>();

        if (nameText != null) nameText.text = skill.name;

        // slider animation
        var animator = skillGO.GetComponent<SkillSliderAnimation>();
        if (animator != null)
        {
            animator.Initialize(skill.percentage);
            animator.PlayAnimation();
        }
    }

    private string GetHeaderName(SkillType type)
    {
        return type switch
        {
            SkillType.GameDevelopment => gameDevelopmentHeader,
            SkillType.ProgrammingAndTechnical => programmingHeader,
            SkillType.SoftAndProfessional => softSkillsHeader,
            _ => type.ToString()
        };
    }
}
