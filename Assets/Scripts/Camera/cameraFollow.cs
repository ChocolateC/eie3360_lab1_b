using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
 public Transform target; // The position that that camera will follow.
 public float smoothing = 5f; // The speed with which the camera will follow.
 public Vector3 offset = new Vector3(); // The initial offset from the target.
 void Start()
 {
    
 }
 void FixedUpdate()
 {
 // Create a position the camera is aiming for based on the offset from the target.
 Vector3 targetCamPos = target.position + offset;
 // Smoothly interpolate between the camera's current and target positions.
 transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
 }
}