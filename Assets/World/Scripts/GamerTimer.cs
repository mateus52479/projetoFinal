using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startTime = 30f; // tempo inicial em segundos
    private float currentTime;

    public TextMeshProUGUI timerText;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // posições onde os inimigos vão nascer

    private bool hasSpawnedFirstWave = false;
    private bool hasSpawnedSecondWave = false;

    void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();
        SpawnEnemies(4); // spawna 4 no início
        hasSpawnedFirstWave = true;
    }

    void Update()
    {
        // diminui o tempo
        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        // depois de 10 segundos, spawna mais 2
        if (currentTime <= startTime - 10f && !hasSpawnedSecondWave)
        {
            SpawnEnemies(2);
            hasSpawnedSecondWave = true;
        }

        // se o tempo acabou
        if (currentTime <= 0)
        {
            currentTime = 0;
            DestroyAllEnemies();
            enabled = false; // desativa o script pra não rodar mais
        }
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(currentTime);
        timerText.text = seconds.ToString("00");
    }

    void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, point.position, Quaternion.identity);
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }
    }
}
