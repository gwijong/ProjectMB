using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalFight : MonoBehaviour
{
    public GameObject[] enemys;
    public Transform waitPos;
    Vector3[] spawnPos;
    private void Start()
    {
        spawnPos = new Vector3[enemys.Length];
        for (int i = 0; i < enemys.Length; i++)
        {
            spawnPos[i] = enemys[i].transform.position;
            enemys[i].GetComponent<NavMeshAgent>().enabled = false;
            enemys[i].transform.position = waitPos.position+ Vector3.up;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Define.Layer.Player)
        {
            for(int i = 0; i< enemys.Length; i++)
            {
                enemys[i].transform.position = spawnPos[i];
                enemys[i].GetComponent<NavMeshAgent>().enabled = true;            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (int)Define.Layer.Player)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponent<NavMeshAgent>().enabled = false;
                enemys[i].transform.position = waitPos.position + Vector3.up;
            }
        }
    }
}
