using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 1f;
    [SerializeField] float minXValue = -1f;
    [SerializeField] float maxXValue = 1f;
    [SerializeField] float minZValue = -2.5f;
    [SerializeField] float maxZValue = 2.5f;

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            float NewPositionX = Mathf.Clamp(transform.position.x - Time.deltaTime * cameraSpeed, minXValue, maxXValue);
            transform.position = new Vector3(NewPositionX, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            float NewPositionX = Mathf.Clamp(transform.position.x + Time.deltaTime * cameraSpeed, minXValue, maxXValue);
            transform.position = new Vector3(NewPositionX, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            float NewPositionZ = Mathf.Clamp(transform.position.z - Time.deltaTime * cameraSpeed, minZValue, maxZValue);
            transform.position = new Vector3(transform.position.x, transform.position.y, NewPositionZ);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            float NewPositionZ = Mathf.Clamp(transform.position.z + Time.deltaTime * cameraSpeed, minZValue, maxZValue);
            transform.position = new Vector3(transform.position.x, transform.position.y, NewPositionZ);
        }
    }
}
