using UnityEngine;

//[CreateAssetMenu(menuName = "메뉴 경로",fileName = "기본 파일명"), order = 메뉴상에서 순서]
[CreateAssetMenu(menuName = "Scriptable/Item/Itemdata", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    [Tooltip("아이템 이름")]
    [SerializeField]
    private string itemName = "나무 열매";
    /// <summary> 아이템 이름 </summary>
    public string ItemName { get { return itemName; } }

    [Tooltip("구매 가격")]
    [SerializeField]
    [Range(1, 10000)]
    private int purchasePrice = 100;
    /// <summary> 구매 가격 </summary>
    public int PurchasePrice { get { return purchasePrice; } }

    [Tooltip("판매 가격")]
    [SerializeField]
    [Range(1, 10000)]
    private int salePrice = 10;
    /// <summary> 판매 가격 </summary>
    public int SalePrice { get { return salePrice; } }

    [Tooltip("아이템 칸 너비")]
    [SerializeField]
    [Range(1, 4)]
    private int width = 1;
    /// <summary> 아이템 칸 너비 </summary>설명문
    public int Width { get { return width; } }

    [Tooltip("아이템 칸 높이")]
    [SerializeField]
    [Range(1, 4)]
    private int height = 1;
    /// <summary> 아이템 칸 높이 </summary>
    public int Height { get { return height; } }

    [Tooltip("아이템 설명문")]
    [SerializeField]
    private string description = "소지품창 안의 일반 아이템";
    /// <summary> 아이템 설명문 </summary>
    public string Description { get { return description; } }
}
