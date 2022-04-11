using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Skill/SkillData", fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField]
    private int rank = 1;
    /// <summary> 스킬 랭크 </summary>
    public int Rank { get { return rank; } }

    [SerializeField]
    private float coefficient = 1.0f;
    /// <summary> 스킬 계수 </summary>
    public float Coefficient { get { return coefficient * Rank; } }

    [SerializeField]
    private float castTime = 1.0f;
    /// <summary> 스킬 시전 시간 </summary>
    public float CastTime { get { return castTime; } }

    [SerializeField]
    private int castCost = 3;
    /// <summary> 스킬 시전 시 들어가는 비용 </summary>
    public float CastCost { get { return castCost; } }


}
