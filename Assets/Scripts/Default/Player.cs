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
    CinemachineTransposer Transposer;
    // Start is called before the first frame update
    void Start()
    {
        Z.Player = this;
        currentCamera = Cameras[0];
        Transposer = currentCamera.GetCinemachineComponent<CinemachineTransposer>();
        BuildObj = FindObjectOfType<BuildObj>();
    }
    float timer = 0;
    float FireTime = 0.1f;
    void Update()
    {
        if (IsPlaying || IsWaiting)
        {
            float VeloCityZ = Z.Ball.rb.velocity.z;
            float t = Mathf.InverseLerp(-20, 20, VeloCityZ);
            float TargetCamYAngle = Mathf.Lerp(50f, 30f, t);
            currentCamera.transform.rotation = Quaternion.Lerp(currentCamera.transform.rotation, Quaternion.Euler(30, -TargetCamYAngle, 0), 0.3f);
        }
        if (IsBuilding && IsClick)
        // if (IsClick)
        {
            timer += Time.deltaTime;
            if (timer > FireTime)
            {

                timer = 0;
                Vector3 pos = currentCamera.transform.position + currentCamera.transform.forward * 3 + -currentCamera.transform.up * 3;
                for (int i = 0; i < 5; i++)
                {
                    if (Z.GM.BrickCount == 0)
                    {
                        Z.GM.LevelComplete(this, 0);
                        break;
                    }
                    Transform goindblock = BuildObj.Getlast();
                    int index = BuildObj.GetIndex();
                    BuildObj.IncreaseIndex();
                    if (goindblock)
                    {
                        GameObject box = Instantiate(BoxPrefab, pos, Quaternion.identity);
                        StartCoroutine(MoveToCor(box.transform, goindblock, () =>
                        {
                            BuildObj.ShowLast(index);
                            Destroy(box);
                        }));
                    }
                    else
                    {
                        Z.GM.LevelComplete(this, 0);
                    }

                    Z.GM.BrickCount--;
                }

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
