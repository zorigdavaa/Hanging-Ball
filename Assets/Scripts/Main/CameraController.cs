using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : Singleton<CameraController>
{
    public static bool IsZoom = false;
    public Transform followTf;
    [Header("Smooth")]
    public bool isSmooth = false;
    public float smoothSpeed = 5;
    [Header("Shake")]
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    [Header("Zoom")]
    public float zoomDuration = 1;
    public float zoomMagnitude = 10;
    Vector3 offset, deltaOffset;
    Vector3 startOffset, LongestOffset;
    // float startZ, longestZ;
    float initialPLayerY;
    [SerializeField]
    Camera cam;
    void Start()
    {
        cam = transform.GetComponentInChildren<Camera>();
        IsZoom = false;
        offset = transform.position - followTf.position;
        deltaOffset = Vector3.zero;
        startOffset = offset;
        LongestOffset = startOffset * 1.2f;
        // startZ = offset.z;
        // longestZ = startZ - startZ * 0.5f;
        initialPLayerY = followTf.transform.position.y;

    }
    private void FixedUpdate()
    {
        if (IsPlaying && followTf)
        {
            if (isSmooth)
            {
                // transform.position = Vector3.Lerp(
                //     transform.position,
                //     offset + deltaOffset + followTf.position,
                //     DT * smoothSpeed
                // );
                Vector3 followPos = offset + followTf.position;
                // followPos.y = Mathf.Clamp(followPos.y, 40, 1000);
                Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, followPos, ref deltaOffset, smoothSpeed, 100);
                transform.position = smoothedPos;

            }
            else
            {
                transform.position = offset + deltaOffset + followTf.position;
            }
            SetFovY();
        }
    }
    public void SetFovY()
    {
        float t = Mathf.InverseLerp(1, initialPLayerY, followTf.position.y);
        // float fov = Mathf.Lerp(60, 80, t);
        // cam.fieldOfView = fov;
        offset = Vector3.Lerp(LongestOffset, startOffset, t);
        // offset.z = Mathf.Lerp(longestZ, startZ, t);
    }
    public Camera GetCamera()
    {
        return cam;
    }
    public void Shake()
    {
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        for (float t = 0; t < duration; t += DT)
        {
            A.Cam.transform.localPosition = transform.TransformDirection(new Vector3(Rnd.Val1, Rnd.Val1, 0) * magnitude);
            yield return null;
        }
        A.Cam.transform.localPosition = Vector3.zero;
    }
    public void OffsetZoom(Vector3 pos)
    {
        StartCoroutine(OffsetZoom(pos, Vector3.Distance(transform.position, pos) / zoomMagnitude * zoomDuration));
    }
    IEnumerator OffsetZoom(Vector3 pos, float duration)
    {
        Vector3 deltaOffsetEnd = transform.position - (offset + followTf.position);
        for (float t = 0; t < duration; t += DT)
        {
            deltaOffset = Vector3.Lerp(deltaOffset, deltaOffsetEnd, t / duration);
            yield return null;
        }
        transform.position = pos;
    }
    public void Zoom(Vector3 pos)
    {
        StartCoroutine(Zoom(pos, Vector3.Distance(transform.position, pos) / zoomMagnitude * zoomDuration));
    }
    IEnumerator Zoom(Vector3 pos, float duration)
    {
        if (!IsZoom)
        {
            IsZoom = true;
            Vector3 startPos = transform.position;
            for (float t = 0; t < duration; t += DT)
            {
                transform.position = Vector3.Lerp(startPos, pos, t / duration);
                yield return null;
            }
            transform.position = pos;
            IsZoom = false;
        }
    }
}