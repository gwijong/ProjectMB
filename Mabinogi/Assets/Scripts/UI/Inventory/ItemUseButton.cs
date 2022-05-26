using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseButton : MonoBehaviour
{
    /// <summary> 아이템 사용 버튼 </summary>
    public void Use()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.eatfood);//버튼 다운 효과음
        FindObjectOfType<Inventory>().Use();
    }
    /// <summary> 아이템 나누기 버튼 </summary>
    public void Divide()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        FindObjectOfType<Inventory>().Divide();
    }
    /// <summary> 아이템 버리기 버튼 </summary>
    public void Drop()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        FindObjectOfType<Inventory>().Drop();
    }
}
