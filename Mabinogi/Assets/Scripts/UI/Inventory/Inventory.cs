using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary> 셀 정보 데이터 컨테이너 </summary>
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
    public Text amountText = null;
    /// <summary> 인벤토리 안에서 셀 위치 </summary>
    Vector2Int location;
    /// <summary> 아이템 창 칸의 평소 색상 </summary>
    static Color normalColor = new Color(0.9f, 0.9f, 0.9f);
    /// <summary> 뭔가 아이템이 들어있을 때 어두운 컬러 </summary>
    static Color filledColor = new Color(0.7f, 0.7f, 0.7f);
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
        Vector2Int itemSize = wantItem.GetSize(); //아이템의 사이즈 가져옴 ex)2*2
        itemType = wantItem; //셀의 아이템 타입을 원하는 아이템 타입으로 바꿈
        itemImage.sprite = wantItem.GetItemImage(); //셀의 아이템 이미지를 원하는 아이템 이미지로 바꿈
        if (itemImage.sprite == null) //이미지 스프라이트가 없으면
        {
            itemImage.color = Color.clear; //이미지를 투명하게 설정
        }
        else //이미지 스프라이트가 있으면
        {
            itemImage.color = Color.white; //온전한 색상 표시
        }
        itemImage.transform.localScale = new Vector3(itemSize.x, itemSize.y, 1);//아이템 이미지를 아이템 사이즈만큼 키움
        CalculateColor(); //아이템이 들어간 칸의 배경 색상 지정
        SetAmount(wantAmount); //셀의 아이템 개수를 원하는 아이템 개수 지정
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
        if ((root == null || root == this) && itemType == Define.Item.None == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary> 개수가 0이면 "" 처리함 </summary>
    public void SetAmount(int wantAmount)
    {
        amount = wantAmount;
        //amountText.text = wantAmount > 0 ? wantAmount.ToString() : ""
        if (wantAmount > 0)
        {
            amountText.text = wantAmount.ToString();
        }
        else
        {
            amountText.text = "";
        }
    }

    /// <summary> 개수를 더해서 남은 걸 리턴 </summary>
    public int AddAmount(int wantAmount)
    {
        amount += wantAmount;  //루트 아이템의 갯수에 줍는 아이템 갯수 더함                                                    
        int overAmount = Mathf.Max(0, amount - itemType.GetMaxStack()); //아이템 겹치기 초과치 계산
        amount -= overAmount;
        SetAmount(amount); //마우스 아이템 텍스트 갱신

        return overAmount;
    }

    //아이템 소모 시 빼는 메서드
    /// <summary> 현재 개수에서 원하는 개수만큼 뺌 </summary>
    public int SubAmount(int wantAmount)
    {
        int result = Mathf.Min(wantAmount, amount);//내가 가지고 있는 것보다 초과해서 뺄 수는 없음

        amount -= result;
        SetAmount(amount); //마우스 아이템 텍스트 갱신

        return result;
    }

    /// <summary> 칸을 비움. root 를 나 자신 셀로 바꾸고 아이템을 none으로 바꿈 </summary>
    public void Clear()
    {
        root = this;
        SetItem(Define.Item.None,0);
    }

    
}

public class Inventory : MonoBehaviour
{
    /// <summary> 소지품창 넓이 </summary>
    public int width;
    /// <summary> 소지품창 높이 </summary>
    public int height;
    /// <summary> 아이템창 칸들의 시작 기준점</summary>
    public RectTransform cellAnchor;
    /// <summary> 아이템창 각 칸 프리팹</summary>
    public GameObject cell;
    /// <summary> 마우스 커서 따라다니는 칸 프리팹</summary>
    public GameObject mouseCell;

    /// <summary> 마우스 커서 따라다니는 아이템 정보 UI</summary>
    public GameObject information;
    /// <summary> 아이템 정보 UI</summary>
    protected GameObject inpo;

    /// <summary> 아이템 사용 선택지문 프리팹 </summary>
    public GameObject useUI;
    /// <summary> 아이템 사용 선택지문 인스턴스</summary>
    public static GameObject use;

    /// <summary> 인벤토리 창 UI 이미지</summary>
    public GameObject parent;
    /// <summary> 마우스가 올려진 셀 위치</summary>
    public Vector2Int overedCellLocation = new Vector2Int(-1, -1);
    /// <summary> [?,?] 칸의 정보</summary>
    CellInfo[,] infoArray;
    /// <summary> 마우스가 집고있는 아이템</summary>
    public static CellInfo mouseItem { get; private set; }
    /// <summary> 마우스가 집고있는 아이템 좌표 </summary>
    public static RectTransform mouseItemPos;

    /// <summary> 사용창 아이템</summary>
    public static CellInfo useItem;
    /// <summary> 소지품창의 좌측 위 </summary>
    public RectTransform leftUp; 
    /// <summary> 소지품창의 우측 아래 </summary>
    public RectTransform rightDown;
    /// <summary> 게임 내 모든 소지품창 리스트 </summary>
    public static List<Inventory> inventoryList = new List<Inventory>();
    public static List<Inventory> playerInventoryList = new List<Inventory>();
    public static StoreInventory store { get; protected set; }
    public Character owner;

    /// <summary> 인벤토리 소멸자 </summary>
    void OnDestroy()
    {
        inventoryList.Remove(this);
        playerInventoryList.Remove(this);
    }
    protected virtual void Start()
    {
        if (mouseItem == null) //마우스가 집고있는 아이템이 없으면 아래에서 할당해줌
        {
            GameObject currentCell = Instantiate(mouseCell); //마우스 계속 따라다니는 빈 셀 지정
            currentCell.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform); //마우스 따라다니는 셀의 부모를 인벤토리로 지정
            currentCell.tag = "MouseCell"; //마우스 셀의 태그를 지정
            mouseItemPos = currentCell.GetComponent<RectTransform>(); //마우스 셀의 RectTransform 대입
            mouseItem = new CellInfo(new Vector2Int(0,0)); //마우스아이템 인스턴스 생성
            mouseItem.amountText = currentCell.GetComponentInChildren<Text>();//셀에서 텍스트 컴포넌트 가져오기
            mouseItem.buttonImage = currentCell.GetComponent<Image>(); //셀에서 버튼 이미지 컴포넌트 가져오기
            mouseItem.itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//셀에서 아이템 이미지 컴포넌트 가져오기
            mouseItem.itemImage.rectTransform.pivot = Vector2.zero; // 마우스따라다니는 아이템 이미지 중심점을 0,0로 맞춤
            mouseItem.SetItem(Define.Item.None,0);//마우스가 쥐고 있는 아이템을 비워주고 0개로 설정
            mouseItem.buttonImage.enabled = false; //버튼 이미지(아이템 칸) 끔
            currentCell.GetComponent<Button>().enabled = false; //버튼 컴포넌트 끔
        }

        if(inpo == null)
        {
            inpo = Instantiate(information); //마우스 계속 따라다니는 정보창 지정
            inpo.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform); //정보창을 인벤토리의 자식오브젝트로 옮김
            Image inpoImage = inpo.GetComponent<Image>();//정보창 이미지 컴포넌트 가져오기
            inpoImage.rectTransform.pivot = new Vector2(0,1); // 정보창 중심점을 0,1로 맞춤
            inpo.SetActive(false);//일단 끄고 필요할때 켜줌
        }

        if (use == null)
        {
            use = Instantiate(useUI); //사용창 생성
            use.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform); //사용창을 인벤토리의 자식오브젝트로 옮김
            Image useImage = use.GetComponent<Image>();//아이템 사용창 이미지 컴포넌트 가져오기
            useImage.rectTransform.pivot = new Vector2(0, 1); // 정보창 중심점을 0,1로 맞춤
            use.SetActive(false);//일단 끄고 필요할때 켜줌
        }

        inventoryList.Add(this);
        playerInventoryList.Add(this);

        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;

        // 인벤토리 칸 만들기
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

                infoArray[y, x].CalculateColor();//칸이 비어있으면 빈 색상, 칸이 차있으면 어두운 색상
                
                RectTransform childRect = currentCell.GetComponent<RectTransform>(); //현재 셀의 RectTransform 가져옴
                Vector2 pos = cellAnchor.anchoredPosition + new Vector2((48 * x), (-48 * y));//각 셀들의 위치를 시작 기준점에서 48픽셀 간격으로 배치함
                childRect.anchoredPosition = pos;//현재 셀의 앵커 포지션을 시작 기준점에서 48 * x, -48 * y 간격으로 배치함
            }
        }
        foreach (CellInfo current in infoArray) //모든 아이템 칸들 순회
        {
            current.itemImage.transform.SetParent(parent.transform); //아이템 이미지를 인벤토리 창의 하위 오브젝트로 지정
            current.amountText.transform.SetParent(parent.transform);//아이템 텍스트를 인벤토리 창의 하위 오브젝트로 지정
            current.SetItem(Define.Item.None, 0); //인벤토리 창 칸들을 비워줌
        }    
    }
    
    /// <summary> 반복 실행 </summary>
    protected virtual void OnUpdate()
    {
        if (mouseItemPos == null) //마우스 따라다니는 셀의 좌표가 null이면 리턴
        {
            return;
        }
        mouseItemPos.position = Input.mousePosition; //마우스 따라다니는 셀의 좌표를 마우스 좌표로 계속 대입 갱신
        Vector3 mousePosition = GetMousePositionFromInventory(); //마우스 위치를 이 인벤토리 기준으로 확인함

        inpo.GetComponent<RectTransform>().position = Input.mousePosition; //아이템 정보창을 마우스 좌표를 항상 따라다니게 함

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0) //마우스 커서가 0,0 이상이면
        {
            CellHighlight(overedCellLocation, false); //기존 마우스 커서가 있는 아이템의 버튼 색상을 기본값으로 바꿔줌
        }

        if(CheckOutBoundary())
        {
            inpo.SetActive(false);
            //마우스 커서가 소지품창을 벗어난 상황에서 마우스 좌클릭을 누르면

            overedCellLocation = Vector2Int.one * -1;// -1번칸은 없으므로 마우스가 올려진 곳이 없다는 뜻임
        }
        else if(CheckMouseInside()) //마우스 커서가 소지품 창 안에 있는 경우
        {
            overedCellLocation.x = (int)mousePosition.x / 48; //마우스 좌표에서 칸 넓이인 48로 나눠서 정수로 변환
            overedCellLocation.y = (int)mousePosition.y / 48; //마우스 좌표에서 칸 높이인 48로 나눠서 정수로 변환
            
            CellHighlight(overedCellLocation,true); //마우스 커서가 있는 아이템의 버튼 색상을 온전한 색상으로 바꿔줌

            if (Input.GetMouseButtonDown(1))//마우스 우클릭하면
            {
                RightClick(overedCellLocation);
            }   
            if (Input.GetMouseButtonDown(0)) //마우스 좌클릭하면
            {
                LeftClick(overedCellLocation); // 마우스 좌클릭 메서드 실행
            }
            if(use.activeSelf != true)//사용창이 꺼져있으면
            {
                ItemInpo(overedCellLocation, true);//정보 UI 켜줌
            }
        };
        //마우스 커서가 사용창을 벗어난 상태에서
        if (Input.mousePosition.x > use.transform.position.x + 90
            || Input.mousePosition.x < use.transform.position.x - 20
            || Input.mousePosition.y > use.transform.position.y + 20
            || Input.mousePosition.y < use.transform.position.y - 90)
        {
            if (Input.GetMouseButtonDown(0)) //마우스 좌클릭하면
            {
                use.SetActive(false); //사용창 닫기
            }
        }
    }

    /// <summary> 인벤 밖이면 true, 인벤 안이면 false </summary>
    public static bool OutAllInvenBoundaryCheck()
    {
        foreach (Inventory current in inventoryList)
        {
            if (current.InMousePosBoundaryCheck()) //마우스 커서가 인벤토리 안에 있다.
            {
                return false; 
            };
        }
        return true;
    }
    /// <summary> 마우스 좌표가 인벤토리 안에 있으면 true </summary>
    bool InMousePosBoundaryCheck()
    {
        if(Input.mousePosition.x > leftUp.position.x  
            && Input.mousePosition.y < leftUp.position.y 
            && Input.mousePosition.x < rightDown.position.x
            && Input.mousePosition.y > rightDown.position.y
            )
        {
            return true;
        }
        return false;
    }

    /// <summary> 사용창 켜기 </summary>
    void UseUI(Vector2Int position, bool active)
    {
        use.transform.position = Input.mousePosition; //사용 UI를 마우스 커서 좌표로 이동
        CellInfo rootCellInfo = CheckItemRoot(position);//마우스 커서가 위치한 셀의 아이템의 루트를 가져오기 시도
        if (active && (rootCellInfo.GetItemType() != Define.Item.None)) //루트 아이템이 존재하면
        {
            use.SetActive(true); //사용창을 활성화한다
        }
        else//루트 아이템이 존재하지 않으면
        {
            use.SetActive(false);//사용창 비활성화
        }
    }

    /// <summary> 아이템 버리기 </summary>
    public static void DropItem()
    {
        GameManager.itemManager.DropItem(mouseItem.GetItemType(), mouseItem.amount);//바닥에 버릴 아이템 생성
        mouseItem.Clear();//마우스 아이템 비워줌
        use.SetActive(false);//사용창 비활성화
    }

    /// <summary> 아이템 나누기 </summary>
    public void Divide()
    {
        if (useItem.amount <=0) //나눌 아이템이 없으면
        {
            use.SetActive(false);//사용창 비활성화
            return;
        }
        if (useItem.amount == 1) //나눌 아이템이 딱 1개면
        {
            LeftClick(useItem.GetLocation()); //좌클릭
            return;
        }
        if (mouseItem.GetItemType() != Define.Item.None) //마우스 아이템이 존재하면
        {
            PutItem(overedCellLocation, mouseItem.GetItemType(), mouseItem.amount); //해당 셀에 아이템 밀어넣기 시도
            mouseItem.SetItem(Define.Item.None, 0); //마우스 아이템 비워줌
        }
        useItem.SetAmount(useItem.GetRoot().amount - 1); //사용창 아이템의 개수를 한개 빼준다
        mouseItem.SetItem(useItem.GetRoot().GetItemType(), 1); //마우스가 집고 있는 아이템을 사용창 아이템 한개로 설정한다.
        use.SetActive(false);//사용창 비활성화
    }

    /// <summary> 아이템 사용 </summary>
    public void Use()
    {
        useItem.SetAmount(useItem.amount-1); //아이템 개수를 한개 줄임
        useItem.amountText.text = useItem.amount.ToString();
        Vector2Int rootLocation = useItem.GetLocation(); //루트 좌표는 해당 좌표 셀의 루트의 좌표이다
        if (useItem.amount <= 0) //사용한 아이템 개수가 0개가 되면 비워줌
        {
            Vector2Int size = useItem.GetItemType().GetSize(); //해당 아이템 사이즈를 확장메서드에서 가져옴
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    infoArray[rootLocation.y + y, rootLocation.x + x].Clear(); //아이템 사이즈 범위만큼 비워줌
                }
            }
        }
        useItem.GetItemType().Use(); //아이템 사용효과 실행
        use.SetActive(false);//사용창 비활성화
    }
    /// <summary> 아이템 버리기 </summary>
    public void Drop()
    {
        GameManager.itemManager.DropItem(useItem.GetItemType(), useItem.amount); //바닥에 아이템 생성
        Define.Item currentItem = SubItem(useItem.GetLocation(), out useItem.amount); //소지품창에서 버린 아이템 개수만큼 빼서 지움
        use.SetActive(false);//사용창 비활성화
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
                    CellInfo currentRoot = infoArray[position.y + y, position.x + x].GetRoot(); //현재 루트를 가져옴
                    if (!rootList.Contains(currentRoot))//루트 리스트에 현재 루트와 같은 값이 없으면
                    {
                        rootList.Add(currentRoot);//루트 리스트에 현재 루트를 추가한다
                    };
                    result = false;
                };
            };
        };
        OverlapTime = rootList.Count; //겹친 횟수는 루트 리스트의 갯수이다.
        return result; //현재 공간에 아이템 넣을 수 있으면 true 반환
    }

    /// <summary> 마우스 우클릭시 정보창 끄고 사용창 켜줌 </summary>
    public virtual void RightClick(Vector2Int pos)
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        inpo.SetActive(false); 
        UseUI(pos, true);
        useItem = CheckItemRoot(pos);
    }
    /// <summary> 마우스 좌클릭 시 아이템 겹치기, 아이템 옮기기 실행 </summary>
    public virtual void LeftClick(Vector2Int pos)
    {
        //마우스 클릭한 좌표가 null이거나 소지품창의 범위를 벗어난 경우
        if (pos == null || pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        {
            return; //탈출
        }
        if (use.activeSelf)//사용창이 켜져있으면 실행안함
        {
            return;
        }
        int overlapTime = 0; //겹친 횟수
        int currentAmount = 0; //현재 갯수
        Define.Item currentItem = Define.Item.None; //현재 아이템을 일단 비워줌

        CellInfo currentCell = CheckItemRoot(pos); //현재 셀에 마우스 좌표의 루트 대입
        if (mouseItem.GetItemType() == Define.Item.None)// 마우스가 집고 있는 아이템이 없는 경우
        {          
             currentItem = SubItem(pos, out currentAmount); //마우스 좌표 셀의 아이템들 빼준다
             mouseItem.SetItem(currentItem, currentAmount); //마우스가 집고 있는 아이템을 위에서 뺀 만큼 세팅해준다
             GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        }
        else //마우스가 뭔가를 집고 있는 경우
        {
            if (CanPlace(pos, mouseItem.GetItemType().GetSize(), out overlapTime))//넣을 공간이 있는 경우
            {
                TryRemovePlace(pos,currentCell.GetLocation() , out currentItem, out currentAmount); //스왑 시도
            }
            else
            {
                if (overlapTime <= 1)
                {
                    if(currentCell.GetItemType() == mouseItem.GetItemType()) //아이템 종류가 같으면 겹쳐주기 시도
                    {                       
                        int maxStack = currentCell.GetItemType().GetMaxStack(); //현재 아이템 칸의 아이템 종류의 최대 겹치는 갯수 가져옴                     
                        if(currentCell.amount >= maxStack) // 아이템 창 아이템이 최대치로 다 차있는 경우
                        {
                            //mouseItem.amount = mouseItem.amount ^ currentCell.amount;
                            //currentCell.amount = mouseItem.amount ^ currentCell.amount;
                            //mouseItem.amount = mouseItem.amount ^ currentCell.amount;
                            int temp = mouseItem.amount; //마우스 아이템 갯수 임시 저장
                            mouseItem.amount = currentCell.amount; //마우스 아이템 갯수를 소지품창 아이템 갯수로 대입
                            currentCell.amount = temp; //소지품창 아이템 갯수를 마우스 아이템 갯수로 대입
                            mouseItem.SetAmount(mouseItem.amount); //마우스 집고있는 아이템 텍스트 갱신
                            currentCell.SetAmount(currentCell.amount);//인벤토리 아이템 칸 텍스트 갱신

                        }
                        else // 아이템 창 아이템이 덜 차있는 경우
                        {
                            currentCell.amount += mouseItem.amount; //일단 아이템 창 아이템에 마우스 아이템 갯수 더함
                            // 넘은양   둘중 더 큰 거      마이너스가 되면(안넘었으면) 그나마 큰 0이 된다
                            int overAmount = Mathf.Max(0, currentCell.amount - maxStack); //최대치 초과하지 않으면 0, 초과하면 초과한 숫자만큼 overAmount 대입
                            mouseItem.amount = overAmount; //초과치만큼 마우스에 들고 있는다
                            currentCell.amount -= overAmount; //초과치만큼 아이템 창 아이템의 갯수를 빼준다
                            mouseItem.SetAmount(mouseItem.amount); //마우스 아이템 텍스트 갱신
                            currentCell.SetAmount(currentCell.amount); //인벤토리 아이템 텍스트 갱신
                            
                        }
                    }
                    else //아이템 종류가 다르면 집고 있는 아이템과 소지품창 아이템을 스왑해줌
                    {
                        currentCell = CheckItemRoot(pos, mouseItem.GetItemType().GetSize());
                        if(currentCell != null) //아이템이 있으면
                        {
                            TryRemovePlace(pos, currentCell.GetLocation(), out currentItem, out currentAmount); //스왑 시도
                        };
                    }
                }
            }
        }
        if(mouseItem.amount <= 0) //마우스가 쥐고 있는 아이템 개수가 0 이하이면
        {
            mouseItem.SetItem(Define.Item.None, 0); //마우스가 쥐고있는 아이템 비워줌
        }
    }


    /// <summary> 아이템 스왑 시도 </summary>
    bool TryRemovePlace(Vector2Int pos, Vector2Int currentPos, out Define.Item currentItem, out int currentAmount)
    {
        currentItem = SubItem(currentPos, out currentAmount);//현재 좌표에서 현재 개수만큼 빼서 현재 아이템에 대입
        bool result = PutItem(pos, mouseItem.GetItemType(), mouseItem.amount) > 0;//마우스가 집고 있는 아이템을 pos 좌표에 밀어넣기 시도
        if (result == false) //밀어넣기가 실패했다면
        {
            PutItem(currentPos, currentItem, currentAmount);//현재 좌표에 현재 아이템을 현재 개수만큼 밀어넣음
        }
        else //밀어넣기가 성공했다면
        {
            mouseItem.SetItem(currentItem, currentAmount); //마우스가 집고 있는 아이템을 현재 아이템, 현재 개수로 설정
        };
        return result; //밀어넣기 결과 리턴
    }

    /// <summary> 아이템 밀어넣기 시도 </summary>
    public int PutItem(Vector2Int position, Define.Item item, int amount)
    {
        amount = Mathf.Min(amount, item.GetMaxStack());

        Vector2Int size = item.GetSize();//해당 아이템의 사이즈 가져오기
        int overlap;//겹쳐진 횟수
        if (CanPlace(position, size, out overlap) == true) //해당 좌표에 해당 사이즈의 아이템을 밀어 넣을  수 있으면
        {
            infoArray[position.y, position.x].SetItem(item, amount); //y,x 좌표에 아이템 밀어넣음
            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (x == 0 && y == 0) continue; //0,0은 루트이므로 넘김

                    //첫번째 칸 루트를 제외한 나머지 칸 루트들은 y,x를 바라보게 지정
                    infoArray[position.y + y, position.x + x].SetRoot(infoArray[position.y, position.x]);
                }
            }
            return amount;
        };
        return 0;
    }

    /// <summary> 소지품창에서 같은 아이템 찾아서 리스트에 넣음</summary>
    public List<CellInfo> FindItemList(Define.Item item) //아이템 찾는중
    {
        List<CellInfo> result = new List<CellInfo>(); //해당 아이템을 가지고 있는 모든 셀 리턴

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++) //소지품창 넓이만큼 순회
            {
                CellInfo root = infoArray[y, x].GetRoot(); 
                if (root.GetItemType() == item) //소지품창 y,x 칸의 아이템이 찾는 아이템이면
                {
                    if (!result.Contains(root)) //리스트에 똑같은 셀 정보 root가 이미 존재하지 않으면
                    {
                        result.Add(root); //리스트에 셀 정보 root 추가
                    }
                }
            }
        }
        return result; //리스트 반환
    }

    /// <summary> 인벤토리 내의 마우스 좌표</summary>
    Vector3 GetMousePositionFromInventory()
    {
        Vector3 result = Input.mousePosition; //마우스 좌표
        result -= cellAnchor.position; //마우스 좌표에서 셀들의 시작 기준점을 빼줌
        result.y *= -1;//y값이 음수면 햇갈리기 때문에 음수 곱해서 양수로 바꿔줌

        return result;
    }

    /// <summary> 리스트에서 지정한 아이템 개수 모두 더해서 리턴</summary>
    public int GetItemAmount(Define.Item item)
    {
        int result = 0;
        foreach(CellInfo current in FindItemList(item)) //소지품창의 모든 아이템 셀 정보 만큼 반복
        {
            result += current.amount; //소지품창 안의 찾는 아이템의 아이템 개수 리턴
        }

        return result;
    }

    /// <summary> 중복된 아이템 밀어넣고 남은 아이템 숫자 반환</summary>
    public static int GetItem(Define.Item item, int amount)
    {
        for(int i =0; i< playerInventoryList.Count; i++)
        {
            amount = playerInventoryList[i].AddItem(item, amount);
        }
        return amount;
    }  

    public int AddItem(Define.Item item, int amount)
    {
        List<CellInfo> sameList = FindItemList(item);
        for(int i = 0; i < sameList.Count; i++)
        {
            amount = sameList[i].AddAmount(amount); //넣고 남은값

            if (amount <= 0) //남은 아이템이 0보다 작으면
            {
                return 0; //트루 반환
            }
        }
        if (amount >= 1)//위에서 중복돤 아이템들 겹치고 나서 남은 아이템이 있으면
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //밀어넣은 아이템 개수만큼 amount 빼줌
                    amount -= PutItem(Vector2Int.zero + new Vector2Int(x, y), item, amount); 
                    
                    if (amount <= 0) return 0;//남은 아이템이 0과 같거나 작으면 트루 반환                       
                }
            }
        }
        return amount; //못 밀어넣었으면 반환
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

    /// <summary> 인벤토리 바깥쪽인지 체크</summary>
    static bool CheckOutBoundary()
    {
        foreach(Inventory current in inventoryList)
        {
            if(current.CheckMouseInside())
            {
                return false; //뭐 하나라도 인벤토리 안에 있다!
            };
        }

        return true; //끝까지 갔는데 리턴이 한 번도 안 걸림!
    }
    /// <summary> 마우스 커서가 인벤토리 안에 있는지 체크</summary>
    bool CheckMouseInside()
    {
        Vector3 position = GetMousePositionFromInventory();
        return //벗어나면 false 안 벗어나면 true
            (!
                (
                position.x < 0 || position.x > width * 48 // 마우스좌표의 x가 0보다 작거나 마우스 좌표가 소지품창 칸 넓이를 넘어가거나
                               ||
                position.y < 0 || position.y > height * 48 // 마우스좌표의 y가 0보다 작거나 마우스 좌표가 소지품창 칸 길이를 넘어가거나
                )
            );
    }

    /// <summary> 해당 좌표 셀의 아이템 타입 반환</summary>
    Define.Item CheckItem(Vector2Int position)
    {
        return CheckItemRoot(position).GetItemType();
    }

    /// <summary> 해당된 칸의 루트를 체크해서 나 자신 CellInfo나 기준점 CellInfo반환</summary>
    public CellInfo CheckItemRoot(Vector2Int position)
    {
        return infoArray[position.y, position.x].GetRoot(); //나 자신 반환하거나, 기준점 반환하거나
    }

    /// <summary> 해당 좌표에서 아이템 사이즈만큼의 범위에 아이템이 있는지 체크해서 아이템이 있으면 해당 아이템 정보 돌려줌</summary>
    CellInfo CheckItemRoot(Vector2Int position, Vector2Int size)
    {
        //소지품창 범위 넘어가면 null 리턴
        if (position.x < 0 || position.y < 0 || position.x + size.x > width || position.y + size.y > height) return null;

        for (int x = 0; x < size.x; x++)
        {   //아이템 사이즈만큼 아이템 칸 순회
            for(int y = 0; y < size.y; y++) 
            {
                if(infoArray[position.y + y, position.x + x].GetItemType() != Define.Item.None) //셀 안에 아이템이 있으면
                {
                    return infoArray[position.y + y, position.x + x]; //셀 정보 반환
                };
            };
        };

        return infoArray[position.y,position.x]; //빈 셀 정보 반환
    }

    /// <summary> 루트를 따라서 하위 셀들 하이라이트 같이 해주는 메서드</summary>
    void CellHighlight(Vector2Int position, bool isHighright)
    {
        CellInfo rootCellInfo = CheckItemRoot(position);
        Define.Item CellItem = CheckItem(position); //해당 좌표의 아이템 받아옴
        Vector2Int size = CellItem.GetSize(); // 아이템 사이즈 받아옴
        if (isHighright)//하이라이트 해줘야 하면
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
        else//하이라이트 안해줘야 하면
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
    /// <summary> 정보 UI 활성화 메서드</summary>
    void ItemInpo(Vector2Int position, bool active)
    {
        CellInfo rootCellInfo = CheckItemRoot(position);//마우스 커서가 위치한 셀의 아이템의 루트를 가져오기 시도
        if(active && (rootCellInfo.GetItemType() != Define.Item.None)) //루트 아이템이 존재하면
        {
            ItemData[] itemData = GameManager.itemManager.data;
            inpo.SetActive(true); //정보창을 활성화한다
            Text[] text = inpo.GetComponentsInChildren<Text>(); //정보창의 자식 오브젝트의 텍스트 컴포넌트들을 가져온다
            for(int i = 0; i< itemData.Length+1; i++) //스크립터블오브젝트 길이만큼 반복
            {
               if(i == (int)rootCellInfo.GetItemType())
                {
                    text[0].text = itemData[i-1].ItemName; //0번 텍스트컴포넌트의 텍스트를 데이터의 아이템 이름으로 바꾼다
                    text[1].text = itemData[i-1].Description;//1번 텍스트컴포넌트의 텍스트를 데이터의 아이템 설명으로 바꾼다
                }
            }
            
        }
        else//루트 아이템이 존재하지 않으면
        {
            inpo.SetActive(false);//정보창 비활성화
        }
        if(active == false)
        {
            inpo.SetActive(false); //정보창 비활성화
        }
    }
}


