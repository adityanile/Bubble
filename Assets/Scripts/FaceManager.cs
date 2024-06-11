using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    public GameObject wand;
    public float offset = 0.2f;

    private void Start()
    {
        wand = GameObject.Find("Wand");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(wand.transform);
    }
}
