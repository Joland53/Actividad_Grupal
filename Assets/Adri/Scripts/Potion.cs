using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            healthManagerSO.PlayerHealed();
        }
        Destroy(gameObject);
    }
}
