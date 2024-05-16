using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZPackage;

public class BuildObj : MonoBehaviour
{
    [SerializeField] List<Transform> Parts;
    int currentPartIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentPartIndex = PlayerPrefs.GetInt(gameObject.name, 0);
        for (int i = currentPartIndex; i < Parts.Count; i++)
        {
            Parts[i].gameObject.SetActive(false);
        }
        // foreach (var item in Parts)
        // {
        //     item.gameObject.SetActive(false);
        // }
        Z.GM.LevelCompleted += OnLevelComplte;
    }

    private void OnLevelComplte(object sender, LevelCompletedEventArgs e)
    {
        PlayerPrefs.SetInt(gameObject.name, currentPartIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowLast();
        }
    }
    public void ShowLast()
    {
        Parts[currentPartIndex].gameObject.SetActive(true);
        currentPartIndex++;
    }
    public Transform Getlast()
    {
        return Parts[currentPartIndex];
    }
    [ContextMenu("Order By Y")]
    public void OrderPartsByY()
    {
        Parts = Parts.OrderBy(x => x.position.y).ToList();
    }
}
