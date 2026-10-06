using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            FindAnyObjectByType<GameManager>().Collect(gameObject);
    }
}