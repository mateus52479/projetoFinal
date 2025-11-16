using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Objetos do Menu")]
    public GameObject pauseUI;        // Painel do pause
    public Toggle muteToggle;         // Toggle para mutar/desmutar som

    private bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);

        // Atualiza toggle baseado no volume
        if (muteToggle != null)
            muteToggle.isOn = AudioListener.volume == 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // ========== PAUSAR O JOGO ==========
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;  // pausa o jogo
        isPaused = true;
    }

    // ========== DESPAUSAR ==========
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;  // volta ao normal
        isPaused = false;
    }

    // ========== MUTAR / DESMUTAR ==========
    public void ToggleSound(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0 : 1;
    }

    // ========== VOLTAR AO MENU INICIAL ==========
    public void GoToMainMenu()
    {
        Time.timeScale = 1; // garante que o jogo volte ao normal
        SceneManager.LoadScene("Menu"); // troque pelo nome da sua cena
    }

}