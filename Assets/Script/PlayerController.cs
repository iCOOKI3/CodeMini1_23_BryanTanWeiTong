using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int plane = 0;

    float speed = 4.0f;
    float Alimit = 10.0f;
    float Blimit = 5.0f;

    float xlimit;
    float zlimit;

    int JumpCount = 0;
    int spacetrack = 0;
    float gravityModifier = 2.0f;

    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gravityModifier; 
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (plane == 0)
        {
            xlimit = Alimit;
            zlimit = Alimit;

            if (transform.position.z > zlimit)
            {
                if (transform.position.x > -xlimit / 2 && transform.position.x < xlimit / 2)
                {
                    transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, zlimit);
                }
            }
            else if (transform.position.z < -zlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zlimit);
            }
            else
            {
                transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
            }
            if (transform.position.x > xlimit)
            {
                transform.position = new Vector3(xlimit, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -xlimit)
            {
                transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            }
        }
        else
        {
            zlimit = Alimit + 2 * Blimit;
            xlimit = Blimit;

            if (transform.position.z < Alimit)
            {
                if (transform.position.x > -Alimit / 2 && transform.position.x < Alimit / 2)
                {
                    transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, Alimit);
                }
            }
            else if (transform.position.z > zlimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zlimit);
            }
            else
            {
                transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
            }

            if (transform.position.x > xlimit)
            {
                transform.position = new Vector3(xlimit, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -xlimit)
            {
                transform.position = new Vector3(-xlimit, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && spacetrack < 2 )
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            spacetrack++;
            JumpCount += 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Debug.Log("In Plane A");
            plane = 0;
        }
        if (collision.gameObject.CompareTag("PlaneB"))
        {
            Debug.Log("In Plane B");
            plane = 1;
        }
        if(collision.gameObject.name == "PlaneA")
        {
            spacetrack = 0;
        }
        else if (collision.gameObject.name == "PlaneB")
        {
            spacetrack = 0;
        }
    }
}