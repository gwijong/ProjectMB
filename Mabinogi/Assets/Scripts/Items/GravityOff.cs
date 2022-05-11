using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Off());
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
}
