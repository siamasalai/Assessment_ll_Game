using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float move = Input.GetAxis("Vertical"); // W = 1, S = -1

        transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
    }
}
