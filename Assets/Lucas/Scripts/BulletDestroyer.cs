using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public float destroyTime = 2f; // Tiempo antes de que la bala se destruya

    void Start()
    {
        // 📌 Si la bala no choca con nada, se destruye después de "destroyTime" segundos
        Destroy(gameObject, destroyTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        {
            // 📌 Si la bala choca con un objeto que tiene las tags "Wall", "Ground" o "Enemy", se destruye
            if (other.CompareTag("Wall") || other.CompareTag("Ground") || other.CompareTag("Enemy"))
            {
                Destroy(gameObject); // 💥 Se destruye la bala
            }
        }
    }
}

