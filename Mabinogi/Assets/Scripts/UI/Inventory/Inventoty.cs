using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInfo  //셀 정보 가지는 곳
{
    Define.Item itemType = Define.Item.None;
    public int amount; //아이템 개수
    public Image buttonImage = null;
    public Image itemImage = null;
    public Text amountText= null;

    public Define.Item GetItem()
    {
        return itemType;
    }
    public void SetItem(Define.Item wantItem)
    {

    }
}

public class Inventoty : MonoBehaviour
{
    public int width;
    public int height;
    public RectTransform cellAnchor;
    public GameObject cell;
    public GameObject parent;

    Vector2Int overedCellLocation = new Vector2Int(-1, -1);//마우스가 올려진 셀 위치

    CellInfo[,] infoArray; //칸 정보

    void Start()
    {
        
        infoArray = new CellInfo[height, width];

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                GameObject currentCell = Instantiate(cell) ; //현재 셀 만들기
                infoArray[i, j] = new CellInfo(); //클래스 CellInfo 인스턴스 만들기

                infoArray[i, j].amountText = currentCell.GetComponentInChildren<Text>();//셀에서 텍스트 컴포넌트 가져오기
                infoArray[i, j].buttonImage = currentCell.GetComponent<Image>(); //셀에서 버튼 이미지 컴포넌트 가져오기
                infoArray[i, j].itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//셀에서 아이템 이미지 컴포넌트 가져오기

                infoArray[i, j].buttonImage.color = new Color(0.6f, 0.6f, 0.6f);//버튼이미지 색상 회색으로 바꿈
                infoArray[i, j].itemImage.color = new Color(0.0f, 0.0f, 1f);//아이템 이미지 색상 파란색으로 바꿈
                RectTransform childRect = currentCell.GetComponent<RectTransform>();
                currentCell.transform.SetParent(parent.transform);
                Vector2 pos = cellAnchor.anchoredPosition + new Vector2((48 * j), (-48 * i));
                childRect.anchoredPosition = pos;
            }
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition -= cellAnchor.position;
        mousePosition.y *= -1;

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0)
        {
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(0.6f, 0.6f, 0.6f);
        }

        if (mousePosition.x < 0 || mousePosition.x > width * 48
            ||mousePosition.y < 0 || mousePosition.y > height * 48)
        {
            //0번칸은 있잖아 -1번칸은 없지 마우스가 올려진 곳이 없다는 뜻임
            overedCellLocation = Vector2Int.one * -1;
        }
        else
        {
            overedCellLocation.x = (int)mousePosition.x / 48;
            overedCellLocation.y = (int)mousePosition.y / 48;
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(1f, 1f, 1f);
            int overlap = 0;
            Debug.Log(CanPlace(overedCellLocation, new Vector2Int(2, 3), out overlap));
        };
    }

    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)
    {
        bool result = true;
        OverlapTime = 0;

        if (infoArray[overedCellLocation.y, overedCellLocation.x] == null) return false;
        Vector2Int rightBottom = position + itemSize - Vector2Int.one;
        if(position.x < 0 || position.y < 0 || rightBottom.x>= width||rightBottom.y >= height) //좌표가 0보다 작아서 소지품 창 범위 벗어나거나, 아이템 오른쪽 아래 좌표가 소지품창 크기보다 크면
        {
            return false;
        }
        for(int y = 0; y< itemSize.y; y++)
        {
            for(int x = 0; x < itemSize.x; x++)
            {
                if(infoArray[position.y + y, position.x + x].GetItem() != Define.Item.None)
                {
                    ++OverlapTime;
                    result = false;
                };
            };
        };

        return result;
    }
}
/*
 * 아이템을 실제로 놓아봐야됨
 * 아이템을 놓을 때 그 위치(기준점)에만 하나를 놓고 나머지 칸을 구성하는 애들은 그 자리를 가리키고 있어야 해
 * OE___
 * EE___
 * _____
 * _____
 * 포개지는 개수에 따라 스왑할 건지 확인
 * 두 개 이상 겹치면 아무 것도 하지 않기
 * 스왑하려고 할 때 같은 종류의 아이템이면 겹치기
 * 같은 종류의 아이템인데 겹칠 수 있는 숫자보다 많으면 남은 숫자만큼은 마우스에다가 놓기
 */