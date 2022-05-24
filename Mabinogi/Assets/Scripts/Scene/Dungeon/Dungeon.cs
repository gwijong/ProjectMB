using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dungeon : MonoBehaviour
{
    public int progress = 0;
    public GameObject[] gate;
    public GameObject[] leftDoor;
    public GameObject[] rightDoor;
    public GameObject[] enemys;
    public Transform[] spawnPos;
    List<GameObject> enemyList = new List<GameObject>();
    GameObject enemy = null;
    void Start()
    {
        
    }


    void Update()
    {
        if(enemy!=null && enemy.GetComponent<Character>().die == true)
        {
            gate[progress - 1].GetComponent<BoxCollider>().enabled = false;
            leftDoor[progress - 1].SetActive(false);
            rightDoor[progress - 1].SetActive(false);
        }
    }

    public void Spawn()
    {
        Vector3 pos = spawnPos[progress].position;
        enemy = Instantiate(enemys[progress]);
        enemy.transform.position = pos;
        progress++;
    }
}
