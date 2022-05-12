using System;
using UnityEngine;

/// <summary> 업데이트 메서드용 클래스 </summary>
public class UpdateManager
{
    /// <summary> 모든 Update 메서드가 누적되는 액션 </summary>
    public Action UpdateMethod = null;

    /// <summary> GameManager의 Update() 에서 실행됨 </summary>
    public void OnUpdate()
    {
        if (UpdateMethod != null) //UpdateMethod를 구독하는 메서드가 뭔가 존재하면
        {
            UpdateMethod.Invoke(); //업데이트 메서드 실행
        }
    }
}
