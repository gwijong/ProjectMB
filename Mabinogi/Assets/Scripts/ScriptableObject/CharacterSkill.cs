using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Skill/CharacterSkill", fileName = "CharacterSkill")]
public class CharacterSkill : ScriptableObject
{
    [SerializeField, Tooltip("컴벳 마스터리 스킬 랭크"), Range(0, 15)] 
    int combatRank = 1;
    /// <summary> 컴벳 마스터리 스킬 랭크 </summary>
    public int CombatRank { get { return combatRank; } }

    [SerializeField, Tooltip("디펜스 스킬 랭크"), Range(0, 15)] 
    int defenseRank = 1;
    /// <summary> 디펜스 스킬 랭크 </summary>
    public int DefenseRank { get { return defenseRank; } }

    [SerializeField, Tooltip("스매시 스킬 랭크"), Range(0, 15)] 
    int smashRank = 1;
    /// <summary> 스매시 스킬 랭크 </summary>
    public int SmashRank { get { return smashRank; } }

    [SerializeField, Tooltip("카운터 어택 스킬 랭크"), Range(0, 15)] 
    int counterRank = 1;
    /// <summary> 카운터 어택 스킬 랭크 </summary>
    public int CounterRank { get { return counterRank; } }

    [SerializeField, Tooltip("윈드밀 스킬 랭크"), Range(0, 15)]
    int windmillRank = 1;
    /// <summary> 윈드밀 스킬 랭크 </summary>
    public int WindmillRank { get { return windmillRank; } }

    [SerializeField, Tooltip("아이스볼트 스킬 랭크"), Range(0, 15)]
    int iceboltRank = 1;
    /// <summary> 아이스볼트 스킬 랭크 </summary>
    public int IceboltRank { get { return iceboltRank; } }

}
