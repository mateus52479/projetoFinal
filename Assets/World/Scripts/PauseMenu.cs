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

        // Atualiza o toggle baseado no volume atual
        if (muteToggle != null)
            muteToggle.isOn = AudioListener.volume == 0;

        // Garante que o toggle chame a função quando clicado
        muteToggle.onValueChanged.AddListener(ToggleSound);
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
        Time.timeScale = 0;
        isPaused = true;

        // Atualiza o estado do toggle ao abrir o pause
        if (muteToggle != null)
            muteToggle.isOn = AudioListener.volume == 0;
    }

    // ========== DESPAUSAR ==========
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    // ========== MUTAR / DESMUTAR ==========
    public void ToggleSound(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    // ========== VOLTAR AO MENU INICIAL ==========
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
