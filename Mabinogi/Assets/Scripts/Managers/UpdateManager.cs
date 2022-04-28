using System;
using UnityEngine;

/// <summary> 업데이트 메서드용 클래스 </summary>
public class UpdateManager
{
    /// <summary> 모든 업데이트가 누적되는 액션 </summary>
    public Action UpdateMethod = null; 

    public void OnUpdate()
    {
        if (UpdateMethod != null)
        {
            UpdateMethod.Invoke();
        }
    }
}
