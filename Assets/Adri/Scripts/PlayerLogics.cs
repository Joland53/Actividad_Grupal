using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogics : MonoBehaviour
{

    [SerializeField] HealthManager healthManagerSO;

    [SerializeField, Range(20,300)]                                                 
    [Tooltip("Velocidad de rotación del Player")]         
    private float rotationSpeed;                                      

    [SerializeField, Range(1,20)]
    [Tooltip("Variable de la velocidad de movimiento de la cápsula")]  
    private float movementSpeed;                                        
  
    private CharacterController characterController;                     
   
    void Start()
    {
        healthManagerSO.ResetManager();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");  
        float vInput = Input.GetAxisRaw("Vertical");    
        
        float rotation = hInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
     
        Vector3 forwardMovement = transform.TransformDirection(Vector3.forward) * vInput * movementSpeed * Time.deltaTime;
        characterController.Move(forwardMovement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            healthManagerSO.PlayerDamaged();
        }
    }
}