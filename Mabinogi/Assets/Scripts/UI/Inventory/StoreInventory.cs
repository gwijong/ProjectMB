using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StoreInventory : Inventory
{
    /// <summary> 구매 UI 프리팹 </summary>
    public GameObject buyUI;
    /// <summary> 판매 UI 프리팹 </summary>
    public GameObject sellUI;
    /// <summary> 구매 UI 인스턴스 </summary>
    GameObject buyInstance;
    /// <summary> 판매 UI 인스턴스 </summary>
    GameObject sellInstance;

    public Define.Item item;

    ItemData[] itemData; //아이템 데이터들 가져옴

    bool isSet = false;
    private void OnEnable()
    {
        if (FindObjectOfType<PlayerController>().target != null)
        {
            SellList sellList;
            if (FindObjectOfType<PlayerController>().target.TryGetComponent(out sellList) == true && isSet == false)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        infoArray[j, i].Clear();
                    }
                }
                sellList = FindObjectOfType<PlayerController>().target.GetComponent<SellList>();
                for (int i = 0; i < sellList.sellItemList.Count; i++)
                {
                    AddItem(sellList.sellItemList[i], 1);
                }
                isSet = true;
            }
        }
    }
    private void OnDisable()
    {
        isSet = false;
        for (int i = 0; i<width; i++)
        {
            for(int j = 0; j<height; j++)
            {
                infoArray[j, i].SetItem(Define.Item.None, 0);
            }
        }
    }

    void OnDestroy()
    {
        inventoryList.Remove(this);
    }  

    protected override void Start()
    {
        base.Start();
        store = this;
        playerInventoryList.Remove(this);
        buyInstance = CreateUI(buyInstance, buyUI); //구매창 인스턴스 생성
        sellInstance = CreateUI(sellInstance, sellUI); //판매창 인스턴스 생성
        itemData = GameManager.itemManager.data; //아이템 데이터들 가져옴
    }

    /// <summary> 구매, 판매 UI 게임오브젝트 생성 </summary>
    GameObject CreateUI(GameObject go,GameObject prefab)
    {
        if (go == null)//UI가 없으면
        {
            go = Instantiate(prefab);//UI생성
            go.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform);//소지품창의 자식으로 설정
            Image image = go.GetComponent<Image>();//이미지 컴포넌트 가져오기
            image.rectTransform.pivot = new Vector2(0, 1); // UI 중심점을 0,1로 맞춤
            go.SetActive(false);//UI를 일단 꺼둠
            return go;
        }
        return null;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if(buyInstance.activeSelf==true || sellInstance.activeSelf == true) //구매창이나 판매창이 활성화되어 있으면
        {
            if (inpo.activeSelf)
            {
                inpo.SetActive(false);//정보창 꺼줌
            }
        }
        CloseUI(buyInstance); //마우스 커서가 UI를 벗어난 상태에서 마우스 입력 들어오면 구매창 닫기
        CloseUI(sellInstance); //마우스 커서가 UI를 벗어난 상태에서 마우스 입력 들어오면 판매창 닫기
    }

    /// <summary> 마우스 커서가 UI를 벗어난 상태면 구매, 판매창 닫기 </summary>
    void CloseUI(GameObject ui)
    {
        if(ui == null)
        {
            return;
        }
        //마우스 커서가 판매창을 벗어난 상태에서
        if (Input.mousePosition.x > ui.transform.position.x + 140
            || Input.mousePosition.x < ui.transform.position.x - 40
            || Input.mousePosition.y > ui.transform.position.y + 30
            || Input.mousePosition.y < ui.transform.position.y - 140)
        {
            if (Input.GetMouseButtonDown(0)) //마우스 좌클릭하면
            {
                ui.SetActive(false); //구매, 판매창 닫기
            }
        }
    }

    /// <summary> 마우스 좌클릭 </summary>
    public override void LeftClick(Vector2Int pos)
    {
        if (mouseItem.GetItemType() == Define.Item.None)//마우스 커서가 집고있는 아이템이 없으면
        {
            CellInfo rootCellInfo = CheckItemRoot(pos);//마우스 커서가 위치한 셀의 아이템의 루트를 가져오기 시도
            if ((rootCellInfo.GetItemType() != Define.Item.None)) //마우스 커서가 위치한 셀에 아이템이 존재하면
            {
                item = rootCellInfo.GetItemType();

                if(Input.GetKey(KeyCode.LeftControl))
                {
                    Buy();
                }
                else
                {
                    buyInstance.SetActive(true);
                    buyInstance.transform.position = Input.mousePosition; //UI를 마우스 커서 좌표로 이동

                    Text[] text = buyInstance.GetComponentsInChildren<Text>(); //자식 오브젝트의 텍스트 컴포넌트들을 가져온다

                    for (int i = 0; i < itemData.Length; i++) //스크립터블오브젝트 길이만큼 반복
                    {
                        if (i == (int)rootCellInfo.GetItemType() - 1) //아이템 데이터의 번호와 마우스 커서의 아이템타입과 같으면 찾기 성공
                        {
                            text[0].text = itemData[i].ItemName; //0번 텍스트컴포넌트의 텍스트를 아이템 이름으로 바꾼다
                            text[1].text = "가격 : " + itemData[i].SalePrice.ToString() + "Gold";//1번 텍스트컴포넌트의 텍스트를 가격으로 바꾼다
                        }
                    }
                };
            }

        }
        else //마우스가 집고있는 아이템이 있으면
        {
            sellInstance.SetActive(true);
            sellInstance.transform.position = Input.mousePosition; //UI를 마우스 커서 좌표로 이동

            Text[] text = sellInstance.GetComponentsInChildren<Text>(); //자식 오브젝트의 텍스트 컴포넌트들을 가져온다

            for (int i = 0; i < itemData.Length; i++) //스크립터블오브젝트 길이만큼 반복
            {
                if (i == (int)mouseItem.GetItemType() - 1) //아이템 데이터의 번호와 마우스 커서의 아이템타입과 같으면 찾기 성공
                {
                    text[0].text = itemData[i].ItemName; //0번 텍스트컴포넌트의 텍스트를 아이템 이름으로 바꾼다
                    text[1].text = "가격 : " + (itemData[i].SalePrice * mouseItem.amount).ToString() + "Gold";//1번 텍스트컴포넌트의 텍스트를 가격으로 바꾼다
                }
            }
        }
    }

    //우클릭은 좌클릭과 똑같다
    public override void RightClick(Vector2Int pos)
    {
        LeftClick(pos); //좌클릭 메서드 실행
    }

    public void Buy()
    {
        if (PlayerController.controller.playerCharacter.gold >= item.GetItemData().SalePrice)//남은 돈이 물품 가격보다 크면
        {
            PlayerController.controller.playerCharacter.gold -= item.GetItemData().SalePrice;//물품 가격만큼 빼주기
            //invenList에는 상점 인벤토리를 제외한 개인 소지품창만 남음
            GetItem(item, 1); //상점에서 마지막으로 마우스 클릭한 아이템 생성해서 인벤토리에 추가
        }
    }

    public void Sell()
    {
        PlayerController.controller.playerCharacter.gold += mouseItem.GetItemType().GetItemData().SalePrice * mouseItem.amount;//남은 돈에 판매가격 더함
        mouseItem.Clear(); //마우스가 집고있는 아이템 비움
    }

    /// <summary> 구매, 판매창 활성화하고 아이템데이터에서 정보 뽑아와 설정 </summary>
    GameObject SetUI(GameObject go)
    { 
        go.transform.position = Input.mousePosition; //UI를 마우스 커서 좌표로 이동

        Text[] text = go.GetComponentsInChildren<Text>(); //자식 오브젝트의 텍스트 컴포넌트들을 가져온다

        for (int i = 0; i < itemData.Length; i++) //스크립터블오브젝트 길이만큼 반복
        {
            if (i == (int)mouseItem.GetItemType()-1) //아이템 데이터의 번호와 마우스 커서의 아이템타입과 같으면 찾기 성공
            {
                Debug.Log(mouseItem.GetItemType() - 1);
                text[0].text = itemData[i].ItemName; //0번 텍스트컴포넌트의 텍스트를 아이템 이름으로 바꾼다
                text[1].text = "가격 : " + itemData[i].SalePrice.ToString() + "Gold";//1번 텍스트컴포넌트의 텍스트를 가격으로 바꾼다
            }
        }
        return go;
    }
}

