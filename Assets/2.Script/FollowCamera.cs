using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform cameraTransform;  // 카메라의 Transform

    void Update()
    {
        // 배경을 카메라의 위치로 이동시키기
        transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);
    }
}
