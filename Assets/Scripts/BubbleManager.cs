using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public Vector3 shootDirection;

    private Vector2 mid;
    private float radius = 1;

    private Rigidbody rb;
    
    public float maxforce = 1;

    public float selfDestructionTime = 4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        mid = transform.position;
        
        float minX = transform.position.x - radius;
        float maxX = transform.position.x + radius;
        
        float minY = transform.position.y - radius;
        float maxY = transform.position.y + radius;

        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);

        shootDirection = new Vector3(xPos, yPos, transform.position.z);
        float force = Random.Range(0, maxforce);

        rb.AddForce(shootDirection * force, ForceMode.Impulse);

        // Destroy the balloon itself
        Destroy(gameObject, selfDestructionTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Table"))
        {
            Destroy(gameObject);
        }
    }

}
