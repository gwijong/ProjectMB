using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 게임 내 한개만 있는 매니저 </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager manager; 

    UpdateManager _update = new UpdateManager(); //업데이트 매니저 객체 생성
    public static UpdateManager update { get { return manager._update; } } //업데이트 매니저 객체 읽기전용 프로퍼티

    public static SoundManager soundmanager { get; private set; } //사운드 매니저
    void Awake() 
    {
        soundmanager = gameObject.GetComponent<SoundManager>(); //사운드매니저 컴포넌트 할당
        //싱글톤 체크
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        update.OnUpdate();
    }
}
