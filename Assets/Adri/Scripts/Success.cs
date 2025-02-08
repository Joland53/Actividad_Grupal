using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            healthManagerSO.Success();
        }
        //Destroy(gameObject);
    }
}
