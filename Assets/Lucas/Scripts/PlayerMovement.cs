﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] HealthManager healthManagerSO;
    [SerializeField] SoundManager soundManagerSO;

    public float movementSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Camera playerCamera;
    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canShoot = true;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto de aparición de la bala
    public float bulletForce = 20f; // Fuerza con la que se dispara la bala

    void Start()
    {
        healthManagerSO.ResetManager();

        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        healthManagerSO.OnPlayerPaused += PauseGame;
        healthManagerSO.OnPlayerResumed += ResumeGame;
    }
    private void OnDisable() 
    {
        healthManagerSO.OnPlayerPaused -= PauseGame;
        healthManagerSO.OnPlayerResumed -= ResumeGame;
    }
    void Update()
    {
        // Detección de suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantiene al jugador pegado al suelo
        }

        // Movimiento con WASD
        float moveX = Input.GetAxisRaw("Horizontal"); // 🔥 Cambiar a GetAxisRaw para evitar suavizado
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

        if (moveDirection.magnitude > 0)
        {
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);
        }

        // 📌 Evita la inercia: Si no hay input, detiene el movimiento
        else
        {
            controller.Move(Vector3.zero);
        }

        // Movimiento de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Salto con "Espacio"
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calcula la fuerza necesaria para saltar
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 📌 DISPARO CON CLIC IZQUIERDO
        if (canShoot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                soundManagerSO.WeaponShooted();
                Shoot();
            }
        }

    }

    public void PauseGame()
    {
        canShoot = false;
        mouseSensitivity = 0f;
    }

    public void ResumeGame()
    {
        canShoot = true;
        mouseSensitivity = 2f;
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            healthManagerSO.PlayerDamaged();
        }
    }

    // 📌 Método para Disparar
    public void Shoot()
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
