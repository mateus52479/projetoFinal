using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Configurações de Som")]
    public Toggle muteToggle;

    void Start()
    {
        // Atualiza toggle baseado no volume atual
        if (muteToggle != null)
            muteToggle.isOn = AudioListener.volume == 0;

        // Listener do toggle
        if (muteToggle != null)
            muteToggle.onValueChanged.AddListener(ToggleSound);
    }

    // ========== COMEÇAR O JOGO ==========
    public void PlayGame()
    {
        
        SceneManager.LoadScene("Tutorial");
    }

    // ========== SAIR DO JOGO ==========
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }

    // ========== MUTAR / DESMUTAR ==========
    public void ToggleSound(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0f : 1f;
    }
}
