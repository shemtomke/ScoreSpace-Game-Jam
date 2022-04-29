using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField, Range(-1, 1)] float thing;

    void FixedUpdate()
    {
        OffsetScrolling.scrollSpeed += (thing - OffsetScrolling.scrollSpeed) / 2;
    }
}
