using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int wave = 0;

    [SerializeField] private int waveEnemyCount = 10;
    [SerializeField] private int spawnedEnemyCount = 0;
    [SerializeField] private int deadEnemyCount = 0;

    private bool gameOver = false;

    //Encapsulation
    public int Wave
    { get { return wave; } private set { wave = value; } }

    public bool GameOver { get => gameOver; set => gameOver = value; }
    public int DeadEnemyCount { get => deadEnemyCount; set => deadEnemyCount = value; }
    public int SpawnedEnemyCount { get => spawnedEnemyCount; set => spawnedEnemyCount = value; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 40; i++)
        {
            GameObject temp = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
            temp.name = "Enemy " + i;
            if (temp.activeInHierarchy)
                temp.SetActive(false);
            enemies.Add(temp);
        }
        SpawnWave();
    }

    //Abstraction
    private void SpawnWave()
    {
        StartCoroutine(DelayedSpawn());
    }

    private IEnumerator DelayedSpawn()
    {
        if (!gameOver)
        {
            for (int i = 0; i < waveEnemyCount; i++)
            {
                yield return new WaitForSeconds(2);
                enemies[spawnedEnemyCount].SetActive(true);
                if (spawnedEnemyCount == waveEnemyCount-1)
                {
                    enemies[spawnedEnemyCount].GetComponent<Enemy>().OnDisableEvent.AddListener(SpawnNextWave);
                }
                spawnedEnemyCount += 1;
                MainUIHandler.Instance.SetScore(deadEnemyCount.ToString());
            }
        }
    }

    private void SpawnNextWave()
    {
        StartCoroutine(NextWave());
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(3);
        if (!gameOver)
        {
            wave += 1;
            Debug.Log("wave : " + wave);
            spawnedEnemyCount = 0;
            deadEnemyCount = 0;
            waveEnemyCount += (wave * 10);
            SpawnWave();
        }
    }
}