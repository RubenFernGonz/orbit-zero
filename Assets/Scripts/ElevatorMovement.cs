using System;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool stop = false;

    void Update()
    {
        if (!stop)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stop = true;

        }
    }
}
