using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController2 : MonoBehaviour
{
    
    [SerializeField] private PointController pointCon;

    [SerializeField] private Star starPrefab;
    [SerializeField] private Meteorite meteoritePrefab;
    [SerializeField] private Shield shieldPrefab;
    [SerializeField] private Balloon balloonPrefab;

    // Позиции в которых спамятся items
    [SerializeField] private GameObject[] startPositions;

    // Все items, которые заспамились и ещё не уничтожены
    private List<Item> allSpawnObjects;

    // Пауза в игре
    private bool stopGame = false;

    // Время между спаунами
    private float timeStarSpawn = 1.5f;
    // Время до следующего спауна
    private float timeStarWait = 0;

    private float timeMeteoriteSpawn = 2;
    private float timeMeteoriteWait = 2;

    private float timeShieldSpawn = 5;
    private float timeShieldWait = 3;

    private float timeBalloonSpawn = 5;
    private float timeBalloonWait = 3;

    private float timeAnySpawn = 15;
    private float timeAnyWait = 10;
    
    private void Awake()
    {
        allSpawnObjects = new List<Item>();
    }


    void Update()
    {
        if (!stopGame)
        {
            
        }
        
    }

    void SpawItem(Item itim)
    {
        Item newItim = Instantiate(itim, startPositions[1].transform.position, Quaternion.identity);
        //newItim.spawnCon = GetComponent<SpawnController>();

        allSpawnObjects.Add(itim);
    }

    // Пауза игры
    public void StopGame()
    {
        stopGame = true;

        foreach (var star in allSpawnObjects)
        {
            star.CanMove();
        }
    }
    // Уничтожение item
    public void DeleteItem(Item item)
    {
        allSpawnObjects.Remove(item);

        Destroy(item.gameObject);
    }
}
