using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
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

    private float timeShieldSpawn = 15;
    private float timeShieldWait = 9;

    private float timeBalloonSpawn = 6;
    private float timeBalloonWait = 1;

    private float timeAnySpawn = 6;
    private float timeAnyWait = 0;

    // Можно ли спаунить на определённой линии
    private bool[] timeSpawnOnLine = new bool[]{false, false, false};

    private bool createItemNow;

    private float lifetime = 0;
    public float spawnRateDiv = 1000;

    public float itemsPerMinute = 100;
    public Vector2 starChance;
    public Vector2 meteoriteChance;
    public Vector2 shieldChance;
    public Vector2 balloonChance;
    public Vector2 anyChance;

    private void Awake()
    {
        allSpawnObjects = new List<Item>();
        lifetime = 0;
    }

    private void Update()
    {
        if (!stopGame)
        {
            /*
            if (timeMeteoriteWait <= Random.Range(0f,lifetime/spawnRateDiv))
            {
                timeMeteoriteWait = timeMeteoriteSpawn;

                StartCoroutine(CreateMeteorite());
            }

            if (timeStarWait <= Random.Range(0f,lifetime/spawnRateDiv))
            {
                timeStarWait = timeStarSpawn;

                StartCoroutine(CreateStar());
            }

            if (timeShieldWait <= Random.Range(0f,lifetime/spawnRateDiv))
            {
                timeShieldWait = timeShieldSpawn;

                StartCoroutine(CreateShield());
            }

            if (timeBalloonWait <= Random.Range(0f,lifetime/spawnRateDiv))
            {
                timeBalloonWait = timeBalloonSpawn;

                StartCoroutine(CreateBalloon());
            }

            if (timeAnyWait <= Random.Range(0f,1f))
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
            lifetime += Time.deltaTime;
            */
            bool rndItem = Random.Range(0f,3600f/(float)itemsPerMinute)<1f;
            if(rndItem){
                float rnd=Random.Range(0f,1f);
                if(rnd>starChance[0]&&rnd<starChance[1]){
                    StartCoroutine(CreateStar());
                }
                if(rnd>meteoriteChance[0]&&rnd<meteoriteChance[1]){
                    StartCoroutine(CreateMeteorite());
                }
                if(rnd>shieldChance[0]&&rnd<shieldChance[1]){
                    StartCoroutine(CreateShield());
                }
                if(rnd>balloonChance[0]&&rnd<balloonChance[1]){
                    StartCoroutine(CreateBalloon());
                }
                if(rnd>anyChance[0]&&rnd<anyChance[1]){
                    switch(Random.Range(0,4)){
                    case 0:
                        StartCoroutine(CreateMeteorite());
                        break;
                    case 1:
                        StartCoroutine(CreateStar());
                        break;
                    case 2:
                        StartCoroutine(CreateShield());
                        break;
                    case 3:
                        StartCoroutine(CreateBalloon());
                        break;
                    }
                }
            }
        }
    }

    //Создание астероида
    private IEnumerator CreateMeteorite()
    {
        if (createItemNow)
            yield return new WaitForSeconds(.2f);

        int rnd = CheckStartPositions();
        if(rnd==-1){
            yield break;
        }

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
        if(rnd==-1){
            yield break;
        }

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
        if(rnd==-1){
            yield break;
        }

        Shield newShield = Instantiate(shieldPrefab, startPositions[rnd].transform.position, Quaternion.identity);
        newShield.spawnCon = GetComponent<SpawnController>();

        allSpawnObjects.Add(newShield);

        createItemNow = false;
    }

    // Создание Балона
    private IEnumerator CreateBalloon()
    {
        if (createItemNow)
            yield return new WaitForSeconds(.2f);

        int rnd = CheckStartPositions();
        if(rnd==-1){
            yield break;
        }

        Balloon newBalloon = Instantiate(balloonPrefab, startPositions[rnd].transform.position, Quaternion.identity);
        newBalloon.spawnCon = GetComponent<SpawnController>();

        allSpawnObjects.Add(newBalloon);

        createItemNow = false;
    }

    // Проверка, на какой линии можно делать спаун item
    private int CheckStartPositions()
    {
        createItemNow = true;

        //int rnd = Random.Range(0, startPositions.Length);
        /*
        if (timeSpawnOnLine[rnd]) //while may cause freeze
        {
            rnd = Random.Range(0, startPositions.Length);
            
        }
        */
        /*
        int fpc=0;
        for(int n=0;n<timeSpawnOnLine.Length;n++){
            fpc+=timeSpawnOnLine[n]==false;
        }
        */
        if(timeSpawnOnLine[0]){ //this is definetly not a good code
            if(timeSpawnOnLine[1]){
                if(timeSpawnOnLine[2]){ 
                    return -1;
                }else{
                    StartCoroutine(CoTimerSpawnOnLive(2));
                    return 2;
                }
            }else{
                if(timeSpawnOnLine[2]){
                    StartCoroutine(CoTimerSpawnOnLive(1));
                    return 1;
                }else{
                    if(1==Random.Range(0, 1)){
                        StartCoroutine(CoTimerSpawnOnLive(1));
                        return 1;
                    }else{
                        StartCoroutine(CoTimerSpawnOnLive(2));
                        return 2;
                    }
                }
            }
        }else{
            if(timeSpawnOnLine[1]){
                if(timeSpawnOnLine[2]){
                        StartCoroutine(CoTimerSpawnOnLive(0));
                        return 0;
                }else{
                    if(1==Random.Range(0, 1)){
                        StartCoroutine(CoTimerSpawnOnLive(0));
                        return 0;
                    }else{
                        StartCoroutine(CoTimerSpawnOnLive(2));
                        return 2;
                    }
                }
            }else{
                if(timeSpawnOnLine[2]){
                    if(1==Random.Range(0, 1)){
                        StartCoroutine(CoTimerSpawnOnLive(0));
                        return 0;
                    }else{
                        StartCoroutine(CoTimerSpawnOnLive(1));
                        return 1;
                    }
                }else{
                        int rnd=Random.Range(0, 2);
                        StartCoroutine(CoTimerSpawnOnLive(rnd));
                        return rnd;
                }
            }
        }
    }

    // Запрет на спаун на 1 секунду на линии текущего спауна
    private IEnumerator CoTimerSpawnOnLive(int numberLine)
    {
        timeSpawnOnLine[numberLine] = true;

        yield return new WaitForSeconds(60f/(float)itemsPerMinute);

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
