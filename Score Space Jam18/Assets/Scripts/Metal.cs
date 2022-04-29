using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    public float metalSpeed;

    Rigidbody2D rb;

    [SerializeField] GameObject HeldObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (HeldObject != null)
        {
            if (HeldObject = gameObject)
            {
                rb.AddForce(new Vector2(Player.MousePos.x - transform.position.x, Player.MousePos.y - transform.position.y));
                transform.Rotate(0, 0, 4);
            }
        }
        else
        {
            //rotates back to 0 rotation when not held
            transform.Rotate(0, 0, (Mathf.DeltaAngle(transform.eulerAngles.z, 0) / 10));
        }


    }
    private void OnMouseDown()
    {
        HeldObject = gameObject;
        rb.drag = 10;
    }

    private void OnMouseUp()
    {
        HeldObject = null;
        rb.drag = 1;
    }
}
