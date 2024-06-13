using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    private GameObject wand;
    public float offset = 0.2f;

    public Transform lookingAt;

    private void Start()
    {
        wand = GameObject.Find("Wand");
        lookingAt = wand.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookingAt);
    }
}
