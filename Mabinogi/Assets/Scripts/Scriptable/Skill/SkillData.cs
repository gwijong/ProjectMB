using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Skill/SkillData", fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [Tooltip("스킬 ID")]
    [SerializeField]
    [Range(0, 100)]
    private Define.SkillState skillId = 0;
    /// <summary> 스킬 ID </summary>
    public Define.SkillState SkillId { get { return skillId; } }

    [SerializeField, Tooltip("스킬 랭크"), Range(1, 15)] int rank = 1;
    /// <summary> 스킬 랭크 </summary>
    public int Rank { get { return rank; } }

    [Tooltip("스킬 계수")]
    [SerializeField]
    [Range(1.0f, 100.0f)]
    private float coefficient = 1.0f;
    /// <summary> 스킬 계수 </summary>
    public float Coefficient { get { return coefficient * Rank; } }

    [Tooltip("스킬 시전 시간")]
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float castTime = 1.0f;
    /// <summary> 스킬 시전 시간 </summary>
    public float CastTime { get { return castTime; } }

    [Tooltip("스킬 시전 시 들어가는 비용")]
    [SerializeField]
    [Range(0, 100)]
    private int castCost = 3;
    /// <summary> 스킬 시전 시 들어가는 비용 </summary>
    public int CastCost { get { return castCost; } }

    [Tooltip("경직 시간")]
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float stiffnessTime = 1.0f;
    /// <summary> 경직 시간 </summary>
    public float StiffnessTime { get { return stiffnessTime; } }

    [Tooltip("다운 게이지")]
    [SerializeField]
    [Range(0, 100)]
    private int downGauge = 100;
    /// <summary> 다운 게이지 </summary>
    public int DownGauge { get { return downGauge; } }

}
