using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerSpeed = 5f;
    float rotateSpeed = 100f;
    private CharacterController pepe;








    // Start is called before the first frame update
    void Start()
    {
        pepe = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = transform.forward * verticalInput * playerSpeed;

        pepe.SimpleMove(moveDirection);

        float finalRotation = rotationInput * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, finalRotation, 0);
    }

}
