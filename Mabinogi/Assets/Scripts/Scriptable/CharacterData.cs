using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Character/Playerdata", fileName = "PlayerData")]
public class CharacterData : ScriptableObject
{
    [Tooltip("생명력. 피격시 감소")]
    [SerializeField]
    [Range(10, 1000)]
    private int hitPoint = 100;
    /// <summary> 생명력. 피격시 감소 </summary>
    public int HitPoint { get { return hitPoint; } }

    [Tooltip("마나. 마법 시전시 감소")]
    [SerializeField]
    [Range(10, 1000)]
    private int manaPoint = 100;
    /// <summary> 마나. 마법 시전시 감소 </summary>
    public int ManaPoint { get { return manaPoint; } }

    [Tooltip("스태미나. 스킬 시전시 감소")]
    [SerializeField]
    [Range(10, 1000)]
    private int staminaPoint = 100;
    /// <summary> 스태미나. 스킬 시전시 감소 </summary>
    public int StaminaPoint { get { return staminaPoint; } }

    [Tooltip("체력. 물리공격력에 영향을 줌")]
    [SerializeField]
    [Range(10, 1000)]
    private int strength = 10;
    /// <summary> 체력. 물리 공격력에 영향을 줌 </summary>
    public int Strength { get { return strength; } }

    [Tooltip("지력. 마법공격력에 영향을 줌")]
    [SerializeField]
    [Range(10, 1000)]
    private int intelligence = 10;
    /// <summary> 지력. 마법공격력에 영향을 줌 </summary>
    public int Intelligence { get { return intelligence; } }

    [Tooltip("솜씨. 밸런스에 영향을 줌")]
    [SerializeField]
    [Range(10, 1000)]
    private int dexterity = 10;
    /// <summary> 솜씨. 밸런스에 영향을 줌 </summary>
    public int Dexterity { get { return dexterity; } }

    [Tooltip("의지. 죽음을 이겨내고 데들리 상태가 될 확률이 증가한다")]
    [SerializeField]
    [Range(10, 1000)]
    private int will = 10;
    /// <summary> 의지. 죽음을 이겨내고 데들리 상태가 될 확률이 증가한다 </summary>
    public int Will { get { return will; } }

    [Tooltip("행운. 치명타 확률에 영향을 줌")]
    [SerializeField]
    [Range(10, 1000)]
    private int luck = 10;
    /// <summary> 행운. 치명타 확률에 영향을 줌 </summary>
    public int Luck { get { return luck; } }

    [Tooltip("최대물리공격력")]
    [SerializeField]
    [Range(1, 1000)]
    private int maxPhysicalStrikingPower = 10;
    /// <summary> 최대물리공격력 </summary>
    public int MaxPhysicalStrikingPower { get { return maxPhysicalStrikingPower + Strength; } }

    [Tooltip("최대마법공격력")]
    [SerializeField]
    [Range(1, 1000)]
    private int maxMagicStrikingPower = 10;
    /// <summary> 최대마법공격력 </summary>
    public int MaxMagicStrikingPower { get { return maxMagicStrikingPower + Intelligence; } }

    [Tooltip("최소물리공격력")]
    [SerializeField]
    [Range(1, 1000)]
    private int minPhysicalStrikingPower = 1;
    /// <summary> 최소물리공격력 </summary>
    public int MinPhysicalStrikingPower { get { return minPhysicalStrikingPower + Strength / 2; } }

    [Tooltip("최소마법공격력")]
    [SerializeField]
    [Range(1, 1000)]
    private int minMagicStrikingPower = 1;
    /// <summary> 최소마법공격력 </summary>
    public int MinMagicStrikingPower { get { return minMagicStrikingPower + Intelligence / 2; } }

    [Tooltip("캐릭터가 입은 부상")]
    [SerializeField]
    [Range(0, 1000)]
    private int wound = 0;
    /// <summary> 캐릭터가 입은 부상 </summary>
    public int Wound { get { return wound; } }

    [Tooltip("공격시 상대방에게 입히는 부상률")]
    [SerializeField]
    [Range(0, 1000)]
    private int woundAttack = 1;
    /// <summary> 공격시 상대방에게 입히는 부상률 </summary>
    public int WoundAttack { get { return woundAttack; } }

    [Tooltip("치명타 확률")]
    [SerializeField]
    [Range(0.0f, 0.8f)]
    private float critical = 0.2f;
    /// <summary> 치명타 확률 </summary>
    public float Critical { get { return critical + (float)Luck/100; } }

    [Tooltip("밸런스, 최소, 최대 데미지가 뜨는 비율")]
    [SerializeField]
    [Range(0.2f, 0.8f)]
    private float balance = 0.2f;
    /// <summary> 밸런스, 최소, 최대 데미지가 뜨는 비율 </summary>
    public float Balance { get { return balance + (float)Dexterity/100; } }

    [Tooltip("물리 방어력. 1대1 비율로 고정적인 데미지 감소")]
    [SerializeField]
    [Range(0, 1000)]
    private int physicalDefensivePower = 3;
    /// <summary> 물리 방어력. 1대1 비율로 고정적인 피해량 감소 </summary>
    public int PhysicalDefensivePower { get { return physicalDefensivePower; } }

    [Tooltip("마법 방어력. 1대1 비율로 고정적인 피해량 감소")]
    [SerializeField]
    [Range(0, 1000)]
    private int magicDefensivePower = 3;
    /// <summary> 마법 방어력. 1대1 비율로 고정적인 피해량 감소 </summary>
    public int MagicDefensivePower { get { return magicDefensivePower; } }

    [Tooltip("물리 보호. 1퍼센트 단위로 데미지 감소 ")]
    [SerializeField]
    [Range(0, 100)]
    private int physicalProtective = 1;
    /// <summary> 물리 보호. 1퍼센트 단위로 데미지 감소 </summary>
    public int PhysicalProtective { get { return physicalProtective; } }

    [Tooltip("마법 보호. 1퍼센트 단위로 데미지 감소")]
    [SerializeField]
    [Range(0, 100)]
    private int magicProtective = 1;
    /// <summary> 마법 보호. 1퍼센트 단위로 데미지 감소 </summary>
    public int MagicProtective { get { return magicProtective; } }

    [Tooltip("사망할 공격을 당했을 때 이겨내고 데들리 상태가 될 확률")]
    [SerializeField]
    [Range(0, 100)]
    private int deadly = 1;
    /// <summary> 사망할 공격을 당했을 때 이겨내고 데들리 상태가 될 확률 </summary>
    public int Deadly { get { return deadly + will/5; } }

    [Tooltip("이동 속도")]
    [SerializeField]
    [Range(0, 100)]
    private int speed = 1;
    /// <summary> 이동 속도 </summary>
    public int Speed { get { return speed; } }
}
