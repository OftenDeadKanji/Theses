using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float verticalAngle = 0.0f, horizontalAngle = 0.0f;
    [SerializeField]
    public float speed = 4.0f;
    private void Start()
    {
        //transform.position = new Vector3(0.0f, 11.93f, 10.895f);
        //transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
    }
    void Update()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? 8.0f : 4.0f;
         
        //translation
        if (Input.GetKey(KeyCode.A))
        {
            //to local left
            transform.position = transform.position + -transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //to local right
            transform.position = transform.position + transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //to local down
            transform.position = transform.position + -transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //to local up
            transform.position = transform.position + transform.forward * Time.deltaTime * speed;
        }
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //to local down
            transform.position = transform.position + -transform.up * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //to local up
            transform.position = transform.position + transform.up * Time.deltaTime * speed;
        }
        



        //rotation
        horizontalAngle += Input.GetAxis("Mouse X");
        verticalAngle += -Input.GetAxis("Mouse Y");

        //transform.localEulerAngles = new Vector3(verticalAngle, horizontalAngle, 0.0f);

    }
}
