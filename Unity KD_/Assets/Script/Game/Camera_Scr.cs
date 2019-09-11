using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Scr : MonoBehaviour
{

    [SerializeField]
    Transform target;
    float pitch;
    float yaw;
    float distance;

    const float minDistance = 1f;
    const float maxDistance = 20f;
    const float defaultDistance = 2f;
    const float zoomSpeed = 400f;

    const float maxPitch = 70f;
    const float minPitch = 20f;
    const float defaultPitch = 30f;
    //Vector3 lastMousePos;
    float speed = 10f;
    float screenW;
    float screenH;

    void Start()
    {
        screenW = Screen.width;
        screenH = Screen.height;
        ResetParam();
        SetCamera();
    }

    void Update()
    {
        //カメラズーム
        var z = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance + z * Time.deltaTime, minDistance, maxDistance);
        //上下操作
        if (Input.GetKey(KeyCode.UpArrow))
        {
            var v = speed;
            pitch = Mathf.Clamp(pitch + v * (maxPitch - minPitch) / screenH, minPitch, maxPitch);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            var v = -speed;
            pitch = Mathf.Clamp(pitch + v * (maxPitch - minPitch) / screenH, minPitch, maxPitch);
        }
        //左右操作
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var h = -speed;
            yaw += h * 360 / screenW;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            var h = speed;
            yaw += h * 360 / screenW;
        }
        //カメラリセット
        if (Input.GetMouseButtonDown(1))
        {
            ResetParam();
        }
        SetCamera();
    }

    void ResetParam()
    {
        pitch = defaultPitch;
        distance = defaultDistance;
        yaw = target.rotation.eulerAngles.y;
    }

    void SetCamera()
    {
        var q = Quaternion.Euler(pitch, yaw, 0f);
        var vec = q * Vector3.forward * distance;
        transform.position = target.position - vec;
        transform.LookAt(target);
    }
}
