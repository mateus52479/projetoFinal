using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startTime = 30f; // tempo inicial
    private float currentTime;

    public TextMeshProUGUI timerText;

    // inimigo já existente na cena, DESATIVADO
    public GameObject enemyBase;

    // pontos de spawn
    public Transform[] spawnPoints;

    private bool hasSpawnedFirstWave = false;
    private bool hasSpawnedSecondWave = false;

    void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();

        // spawn inicial de 4 inimigos
        SpawnEnemies(4);
        hasSpawnedFirstWave = true;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        // depois de 10 segundos, spawna +2
        if (currentTime <= startTime - 10f && !hasSpawnedSecondWave)
        {
            SpawnEnemies(2);
            hasSpawnedSecondWave = true;
        }

        // quando o tempo acaba
        if (currentTime <= 0)
        {
            currentTime = 0;
            DestroyAllEnemies();
            enabled = false;
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

            // cria um clone do inimigo existente e ativa
            GameObject clone = Instantiate(enemyBase, point.position, Quaternion.identity);
            clone.SetActive(true);
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
