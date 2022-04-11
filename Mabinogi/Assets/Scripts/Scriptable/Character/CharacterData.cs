using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Character/Playerdata", fileName = "PlayerData")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private int hitPoint = 100;
    /// <summary> 생명력. 피격시 감소 </summary>
    public int HitPoint { get { return hitPoint; } }

    [SerializeField]
    private int manaPoint = 100;
    /// <summary> 마나. 마법 시전시 감소 </summary>
    public int ManaPoint { get { return manaPoint; } }

    [SerializeField]
    private int staminaPoint = 100;
    /// <summary> 스태미나. 스킬 시전시 감소 </summary>
    public int StaminaPoint { get { return staminaPoint; } }

    [SerializeField]
    private int strength = 10;
    /// <summary> 체력 </summary>
    public int Strength { get { return strength; } }

    [SerializeField]
    private int intelligence = 10;
    /// <summary> 지력 </summary>
    public int Intelligence { get { return intelligence; } }

    [SerializeField]
    private int dexterity = 10;
    /// <summary> 솜씨 </summary>
    public int Dexterity { get { return dexterity; } }

    [SerializeField]
    private int will = 10;
    /// <summary> 의지 </summary>
    public int Will { get { return will; } }

    [SerializeField]
    private int luck = 10;
    /// <summary> 행운 </summary>
    public int Luck { get { return luck; } }

    [SerializeField]
    private int physicalStrikingPower = 10;
    /// <summary> 물리공격력 </summary>
    public int PhysicalStrikingPower { get { return physicalStrikingPower + Strength; } }

    [SerializeField]
    private int magicStrikingPower = 10;
    /// <summary> 마법공격력 </summary>
    public int MagicStrikingPower { get { return physicalStrikingPower + Intelligence; } }

    [SerializeField]
    private int wound = 0;
    /// <summary> 부상 </summary>
    public int Wound { get { return wound; } }

    [SerializeField]
    private float critical = 0.2f;
    /// <summary> 치명타 확률 </summary>
    public int Critical { get { return physicalStrikingPower + Luck; } }

    [SerializeField]
    private float balance = 0.2f;
    /// <summary> 밸런스, 최소, 최대 데미지가 뜨는 비율 </summary>
    public int Balance { get { return physicalStrikingPower + Dexterity; } }

    [SerializeField]
    private int physicalDefensivePower = 3;
    /// <summary> 물리 방어력 </summary>
    public int PhysicalDefensivePower { get { return physicalDefensivePower; } }

    [SerializeField]
    private int magicDefensivePower = 3;
    /// <summary> 마법 방어력 </summary>
    public int MagicDefensivePower { get { return magicDefensivePower; } }

    [SerializeField]
    private int physicalProtective = 1;
    /// <summary> 물리 보호 </summary>
    public int PhysicalProtective { get { return physicalProtective; } }

    [SerializeField]
    private int magicProtective = 1;
    /// <summary> 마법 보호 </summary>
    public int MagicProtective { get { return magicProtective; } }
}
