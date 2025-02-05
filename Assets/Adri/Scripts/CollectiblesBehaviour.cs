using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesBehaviour : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManagerSO;
    [SerializeField] private float rotationSpeed = 5;
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManagerSO.CollectedCoin();
            Destroy(gameObject);
        }
    }
}
