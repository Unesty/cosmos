using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private PointController pointCon;

    [SerializeField] private Star starPrefab;
    [SerializeField] private Meteorite meteoritePrefab;
    [SerializeField] private Shield shieldPrefab;

    [SerializeField] private GameObject[] startPositions;

    private List<Item> allSpawnObjects;

    private bool stopGame = false;

    private float timeStarSpawn = 1.5f;
    private float timeStarWait = 0;

    private float timeMeteoriteSpawn = 2;
    private float timeMeteoriteWait = 2;

    private float timeShieldSpawn = 5;
    private float timeShieldWait = 3;

    private bool[] timeSpawnOnLine = new bool[]{false, false, false};
    
    private bool createItemNow;

    private void Awake()
    {
        allSpawnObjects = new List<Item>();
    }

    private void Update()
    {
        if (!stopGame)
        {
            if (timeMeteoriteWait <= 0)
            {
                timeMeteoriteWait = timeMeteoriteSpawn;

                StartCoroutine(CreateMeteorite());
            }

            if (timeStarWait <= 0)
            {
                timeStarWait = timeStarSpawn;

                StartCoroutine(CreateStar());
            }

            if (timeShieldWait <= 0)
            {
                timeShieldWait = timeShieldSpawn;

                StartCoroutine(CreateShield());
            }

            timeStarWait -= Time.deltaTime;
            timeMeteoriteWait -= Time.deltaTime;
            timeShieldWait -= Time.deltaTime;
        }
    }

    private IEnumerator CreateMeteorite()
    {
        if (createItemNow)
            yield return new WaitForSeconds(.2f);

        int rnd = CheckStartPositions();

        Meteorite newMeteorite = Instantiate(meteoritePrefab, startPositions[rnd].transform.position, Quaternion.identity);
        newMeteorite.spawnCon = GetComponent<SpawnController>();

        allSpawnObjects.Add(newMeteorite);

        createItemNow = false;
    }

    // Создание звезды
    private IEnumerator CreateStar()
    {
        if (createItemNow)
            yield return new WaitForSeconds(.2f);

        int rnd = CheckStartPositions();

        Star newStar = Instantiate(starPrefab, startPositions[rnd].transform.position, Quaternion.identity);
        newStar.spawnCon = GetComponent<SpawnController>();

        newStar.pointCon = pointCon;

        allSpawnObjects.Add(newStar);

        createItemNow = false;
    }

    // Создание Щита
    private IEnumerator CreateShield()
    {
        if (createItemNow)
            yield return new WaitForSeconds(.2f);

        int rnd = CheckStartPositions();

        Shield newShield = Instantiate(shieldPrefab, startPositions[rnd].transform.position, Quaternion.identity);
        newShield.spawnCon = GetComponent<SpawnController>();

        allSpawnObjects.Add(newShield);

        createItemNow = false;
    }

    private int CheckStartPositions()
    {
        createItemNow = true;

        int rnd = Random.Range(0, startPositions.Length);
        while (timeSpawnOnLine[rnd])
        {
            rnd = Random.Range(0, startPositions.Length);
        }

        StartCoroutine(CoTimerSpawnOnLive(rnd));
        
        return rnd;
    }

    private IEnumerator CoTimerSpawnOnLive(int numberLine)
    {
        timeSpawnOnLine[numberLine] = true;

        yield return new WaitForSeconds(1);

        timeSpawnOnLine[numberLine] = false;
    }

    public void StopGame()
    {
        stopGame = true;

        foreach (var star in allSpawnObjects)
        {
            star.CanMove();
        }
    }

    public void DeleteItem(Item item)
    {
        allSpawnObjects.Remove(item);
    }
}
