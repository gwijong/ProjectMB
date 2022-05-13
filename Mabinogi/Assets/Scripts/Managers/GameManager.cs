using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 게임 내 한개만 있는 매니저 </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> 게임 내 한개만 있는 매니저 </summary>
    public static GameManager manager; //게임매니저 스태틱 딱 하나
    /// <summary> 업데이트 매니저 객체 </summary>
    UpdateManager _update = new UpdateManager(); //업데이트 매니저 객체 생성
    /// <summary> 업데이트 매니저 읽기전용 프로퍼티 </summary>
    public static UpdateManager update { get { return manager._update; } } //업데이트 매니저
    /// <summary> 사운드 매니저 자동구현 프로퍼티  </summary>
    public static SoundManager soundManager { get; private set; } //사운드 매니저
    /// <summary> NPC사운드 매니저 자동구현 프로퍼티  </summary>
    public static NPCSoundManager npcSoundManager { get; private set; } //사운드 매니저
    /// <summary> 아이템 매니저 자동구현 프로퍼티  </summary>
    public static ItemManager itemManager { get; private set; } //업데이트 매니저
    void Awake() 
    {
        soundManager = GetComponent<SoundManager>(); //사운드매니저 컴포넌트 할당
        itemManager = GetComponent<ItemManager>(); //아이템매니저 컴포넌트 할당
        npcSoundManager = GetComponent<NPCSoundManager>(); //NPC사운드매니저 컴포넌트 할당
        //싱글톤 체크
        if (manager == null) //매니저가 없으면
        {
            manager = this;  //이 GameManager 컴포넌트가 매니저다
        }
        else //매니저기 이미 있으면
        {
            Destroy(gameObject);//이 오브젝트를 파괴한다.
        }
    }

    /// <summary> 이 프로젝트에 딱 하나 있는 업데이트 메서드 </summary>
    private void Update()
    {
        update.OnUpdate();//모든 OnUpdate 메서드가 실행됨
    }
}
