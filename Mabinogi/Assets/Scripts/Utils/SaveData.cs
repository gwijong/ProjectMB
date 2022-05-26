using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
[Serializable]
public class InvenData
{
    /// <summary> 플레이어의 골드</summary>
    public int gold;
    /// <summary> 해당 [아이템아이디] 아이템의 개수</summary>
    public int[] itemAmount = new int[System.Enum.GetValues(typeof(Define.Item)).Length];
}

public class SaveData : MonoBehaviour
{
    /// <summary> 저장용 데이터컨테이너</summary>
    InvenData invendata;
    /// <summary> 플레이어 인벤토리</summary>
    PlayerInventory playerInventory;

    string readJson;
    InvenData saveFile;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        playerInventory = FindObjectOfType<PlayerInventory>();

        readJson = File.ReadAllText(Application.streamingAssetsPath + "/savefile.txt");
        saveFile = JsonUtility.FromJson<InvenData>(readJson);

        //LoadData();

    }
    /// <summary> 씬 시작 시 실행</summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("LoadData", 0.1f);
    }

    private void LoadData()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++)//아이템 종류만큼 반복
        {
            PlayerInventory.GetItem((Define.Item)(i + 1), saveFile.itemAmount[i]);//각 아이템 종류마다 저장된 개수만큼 소지품창에 추가
        }
        playerInventory.owner.gold = saveFile.gold; //저장한 골드 불러옴
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary> 저장 반복</summary>
    void OnUpdate()
    {
        Save();
    }

    /// <summary> 골드, 아이템 개수 저장</summary>
    void Save()
    {
        invendata = new InvenData();
        int itemId = 0;//현재 찾는 아이템 아이디
        int amount = 0; //현재 찾는 아이템의 총 개수
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++) //아이템 종류만큼 반복
        {
            for (int y = 0; y < playerInventory.height; y++) //소지품창 높이만큼 반복
            {
                for (int x = 0; x < playerInventory.width; x++) //소지품창 넓이만큼 반복
                {
                    if (playerInventory.infoArray[y, x].GetItemType() == (Define.Item)itemId + 1) //인벤토리 칸 아이템 타입이 현재 찾는 아이템 타입과 같으면
                    {
                        amount += playerInventory.infoArray[y, x].amount; //같은 아이템 타입끼리 누적합 해줌
                    }
                }
            }
            invendata.itemAmount[itemId] = amount; //해당 아이템 타입의 누적합한 개수를 저장용 데이터컨테이너에 넣음
            itemId++;
            amount = 0; //다른 아이템 타입의 개수를 저장하기 위해 초기화
            invendata.gold = playerInventory.owner.gold; //플레이어 골드 저장
        }
        string json = JsonUtility.ToJson(invendata);
        string path = Application.streamingAssetsPath + "/savefile.txt";
        File.WriteAllText(path, json);
    }
}
