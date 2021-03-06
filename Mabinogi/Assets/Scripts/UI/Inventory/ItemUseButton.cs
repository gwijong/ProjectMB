using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 아이템 우클릭 시 나오는 버튼들 </summary>
public class ItemUseButton : MonoBehaviour
{
    /// <summary> 아이템 사용 버튼 </summary>
    public void Use()
    {
        FindObjectOfType<PlayerInventory>().Use();
    }
    /// <summary> 아이템 나누기 버튼 </summary>
    public void Divide()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        FindObjectOfType<PlayerInventory>().Divide();
    }
    /// <summary> 아이템 버리기 버튼 </summary>
    public void Drop()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        FindObjectOfType<PlayerInventory>().Drop();
    }
}
