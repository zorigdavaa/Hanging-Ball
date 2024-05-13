using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimneyCreator : MB
{
    float TAU = 6.283185307179586f;
    public int floorCount = 3;
    // public int brickPerFloor = 20;
    public float ChimneyRadius = 3;
    public float ChimneyInnerRadius = 1;
    public float SecondOffset = 6;
    public float rotationSpeed = 5;
    public float overlapCount = 2;
    public float overlapDistance = 0.5f;
    public int[] brickPerFloors = { 24, 20 };
    public int[] brickPerFloorsTop = { 20, 14 };
    public Material[] brickMats;
    public Brick brick;
    public List<Brick> bricks;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < floorCount; i++)
        {
            float tRad = (float)i / (float)floorCount;
            float radius = Mathf.Lerp(ChimneyRadius, ChimneyInnerRadius, tRad);
            for (int z = 0; z < overlapCount; z++)
            {
                int brickCount = Mathf.RoundToInt(Mathf.Lerp(brickPerFloors[z], brickPerFloorsTop[z], tRad));
                // Material mat = brickMats[z];
                // for (int j = 0; j < brickPerFloors[z]; j++)
                for (int j = 0; j < brickCount; j++)
                {
                    // float tBrick = (float)j / (float)brickPerFloors[z] * TAU;
                    float tBrick = (float)j / (float)brickCount * TAU;
                    float y = 1.15f + i * 0.25f;
                    Vector3 dir = AngleToDir(tBrick, i % 2 == 0 ? TAU / SecondOffset : 0);
                    Vector3 point = dir * (radius - overlapDistance * z);
                    point.y = y;
                    point = transform.TransformPoint(point);
                    Brick insBrick = Instantiate(brick, point, Quaternion.LookRotation(dir.normalized), transform);
                    bricks.Add(insBrick);
                    // insBrick.creator = this;
                    // insBrick.transform.GetChild(0).GetComponent<Renderer>().material = mat;
                    // if (i == 0)
                    // {
                    //     insBrick.bottomCheck = false;
                    // }
                }
            }
        }
    }
    private void OnValidate()
    {

    }
    public void RemoveBrickFromList(Brick brick)
    {
        bricks.Remove(brick);
        if (bricks.Count == 0)
        {
            GameController.Instance.LevelCompleted();
        }
    }
    public Vector3 GetTopOnesPos()
    {
        // return bricks[bricks.Count - 1].transform.position;
        Vector3 firePos = bricks[bricks.Count - 1].transform.position;
        firePos.z = -ChimneyRadius;
        firePos.x = 0;
        return firePos;
    }
    public float GetTopY()
    {
        float maxY = 0;
        if (bricks.Count > 48)
        {
            maxY = bricks[bricks.Count - 1].transform.position.y;
        }
        return Mathf.Max(maxY, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // if (IsPlaying && IsClick)
        // {
        //     float input = Input.GetAxis("Mouse X");
        //     transform.Rotate(new Vector3(0, input * rotationSpeed, 0));
        // }
    }
    private void OnDrawGizmos()
    {
        // for (int i = 0; i < floorCount; i++)
        // {
        //     for (int z = 0; z < overlapCount; z++)
        //     {
        //         for (int j = 0; j < brickPerFloors[z]; j++)
        //         {
        //             float tBrick = (float)j / (float)brickPerFloors[z] * TAU;
        //             float y = 0.5f + i * 0.5f;
        //             Vector3 dir = AngleToDir(tBrick, i % 2 == 0 ? TAU / SecondOffset : 0);
        //             Vector3 point = dir * (ChimneyRadius - overlapDistance * z);
        //             point.y = y;
        //             Gizmos.DrawSphere(point, 0.5f);

        //         }

        //     }

        // }
        // for (int i = 0; i < floorCount; i++)
        // {
        //     for (int j = 0; j < brickPerFloors[0]; j++)
        //     {
        //         float tBrick = (float)j / (float)brickPerFloors[0] * TAU;
        //         float y = 0.5f + i * 0.5f;
        //         Vector3 dir = AngleToDir(tBrick, i % 2 == 0 ? TAU / SecondOffset : 0) * ChimneyRadius;
        //         dir.y = y;
        //         Gizmos.DrawSphere(dir, 0.5f);
        //     }
        // }
    }

    Vector3 AngleToDir(float t, float offset = 0)
    {
        float value = t + offset;
        return new Vector3(Mathf.Cos(value), 0, Mathf.Sin(value));
    }

    internal void DestroyAbovePos(Vector3 pos)
    {
        List<Brick> aboveBricks = new List<Brick>();
        foreach (var item in bricks)
        {
            if (item.transform.position.y > pos.y)
            {
                aboveBricks.Add(item);
            }
        }
        foreach (var item in aboveBricks)
        {
            item.SetFRee();
        }
        // for (int i = 0; i < bricks.Count; i++)
        // {
        //     if (bricks[i].transform.position.y > pos.y)
        //     {
        //         bricks[i].SetFRee();
        //     }
        // }
    }
}
