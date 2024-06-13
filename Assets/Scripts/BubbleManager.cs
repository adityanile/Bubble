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

    private ParticleSystem ps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ps = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        mid = transform.position;
        
        float minX = transform.position.x - radius;
        float maxX = transform.position.x + radius;
        
        float minY = transform.position.y - radius;
        float maxY = transform.position.y + radius;

        float mixZ = transform.position.z;
        float maxZ = transform.position.z - radius;

        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);
        float zPos = Random.Range(mixZ, maxZ);

        shootDirection = new Vector3(xPos, yPos, zPos);
        float force = Random.Range(0, maxforce);

        rb.AddForce(shootDirection * force, ForceMode.Impulse);

        // Destroy the balloon itself
        Destroy(gameObject, selfDestructionTime);
        Invoke("StartDeathEffect", selfDestructionTime - 0.3f);
    }
    
    void StartDeathEffect()
    {
        ps.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Table"))
        {
            ps.Play();
            Destroy(gameObject, 0.3f);
        }
    }

}
