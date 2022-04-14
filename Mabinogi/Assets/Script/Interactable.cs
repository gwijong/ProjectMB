using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractType
{
    None,
    Talk,
    Attack,
    Get,
}


public class Interactable : MonoBehaviour
{
    public virtual InteractType Interact() { return InteractType.None; }//클릭하거나 AI가 쓰는거
}
