using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 셀 정보 가지는 곳 </summary>
public class CellInfo  
{
    /// <summary> 아이템을 찾으려면 루트를 먼저 찾아준다 루트가 null이거나 나 자신 셀이면 이놈이 시작지점이다 </summary>
    CellInfo root = null; 
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
    /// <summary> 아이템 창 칸의 평소 색상 </summary>
    static Color normalColor = new Color(0.9f,0.9f,0.9f);
    /// <summary> 뭔가 아이템이 들어있을 때 어두운 컬러 </summary>
    static Color filledColor = new Color(0.7f,0.7f,0.7f);
    /// <summary> 마우스 커서가 위치한 칸 밝게 강조하는 컬러 </summary>
    public static Color highlightColor = new Color(1, 1, 1);

    /// <summary> 소지품창 안에서의 아이템 좌표와 나 자신을 루트로 지정하는 생성자 </summary>
    public CellInfo(Vector2Int wantLocation)
    {
        location = wantLocation; //인벤토리 안에서의 셀 위치 지정
        root = this; //루트는 나 자신 셀이다
    }

    /// <summary> Define.Item 아이템 타입 반환 </summary>
    public Define.Item GetItemType()
    {
        return itemType;
    }

    /// <summary> 셀의 아이템 변경과 칸의 색상 세팅 </summary>
    public void SetItem(Define.Item wantItem, int wantAmount)
    {
        Vector2Int itemSize = wantItem.GetSize();
        itemType = wantItem;
        itemImage.sprite = wantItem.GetItemImage();
        if (itemImage.sprite == null)
        {
            itemImage.color = Color.clear;
        }
        else
        {
            itemImage.color = Color.white;
        }
        itemImage.transform.localScale = new Vector3(itemSize.x, itemSize.y, 1);
        CalculateColor();
        SetAmount(wantAmount);
    }


    /// <summary> 루트 변경과 칸의 색상 세팅 </summary>
    public void SetRoot(CellInfo setRoot)
    {
        root = setRoot;
        CalculateColor();
    }

    /// <summary> 버튼 이미지 원하는 색상 지정 normalColor, filledColor, highlightColor </summary>
    public void SetColor(Color wantColor)
    {
        buttonImage.color = wantColor;
    }

    /// <summary> 칸이 비어있으면 normalColor, 칸이 차있으면 filledColor </summary>
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

    /// <summary> 인벤토리 안에서의 셀의 위치 반환 </summary>
    public Vector2Int GetLocation()
    {
        return location;
    }

    /// <summary> 칸이 비어있는지 체크. 루트가 null이거나 나 자신 셀이면서 아이템타입이 none이면 true </summary>
    public bool IsEmpty()
    {
        if((root == null || root == this) && itemType == Define.Item.None == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void SetAmount(int wantAmount)
    {
        amount = wantAmount;
        amountText.text = wantAmount > 0 ? wantAmount.ToString() : "";
    }

    /// <summary> 칸을 비움. root 를 나 자신 셀로 바꾸고 아이템을 none으로 바꿈 </summary>
    public void Clear()
    {
        root = this;
        SetItem(Define.Item.None,0);
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
    /// <summary> 마우스가 집고있는 아이템</summary>
    public static CellInfo mouseItem { get; private set; }

    public static RectTransform mouseItemPos;

    //ItemData bottle = Resources.Load<ItemData>("Data/ItemData/Bottle");

    void Start()
    {
        if (mouseItem == null)
        {
            GameObject currentCell = Instantiate(cell);
            currentCell.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform);
            mouseItemPos = currentCell.GetComponent<RectTransform>();
            mouseItem = new CellInfo(new Vector2Int(0,0));
            mouseItem.amountText = currentCell.GetComponentInChildren<Text>();//셀에서 텍스트 컴포넌트 가져오기
            mouseItem.buttonImage = currentCell.GetComponent<Image>(); //셀에서 버튼 이미지 컴포넌트 가져오기
            mouseItem.itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//셀에서 아이템 이미지 컴포넌트 가져오기
            mouseItem.itemImage.rectTransform.pivot = Vector2.one * 0.5f;
            mouseItem.SetItem(Define.Item.Wool,1);
            mouseItem.buttonImage.enabled = false;
            currentCell.GetComponent<Button>().enabled = false;
        }
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;

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
        foreach (CellInfo current in infoArray)
        {
            current.itemImage.transform.SetParent(parent.transform);
            current.amountText.transform.SetParent(parent.transform);
            current.SetItem(Define.Item.None,0);
        }
        PutItem(Vector2Int.one, Define.Item.Wool, 3);
        PutItem(Vector2Int.zero, Define.Item.Fruit, 1);
    }

    private void OnUpdate()
    {
        if(mouseItemPos == null)
        {
            return;
        }
        mouseItemPos.position = Input.mousePosition; 
        /// <summary> 마우스 좌표 </summary>
        Vector3 mousePosition = Input.mousePosition;
        mousePosition -= cellAnchor.position; //마우스 좌표에서 셀들의 시작 기준점을 빼줌
        mousePosition.y *= -1;//y값이 음수면 햇갈리기 때문에 음수 곱해서 양수로 바꿔줌

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0) //마우스 커서가 0,0 이상이면
        {
            //해당 좌표 셀의 버튼 이미지의 컬러를 회색으로 바꿈
            //infoArray[overedCellLocation.y, overedCellLocation.x].CalculateColor();
            CellHighlight(overedCellLocation, false); //기존 마우스 커서가 있는 아이템의 버튼 색상을 기본값으로 바꿔줌
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

            
            CellHighlight(overedCellLocation,true); //마우스 커서가 있는 아이템의 버튼 색상을 온전한 색상으로 바꿔줌

            int overlap = 0;
            int amount = 0;
            if (Input.GetMouseButtonDown(1))
                SubItem(overedCellLocation, out amount);
            if (Input.GetMouseButtonDown(0))
            {
                LeftClick(overedCellLocation);
            }

            

        };

    }

    /// <summary> 해당 위치에 아이템을 밀어 넣는게 가능한지 체크 </summary>
    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)    //셀 좌표, 아이템크기,아이템 사이즈 , 아이템 겹친 횟수
    {
        bool result = true; //true면 넣을 수 있음, false 면 넣을 수 없음
        OverlapTime = 0;

        List<CellInfo> rootList = new List<CellInfo>(); //겹친 루트 갯수 확인

        if (infoArray[position.y, position.x] == null) return false; //좌표값이 null일 경우 리턴
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
                    CellInfo currentRoot = infoArray[position.y + y, position.x + x].GetRoot();
                    if (!rootList.Contains(currentRoot))
                    {
                        rootList.Add(currentRoot);
                    };
                    result = false;
                };
            };
        };
        OverlapTime = rootList.Count;
        return result;
    }

    void LeftClick(Vector2Int pos)
    {
        
        if (pos == null || pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        {
            return;
        }
        int overlapTime = 0;
        int currentAmount = 0;
        Define.Item currentItem = Define.Item.None;

        CellInfo currentCell = CheckItemRoot(pos);

        if (mouseItem.GetItemType() == Define.Item.None)
        {
            currentItem = SubItem(pos, out currentAmount);
            mouseItem.SetItem(currentItem, currentAmount);
        }
        else
        {
            if(CanPlace(pos, mouseItem.GetItemType().GetSize(), out overlapTime))
            {
                TryRemovePlace(pos,currentCell.GetLocation() , out currentItem, out currentAmount);
            }
            else
            {
                if(overlapTime <= 1)
                {
                    if(currentCell.GetItemType() == mouseItem.GetItemType())
                    {                       
                        int maxStack = currentCell.GetItemType().GetMaxStack();                      
                        if(currentCell.amount >= maxStack) // 얘는 이미 다 찼어
                        {
                            //int temp = mouseItem.amount;
                            //mouseItem.amount = currentCell.amount;
                            //currentCell.amount = temp;
                            mouseItem.amount = mouseItem.amount ^ currentCell.amount;
                            currentCell.amount = mouseItem.amount ^ currentCell.amount;
                            mouseItem.amount = mouseItem.amount ^ currentCell.amount;
                            mouseItem.SetAmount(mouseItem.amount);
                            currentCell.SetAmount(currentCell.amount);

                        }
                        else // 그리고 아직 덜 찬 상태였다!
                        {
                            currentCell.amount += mouseItem.amount;
                            // 넘은양   둘중 더 큰 거      마이너스가 되면(안넘었으면)  0인 거지
                            int overAmount = Mathf.Max(0, currentCell.amount - maxStack);
                            mouseItem.amount = overAmount;
                            currentCell.amount -= overAmount;
                            mouseItem.SetAmount(mouseItem.amount);
                            currentCell.SetAmount(currentCell.amount);
                            
                        }
                    }
                    else
                    {
                        currentCell = CheckItemRoot(pos, mouseItem.GetItemType().GetSize());
                        if(currentCell != null)
                        {
                            TryRemovePlace(pos, currentCell.GetLocation(), out currentItem, out currentAmount);
                        };
                    }
                }
            }
        }
        if(mouseItem.amount <= 0)
        {
            mouseItem.SetItem(Define.Item.None, 0);
        }
    }

    bool TryRemovePlace(Vector2Int pos, Vector2Int currentPos, out Define.Item currentItem, out int currentAmount)
    {
        currentItem = SubItem(currentPos, out currentAmount);
        bool result = PutItem(pos, mouseItem.GetItemType(), mouseItem.amount);
        if(result == false)
        {
            PutItem(currentPos, currentItem, currentAmount);
        }
        else
        {
            mouseItem.SetItem(currentItem, currentAmount);
        };
        return result;
    }

    /// <summary> 아이템 밀어넣기 시도 </summary>
    bool PutItem(Vector2Int position, Define.Item item, int amount)
    {
        Vector2Int size = item.GetSize();//해당 아이템의 사이즈 가져오기
        int overlap;//겹쳐진 횟수
        if (CanPlace(position, size, out overlap) == true) //해당 좌표에 해당 사이즈의 아이템을 밀어 넣을  수 있으면
        {
            infoArray[position.y, position.x].SetItem(item, amount); //y,x 좌표에 아이템 밀어넣음
            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (x == 0 && y == 0) continue;

                    //첫번째 칸 루트를 제외한 나머지 칸 루트들은 y,x를 바라보게 지정
                    infoArray[position.y + y, position.x + x].SetRoot(infoArray[position.y, position.x]);
                }
            }
            return true;
        };
        return false;
    }
    /// <summary> 해당된 셀의 아이템 빼서 리턴</summary>
    Define.Item SubItem(Vector2Int position, out int amount) //셀 좌표, 아이템 개수
    {
        CellInfo selectedInfo = CheckItemRoot(position);//해당 좌표 셀의 루트 CellInfo 가져옴
        Vector2Int rootLocation = selectedInfo.GetLocation(); //루트 좌표는 해당 좌표 셀의 루트의 좌표이다

        amount = selectedInfo.amount; //아이템 개수
        Define.Item result = selectedInfo.GetItemType();//해당 셀의 아이템 타입 가져옴


        if (result == Define.Item.None) //가져온 아이템 타입이 None이면
        {
            return result; // None 반환 아무것도 없음
        }
        else //가져온 아이템 타입이 있으면
        {     
            Vector2Int size = result.GetSize(); //해당 아이템 사이즈를 확장메서드에서 가져옴
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    infoArray[rootLocation.y + y, rootLocation.x + x].Clear(); //아이템 사이즈 범위만큼 비워줌
                }
            }            
        }
        return result; //해당 셀의 아이템 타입 반환
    }

    /// <summary> 해당 좌표 셀의 아이템 타입 반환</summary>
    Define.Item CheckItem(Vector2Int position)
    {
        return CheckItemRoot(position).GetItemType();
    }

    /// <summary> 해당된 칸의 루트를 체크해서 나 자신 CellInfo나 기준점 CellInfo반환</summary>
    CellInfo CheckItemRoot(Vector2Int position)
    {
        return infoArray[position.y, position.x].GetRoot(); //나 자신 반환하거나, 기준점 반환하거나
    }

    CellInfo CheckItemRoot(Vector2Int position, Vector2Int size)
    {
        if (position.x < 0 || position.y < 0 || position.x + size.x > width || position.y + size.y > height) return null;

        for (int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                if(infoArray[position.y + y, position.x + x].GetItemType() != Define.Item.None)
                {
                    return infoArray[position.y + y, position.x + x];
                };
            };
        };

        return infoArray[position.y,position.x];
    }

    /// <summary> 루트를 따라서 하위 셀들 하이라이트 같이 해주는 메서드</summary>
    void CellHighlight(Vector2Int position, bool isHighright)
    {
        CellInfo rootCellInfo = CheckItemRoot(position);
        Define.Item CellItem = CheckItem(position); //해당 좌표의 아이템 받아옴
        Vector2Int size = CellItem.GetSize(); // 아이템 사이즈 받아옴
        if (isHighright)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    //아이템 사이즈만큼 하이라이트 해줌
                    infoArray[rootCellInfo.GetLocation().y + y, rootCellInfo.GetLocation().x + x].SetColor(CellInfo.highlightColor); 
                }
            }
        }
        else
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    //아이템 사이즈만큼 원래 색상으로 돌려줌
                    infoArray[rootCellInfo.GetLocation().y + y, rootCellInfo.GetLocation().x + x].CalculateColor();
                }
            }
        }
    }   
}


/*
 * 
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
 * 
 * 
 * 마우스가 올려져 있는 칸의 루트 확인, 
 * 루트의 아이템 타입의 사이즈 받아옴, 
 * 사이즈 개수만큼 하이라이트 해줌,
 * 지금 마우스 말고 직전에 있었던 마우스도 체크해야함 그래야 원래 색깔로 돌려줌, 
 * 직전과 지금 마우스 위치 다르면 직전은 풀어줘야함
 */

