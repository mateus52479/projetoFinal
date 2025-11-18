using UnityEngine;

public class TimerAtivaArea : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoTotal = 5f; // Tempo do cronômetro em segundos

    [Header("Objetos")]
    public GameObject areaParaAtivar; // A área que será ativada

    private float tempoAtual;
    private bool ativado = false;

    void Start()
    {
        tempoAtual = tempoTotal;

        // Garante que a área comece desativada
        if (areaParaAtivar != null)
            areaParaAtivar.SetActive(false);
    }

    void Update()
    {
        if (ativado) return; // Evita ativar mais de uma vez

        tempoAtual -= Time.deltaTime;

        if (tempoAtual <= 0)
        {
            AtivarArea();
        }
    }

    void AtivarArea()
    {
        ativado = true;

        if (areaParaAtivar != null)
        {
            areaParaAtivar.SetActive(true);
        }

        Debug.Log("Área ativada após o tempo!");
    }
}
