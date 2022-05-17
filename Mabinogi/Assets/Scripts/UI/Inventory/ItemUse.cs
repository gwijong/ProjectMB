using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public void Use()
    {
        FindObjectOfType<Inventory>().Use();
    }
    public void Divide()
    {
        FindObjectOfType<Inventory>().Divide();
    }

    public void Drop()
    {
        FindObjectOfType<Inventory>().Drop();
    }
}
