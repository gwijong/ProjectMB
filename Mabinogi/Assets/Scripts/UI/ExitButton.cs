using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 게임 종료 버튼 </summary>
public class ExitButton : MonoBehaviour
{
    /// <summary> 게임 종료 </summary>
    public void Exit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        Application.Quit();
    }
}
