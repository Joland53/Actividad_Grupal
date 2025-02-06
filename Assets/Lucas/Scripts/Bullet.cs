using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Velocidad de la bala

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("⚠️ No se encontró Rigidbody en la bala.");
            return;
        }

        // 📌 Asegurar que la bala se mueve hacia adelante
        rb.velocity = transform.forward * speed;

        Debug.Log("✅ Bala creada y moviéndose.");
    }
}



