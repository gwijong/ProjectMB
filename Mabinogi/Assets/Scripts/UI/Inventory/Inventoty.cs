using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 셀 정보 가지는 곳 </summary>
public class CellInfo  
{
    CellInfo root = null; //아이템을 찾으려면 루트를 먼저 찾아준다 루트가 null이면 이놈이 시작지점이다
    /// <summary> 아이템 타입 </summary>
    Define.Item itemType = Define.Item.None;
    /// <summary> 아이템 개수 </summary>
    public int amount;
    /// <summary> 아이템 배경이 되는 버튼 이미지 </summary>
    public Image buttonImage = null;
    /// <summary> 아이템 이미지 </summary>
    public Image itemImage = null;
    /// <summary> 아이템 개수 표시하는 텍스트 </summary>
    public Text amountText= null;
    /// <summary> 인벤토리 안에서 셀 위치 </summary>
    Vector2Int location;

    static Color normalColor = new Color(0.9f,0.9f,0.9f);
    static Color filledColor = new Color(0.7f,0.7f,0.7f);//뭐가 들어있을 때 컬러

    /// <summary> 로케이션 지정하는 생성자 </summary>
    public CellInfo(Vector2Int wantLocation)
    {
        location = wantLocation;
        root = this;
    }

    /// <summary> 아이템 타입 반환 </summary>
    public Define.Item GetItemType()
    {
        return itemType;
    }

    /// <summary> 아이템 변경 </summary>
    public void SetItem(Define.Item wantItem)
    {
        itemType = wantItem;
        CalculateColor();
    }


    /// <summary> 루트 셋 </summary>
    public void SetRoot(CellInfo setRoot)
    {
        root = setRoot;
        CalculateColor();
    }

    public void SetColor(Color wantColor)
    {
        buttonImage.color = wantColor;
    }
    
    public void CalculateColor()
    {
        if (IsEmpty())
        {
            SetColor(normalColor);
        }
        else
        {
            SetColor(filledColor);
        };
    }

    /// <summary> 루트 반환 </summary>
    public CellInfo GetRoot()
    {
        return root;
    }

    public Vector2Int GetLocation()
    {
        return location;
    }

    /// <summary> 칸이 비어있는지 </summary>
    public bool IsEmpty()
    {
        return (root == null || root == this) && itemType == Define.Item.None; 
    }

    public void Clear()
    {
        root = this;
        SetItem(Define.Item.None);
    }

    
}

public class Inventoty : MonoBehaviour
{
    /// <summary> 소지품창 넓이 </summary>
    public int width;
    /// <summary> 소지품창 높이 </summary>
    public int height;
    /// <summary> 아이템창 칸들의 시작 기준점</summary>
    public RectTransform cellAnchor;
    /// <summary> 아이템창 각 칸 프리팹</summary>
    public GameObject cell;
    /// <summary> 인벤토리 창 UI 이미지</summary>
    public GameObject parent;
    /// <summary> 마우스가 올려진 셀 위치</summary>
    Vector2Int overedCellLocation = new Vector2Int(-1, -1);
    /// <summary> [?,?] 칸의 정보</summary>
    CellInfo[,] infoArray;

    //ItemData bottle = Resources.Load<ItemData>("Data/ItemData/Bottle");

    void Start()
    {
        
        infoArray = new CellInfo[height, width]; //2차원 배열의 크기를 소지품창 크기로 설정

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject currentCell = Instantiate(cell) ; //현재 셀 만들기
                currentCell.transform.SetParent(parent.transform); //현재 셀을 부모 오브젝트의 자식 오브젝트로 지정

                infoArray[y, x] = new CellInfo(new Vector2Int(x,y)); //클래스 CellInfo 인스턴스 만들기

                infoArray[y, x].amountText = currentCell.GetComponentInChildren<Text>();//셀에서 텍스트 컴포넌트 가져오기
                infoArray[y, x].buttonImage = currentCell.GetComponent<Image>(); //셀에서 버튼 이미지 컴포넌트 가져오기
                infoArray[y, x].itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//셀에서 아이템 이미지 컴포넌트 가져오기

                infoArray[y, x].CalculateColor();
                
                RectTransform childRect = currentCell.GetComponent<RectTransform>(); //현재 셀의 RectTransform 가져옴
                Vector2 pos = cellAnchor.anchoredPosition + new Vector2((48 * x), (-48 * y));//각 셀들의 위치를 시작 기준점에서 48픽셀 간격으로 배치함
                childRect.anchoredPosition = pos;//현재 셀의 앵커 포지션을 시작 기준점에서 48 * x, -48 * y 간격으로 배치함
            }
        }
    }

    private void Update()
    {
        /// <summary> 마우스 좌표 </summary>
        Vector3 mousePosition = Input.mousePosition;
        mousePosition -= cellAnchor.position; //마우스 좌표에서 셀들의 시작 기준점을 빼줌
        mousePosition.y *= -1;//y값이 음수면 햇갈리기 때문에 음수 곱해서 양수로 바꿔줌

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0) //마우스 커서가 0,0 이상이면
        {
            //해당 좌표 셀의 버튼 이미지의 컬러를 회색으로 바꿈
            infoArray[overedCellLocation.y, overedCellLocation.x].CalculateColor();
        }

        if (mousePosition.x < 0 || mousePosition.x > width * 48 // 마우스좌표의 x가 0보다 작거나 마우스 좌표가 소지품창 칸 넓이를 넘어가거나
            ||mousePosition.y < 0 || mousePosition.y > height * 48) // 마우스좌표의 y가 0보다 작거나 마우스 좌표가 소지품창 칸 길이를 넘어가거나
        {

            //overedCellLocation을 (-1,-1)로 바꿈
            overedCellLocation = Vector2Int.one * -1;// -1번칸은 없으므로 마우스가 올려진 곳이 없다는 뜻임
        }
        else //마우스 커서가 소지품 창 안에 있는 경우
        {
            overedCellLocation.x = (int)mousePosition.x / 48; //마우스 좌표에서 칸 넓이인 48로 나눠서 정수로 변환
            overedCellLocation.y = (int)mousePosition.y / 48; //마우스 좌표에서 칸 높이인 48로 나눠서 정수로 변환

            //마우스 커서가 있는 아이템의 버튼 색상을 온전한 색상으로 바꿔줌
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(1f, 1f, 1f); 

            int overlap = 0;
            int amount = 0;
            if (Input.GetMouseButtonDown(1))
                SubItem(overedCellLocation, out amount);
            if (Input.GetMouseButtonDown(0))
            {
                PutItem(overedCellLocation,Define.Item.Bottle, 1);
            }

            

        };

    }

    //좌표, 아이템크기,
    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)
    {
        bool result = true; //true면 넣을 수 있음, false 면 넣을 수 없음
        OverlapTime = 0;

        if (infoArray[overedCellLocation.y, overedCellLocation.x] == null) return false; //좌표값이 null일 경우 리턴
        Vector2Int rightBottom = position + itemSize - Vector2Int.one; //아이템의 오른쪽 아래 모서리 좌표

        // 좌표가 0보다 작아서 소지품 창 범위 벗어나거나, 아이템 오른쪽 아래 모서리 좌표가 소지품창 크기보다 초과되면
        if (position.x < 0 || position.y < 0 || rightBottom.x>= width||rightBottom.y >= height) 
        {
            return false;
        }
        for(int y = 0; y< itemSize.y; y++) //아이템의 y크기 만큼 반복
        {
            for(int x = 0; x < itemSize.x; x++) //아이템의 x크기 만큼 반복
            {
                //아이템 크기만큼의 공간이 전부 빈 상태가 아닌 경우
                if(infoArray[position.y + y, position.x + x].IsEmpty() == false)
                {
                    ++OverlapTime;
                    result = false;
                };
            };
        };
        return result;
    }
                          //셀 좌표
    void PutItem(Vector2Int position, Define.Item item, int amount)
    {
        Vector2Int size = item.GetSize();
        int overlap;
        if (CanPlace(position, size, out overlap) == true)
        {
            infoArray[position.y, position.x].SetItem(item);
            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (x == 0 && y == 0) continue;
                    infoArray[position.y + y, position.x + x].SetRoot(infoArray[position.y, position.x]);
                }
            }
        }
    }
    /// <summary> 해당된 셀의 아이템 리턴</summary>
    Define.Item SubItem(Vector2Int position, out int amount)
    {
        CellInfo selectedInfo = CheckItemRoot(position);
        Vector2Int rootLocation = selectedInfo.GetLocation();

        amount = selectedInfo.amount;
        Define.Item result = selectedInfo.GetItemType();


        if (result == Define.Item.None)
        {
            return result;
        }
        else
        {     
            Vector2Int size = result.GetSize();
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    infoArray[rootLocation.y + y, rootLocation.x + x].Clear();
                }
            }            
        }
        return result;
    }

    /// <summary> 해당된 셀의 아이템 리턴</summary>
    Define.Item CheckItem(Vector2Int position)
    {
        return CheckItemRoot(position).GetItemType();
    }

    /// <summary> 해당된 칸의 루트를 체크하는 구간</summary>
    CellInfo CheckItemRoot(Vector2Int position)
    {
        return infoArray[position.y, position.x].GetRoot(); //나 자신 반환하거나, 기준점 반환하거나
    }
}


/*
 * 그 위치에 아이템이 있는지 찾아봐야 함
 * 어떤 아이템이 있는지 체크해보기
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

//마우스가 올려져 있는 칸의 루트 확인, 루트의 아이템 타입의 사이즈 받아옴, 사이즈 개수만큼 하이라이트 해줌,지금 마우스 말고 직전에 있었던 마우스도 체크해야함 그래야 원래 색깔로 돌려줌, 직전과 지금 마우스 위치 다르면 직전은 풀어줘야함