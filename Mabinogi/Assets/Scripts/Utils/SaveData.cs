using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
/// <summary> 인벤토리 안의 골드와 아이템 개수들</summary>
[Serializable]
public class InvenData
{
    /// <summary> 플레이어의 골드</summary>
    public int gold;
    /// <summary> 해당 [아이템아이디] 아이템의 개수</summary>
    public int[] itemAmount = new int[System.Enum.GetValues(typeof(Define.Item)).Length];
}

/// <summary> 소지품 데이터 저장</summary>
public class SaveData : MonoBehaviour
{
    /// <summary> 저장용 데이터컨테이너</summary>
    InvenData invendata;
    /// <summary> 플레이어 인벤토리</summary>
    PlayerInventory playerInventory;
    /// <summary> Json 문자열</summary>
    string readJson;
    /// <summary> 세이브 파일 데이터</summary>
    InvenData saveFile;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        playerInventory = FindObjectOfType<PlayerInventory>();//플레이어 인벤토리
        readJson = File.ReadAllText(Application.streamingAssetsPath + "/savefile.txt"); //세이브 파일 데이터의 문자열
        saveFile = JsonUtility.FromJson<InvenData>(readJson); //세이브 파일 데이터
    }
    /// <summary> 씬 시작 시 실행</summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("LoadData", 0.1f);//씬 로드보다 살짝 늦게 시작해야 소지품 데이터 로드가 제대로 됨
    }

    /// <summary> 소지품 데이터 불러오기</summary>
    private void LoadData()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++)//아이템 종류만큼 반복
        {
            PlayerInventory.GetItem((Define.Item)(i + 1), saveFile.itemAmount[i]);//각 아이템 종류마다 저장된 개수만큼 소지품창에 추가
        }
        playerInventory.owner.gold = saveFile.gold; //저장한 골드 불러옴
    }

    /// <summary> 씬 시작 시 데이터 로드 메서드 더함</summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary> 씬 종료 시 데이터 로드 메서드 뺌</summary>
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
        invendata = new InvenData(); // 소지품 데이터
        int itemId = 0;//현재 찾는 아이템 아이디
        int amount = 0; //현재 찾는 아이템의 총 개수
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++) //아이템 종류만큼 반복
        {
            amount = playerInventory.GetItemAmount((Define.Item)itemId + 1);
            invendata.itemAmount[itemId] = amount; //해당 아이템 타입의 누적합한 개수를 저장용 데이터컨테이너에 넣음
            itemId++;
            invendata.gold = playerInventory.owner.gold; //플레이어 골드 저장
        }
        string json = JsonUtility.ToJson(invendata); //저장되는 문자열
        string path = Application.streamingAssetsPath + "/savefile.txt"; //경로
        File.WriteAllText(path, json); //지정한 경로에 json 문자열 저장
    }
}
