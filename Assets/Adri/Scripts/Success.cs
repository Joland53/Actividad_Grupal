using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private ParticleSystem successVFX;

    void Start()
    {
        if (successVFX != null)
        {
            successVFX.Play();
        }
    }
    
    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            playerMovement.PauseGame();
            healthManagerSO.Success();
        }
        //Destroy(gameObject);
    }
}
