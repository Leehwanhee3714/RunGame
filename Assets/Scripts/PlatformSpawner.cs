using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int count = 8;

    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn; //랜덤으로 발사 시간 1.25초~2.25초 사이값을 가져오고, 그 다음 발사시점

    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 20f; // x좌표 고정, y좌표 -3.5~1.5 (랜덤)

    private GameObject[] platforms;
    private int currentIndex = 0; 

    private Vector2 poolPosition = new Vector2(0, -25); 
    private float lastSpawnTime;

    void Start()
    {
        platforms = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity); //Quaternion : 회전 안시키게하는거
        }
        lastSpawnTime = 0f; 
        timeBetSpawn = 0f; 
    }

    void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }        //게임 종료

        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);

            platforms[currentIndex].SetActive(false); //Platform 스크립트에 OnEnable땜에 flase해주고 다시 true
            platforms[currentIndex].SetActive(true);

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
