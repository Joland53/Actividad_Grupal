using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private Camera playerCamera;
    private float xRotation = 0f;
    private bool isGrounded;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto de aparición de la bala
    public float bulletForce = 20f; // Fuerza con la que se dispara la bala

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento con WASD y Flechas
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);

        // Movimiento de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0 || mouseY != 0)
        {
            mouseX *= mouseSensitivity;
            mouseY *= mouseSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // Salto con "Espacio"
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // 📌 DISPARO CON CLIC IZQUIERDO
        if (Input.GetButtonDown("Fire1")) // "Fire1" es el botón del clic izquierdo
        {
            Shoot();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 📌 Método para Disparar
    void Shoot()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null)
        {
            Debug.LogError("Falta asignar el prefab de la bala o el punto de spawn.");
            return;
        }

        // Crear la bala en el punto de spawn con la rotación de la cámara
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, playerCamera.transform.rotation);

        // Agregar fuerza a la bala para que avance hacia adelante
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(playerCamera.transform.forward * bulletForce, ForceMode.Impulse);
        }
    }
}
