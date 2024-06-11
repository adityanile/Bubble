using System;
using System.Collections;
using UnityEngine;

public class WandManager : MonoBehaviour
{
    public GameObject[] bubbles;
    public Transform spawnPos;

    public Transform spawnedBalloons;

    private Vector3 screenPoint;
    private Vector3 offset;

    private Rigidbody rb;

    public float waitTime = 0.1f;

    // Speed Management

    [SerializeField]
    private int speed;
    public float normalSpawnRate = 0.5f;
    public float maxspawnRate = 0.2f;
    public int speedLimit = 50;

    private bool highrate = false;

    private float originalRate;

    private bool alreadySpawing = false;

    public float gravityModifier = -9.81f;
    private Vector3 gravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(CalcVelocity());

        // Changing the game gravity
        gravity = new Vector3(0, gravityModifier, 0);
        Physics.gravity = gravity;

        originalRate = normalSpawnRate;
    }

    void Update()
    {
        // Manging the spawn rate of the bubbles with the wand speed

        if (!highrate)
        {
            if (speed > speedLimit)
            {
                highrate = true;
                normalSpawnRate = maxspawnRate;

                StartCoroutine(DecreaseRate());
            }
        }
    }

    IEnumerator DecreaseRate()
    {
        while (highrate)
        {
            yield return new WaitForSeconds(waitTime);

            if (speed < speedLimit)
            {
                highrate = false;
                normalSpawnRate = originalRate;
                StopCoroutine(DecreaseRate());
            }
        }
    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            Vector3 prevPos = transform.position;

            yield return new WaitForEndOfFrame();

            Vector3 currVel = (prevPos - transform.position) / Time.deltaTime;
            speed = Convert.ToInt32(currVel.magnitude);
        }
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        if (!alreadySpawing)
        {
            alreadySpawing = true;
            StartCoroutine(SpawnBubbles());
        }
    }

    private void OnMouseUp()
    {
        // Stop spawing the balloons
        StopCoroutine(SpawnBubbles());
        alreadySpawing = false;
    }

    // Spawing the bubbles when wand is moved
    IEnumerator SpawnBubbles()
    {
        while (alreadySpawing)
        {
            yield return new WaitForSeconds(normalSpawnRate);

            int index = UnityEngine.Random.Range(0, bubbles.Length);
            Instantiate(bubbles[index], spawnPos.position, bubbles[index].transform.rotation, spawnedBalloons);
        }
    }


}
