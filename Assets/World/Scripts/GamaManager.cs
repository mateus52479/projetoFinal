using UnityEngine;
using TMPro;

public class GamaManager : MonoBehaviour
{
    public HeartSystem heart;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            heart.vida--;
        }
    }


}
