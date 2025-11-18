using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startTime = 30f;
    private float currentTime;

    public TextMeshProUGUI timerText;

    // inimigo base desativado
    public GameObject enemyBase;

    // pontos de spawn dos inimigos
    public Transform[] spawnPoints;


    public GameObject tp;

    public Transform tpSpawnPoint;

    private bool hasSpawnedSecondWave = false;
    private bool hasEnded = false;

    void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();

        // spawn inicial de 2 inimigos
        SpawnEnemies(2);
    }

    void Update()
    {
        if (hasEnded) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        // depois de 10 segundos spawna +2
        if (currentTime <= startTime - 10f && !hasSpawnedSecondWave)
        {
            SpawnEnemies(2);
            hasSpawnedSecondWave = true;
        }

        // cronômetro zerou
        if (currentTime <= 0)
        {
            currentTime = 0;
            EndTimer();
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

    void EndTimer()
    {
        hasEnded = true;

        DestroyAllEnemies();

        // muda a mensagem
        timerText.text = "porta aberta";

        // cria o objeto tp no ponto exato definido
        Instantiate(tp, tpSpawnPoint.position, Quaternion.identity).SetActive(true);

        // desativa o script
        enabled = false;
    }
}
