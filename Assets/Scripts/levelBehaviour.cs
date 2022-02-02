using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject springPrefab;
    public GameObject breakPrefab;

    private int platformSpawn = 6;
    private float xPos = 0.0f;
    private float yPos = -4.0f;
    private float higher_platform_yPos = 2.0f;
    private float playerPosY;
   
    private float minRangeX = -2.0f;
    private float maxRangeX = 2.0f;
    private float minRangeY = 1.0f;
    private float maxRangeY = 2.5f;

    private float timeLimit = 5.0f;
    private float escalatedTime;

    private int platformLayer = 9;
    private int springLayer = 10;
    private int breakLayer = 11;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosY = player.transform.position.y;
        SpawnBreak();
    }

    void SpawnPlatform()
    {
        for (int i = 0; i < platformSpawn; i++)
        {
            xPos = Random.Range(minRangeX, maxRangeX);
            GameObject platformGo = Instantiate(platformPrefab, new Vector2(xPos, playerPosY + (yPos)), Quaternion.identity);
            yPos += higher_platform_yPos;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == platformLayer) 
        {
            if (Random.Range(1, 10) == 1)
            {
                Destroy(collision.gameObject);
                xPos = Random.Range(minRangeX, maxRangeX);
                GameObject platformGo = Instantiate(springPrefab, new Vector2(xPos, playerPosY + yPos), Quaternion.identity);
                yPos += Random.Range(minRangeY, maxRangeY);
            }
            else
            {
                xPos = Random.Range(minRangeX, maxRangeX);
                collision.gameObject.transform.position = new Vector2(xPos, playerPosY + yPos);
                yPos += Random.Range(minRangeY, maxRangeY);
            }
        }
        else if (collision.gameObject.layer == springLayer) 
        {
            if (Random.Range(1, 10) == 1)
            {
                xPos = Random.Range(minRangeX, maxRangeX);
                collision.gameObject.transform.position = new Vector2(xPos, playerPosY + yPos);
                yPos += Random.Range(minRangeY, maxRangeY);
            }
            else
            {
                Destroy(collision.gameObject);
                xPos = Random.Range(minRangeX, maxRangeX);
                GameObject platformGo = Instantiate(platformPrefab, new Vector2(xPos, playerPosY + yPos), Quaternion.identity);
                yPos += Random.Range(minRangeY, maxRangeY);
            }
        }

        if (collision.gameObject.layer == breakLayer) 
        {
            Destroy(collision.gameObject);         
        }
    }

    void SpawnBreak()
    {
        escalatedTime += Time.deltaTime;
        if (escalatedTime >= timeLimit)
        {
            xPos = Random.Range(minRangeX, maxRangeX);
            Instantiate(breakPrefab, new Vector2(xPos, playerPosY + yPos), Quaternion.identity);
            yPos++;
            escalatedTime = 0.0f;
        }
    }
}
