using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private PointController pointCon;

    [SerializeField] private Star starPrefab;
    [SerializeField] private Meteorite meteoritePrefab;
    [SerializeField] private Shield shieldPrefab;

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

    private float timeAnySpawn = 5;
    private float timeAnyWait = 0;

    // Можно ли спаунить на определённой линии
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
            if (timeAnyWait <= 0)
            {
                timeAnyWait = timeAnySpawn;
                int rnd = (int)Random.Range(0,4);
                switch(rnd){
                case 0:
                    StartCoroutine(CreateMeteorite());
                    break;
                case 1:
                    StartCoroutine(CreateStar());
                    break;
                case 2:
                    StartCoroutine(CreateShield());
                    break;
                }
            }

            timeStarWait -= Time.deltaTime;
            timeMeteoriteWait -= Time.deltaTime;
            timeShieldWait -= Time.deltaTime;
            timeAnyWait -= Time.deltaTime;
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

    // Проверка, на какой линии можно делать спаун item
    private int CheckStartPositions()
    {
        createItemNow = true;

        int rnd = Random.Range(0, startPositions.Length);
        if (timeSpawnOnLine[rnd]) //while may cause freeze
        {
            rnd = Random.Range(0, startPositions.Length);
        }

        StartCoroutine(CoTimerSpawnOnLive(rnd));
        
        return rnd;
    }

    // Запрет на спаун на 1 секунду на линии текущего спауна
    private IEnumerator CoTimerSpawnOnLive(int numberLine)
    {
        timeSpawnOnLine[numberLine] = true;

        yield return new WaitForSeconds(1);

        timeSpawnOnLine[numberLine] = false;
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
