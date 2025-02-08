using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    [SerializeField] private HealthManager healthManagerSO;

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
            healthManagerSO.Success();
        }
        //Destroy(gameObject);
    }
}
