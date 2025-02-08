using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthManagerSO.PlayerHealed();
        }
    }
}
