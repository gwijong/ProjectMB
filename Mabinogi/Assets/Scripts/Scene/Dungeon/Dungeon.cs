using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int progress = -1;
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
            Debug.Log("¡¯¿‘");
            gate[progress].GetComponent<BoxCollider>().enabled = false;
            leftDoor[progress].SetActive(false);
            rightDoor[progress].SetActive(false);
        }
    }

    public void Spawn()
    {
        enemy = Instantiate(enemys[progress]);
        enemy.transform.position = spawnPos[progress].position;        
        progress++;
    }
}
