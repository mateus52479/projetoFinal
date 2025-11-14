using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTeleport2D : MonoBehaviour
{
    [SerializeField] private string nomeDaCena;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}
