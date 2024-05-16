using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;
using ZPackage.Helper;
using Random = UnityEngine.Random;
using UnityEngine.Pool;
using ZPackage.Utility;
using System.Linq;
using Cinemachine;

public class Player : Mb
{
    [SerializeField] List<CinemachineVirtualCamera> Cameras;
    [SerializeField] GameObject BoxPrefab;
    BuildObj BuildObj;
    // Start is called before the first frame update
    void Start()
    {
        Z.Player = this;
        currentCamera = Cameras[0];
        BuildObj = FindObjectOfType<BuildObj>();
    }
    float timer = 0;
    float FireTime = 0.1f;
    void Update()
    {
        if (IsBuilding && IsClick)
        // if (IsClick)
        {
            timer += Time.deltaTime;
            if (timer > FireTime)
            {
                Z.GM.BrickCount--;
                timer = 0;
                GameObject box = Instantiate(BoxPrefab, currentCamera.transform.position, Quaternion.identity);
                if (Z.GM.BrickCount == 0)
                {
                    Z.GM.LevelComplete(this, 0);
                }
                StartCoroutine(MoveToCor(box.transform, BuildObj.Getlast(), () =>
                {
                    BuildObj.ShowLast();
                    Destroy(box);
                }));
            }
        }
    }
    IEnumerator MoveToCor(Transform obj, Transform target, Action AfterAction = null)
    {
        float startTime = Time.time;
        Vector3 startPos = obj.position;
        Vector3 targetPos = target.position;
        float duration = 1;

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            obj.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;
        }
        obj.position = targetPos;
        AfterAction?.Invoke();
    }

    public int OldCameraIndex;
    public int currentCameraIndex;
    [SerializeField] CinemachineVirtualCamera currentCamera;
    public void ChangeCamera(int index)
    {
        OldCameraIndex = currentCameraIndex;
        // if (currentCameraIndex != index && Driver && CanChangeCamera)
        if (currentCameraIndex != index)
        {
            currentCamera.Priority = 0;
            currentCamera = Cameras[index];
            currentCamera.Priority = 1;
            currentCameraIndex = index;

        }
    }
}
