using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Componentes")]
    public AudioSource audioSource;   // Um único AudioSource

    [Header("Sons")]
    public AudioClip footstepSound;   // Som de passo
    public AudioClip attackSound;     // Som de ataque

    [Header("Configurações de Passos")]
    public float stepInterval = 0.4f;

    private float stepTimer;
    private bool isMoving;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        isMoving = (horizontal != 0 || vertical != 0);

        HandleFootsteps();
        HandleAttack();
    }

    void HandleFootsteps()
    {
        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                audioSource.PlayOneShot(footstepSound);
                stepTimer = 0;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
