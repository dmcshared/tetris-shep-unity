using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlled : MonoBehaviour
{
    Rigidbody[] parts;

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        parts = GetComponentsInChildren<Rigidbody>();

        foreach (var part in parts)
        {
            part.useGravity = false;

            part.velocity = new Vector3(0f, -speed, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        foreach (var part in parts)
        {
            Vector3 sidewaysForce = Vector3.right * horizontal * 2f;
            part.AddForce(sidewaysForce, ForceMode.Force);
        }

        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            foreach (var part in parts)
            {
                Vector3 torque = Vector3.forward * vertical * 2f;
                part.AddTorque(torque, ForceMode.Force);
            }
        }
        else if (vertical < 0)
        {
            foreach (var part in parts)
            {
                part.useGravity = true;
            }
        }
        else
        {
            foreach (var part in parts)
            {
                part.useGravity = false;
                part.velocity = new Vector3(part.velocity.x, -speed, part.velocity.z);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Detach();
        }


        Vector3 averageVelocity = Vector3.zero;
        foreach (var part in parts)
        {
            averageVelocity += part.velocity;
        }
        averageVelocity /= parts.Length;

        if (averageVelocity.y > -speed / 2)
        {
            Detach();
        }
    }

    public void Detach()
    {
        foreach (var part in parts)
        {
            part.useGravity = true;
        }

        Destroy(this);
    }
}
