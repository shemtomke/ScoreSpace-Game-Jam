using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    public float metalSpeed;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move to the right side
        transform.Translate((transform.right * metalSpeed * Time.deltaTime));
    }
}
