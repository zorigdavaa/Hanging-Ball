using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelSpawner : Singleton<LevelSpawner>
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject Boxes;
    [SerializeField] List<Material> materials;
    public List<Level> levels;
    List<Vector3> cubePos = new List<Vector3>();
    // Vector3 lastCubePos;
    public void Init()
    {
        LoadLevel();
        // LeaderBoardData.SetDatas();
    }
    public void LoadLevel()
    {
        InstantiateRoad(200);
        InstantiateBoxes(20);
        InstantiateBoss();
    }

    private void InstantiateBoss()
    {
        throw new System.NotImplementedException();
    }

    private void InstantiateRoad(int v)
    {
        for (int i = 0; i < v; i++)
        {
            Vector3 lastPos = cubePos.LastOrDefault();
            GameObject dood = Instantiate(cube, lastPos, Quaternion.identity, transform);
            GameObject deed = Instantiate(cube, lastPos + new Vector3(0, 60, 0), Quaternion.identity, transform);
            dood.transform.GetChild(0).GetComponent<Renderer>().material = materials[i % materials.Count];
            // deed.transform.GetChild(0).GetComponent<Renderer>().material = materials[i % materials.Count];
            cubePos.Add(lastPos + new Vector3(0, Random.Range(-2, 2), 3));
            dood.gameObject.layer = 0;
        }
    }
    public Vector3 GetPointByZ(int z)
    {
        int index = Mathf.FloorToInt(z / 3);
        // Vector3 nearest = cubePos.Where(x => x.z - z < 0.5f).First();
        if (cubePos.Count - 1 > index)
        {
            return cubePos[index];
        }
        return cubePos[cubePos.Count - 1];
    }

    int lastBoxPos = 10;
    private void InstantiateBoxes(int v)
    {
        for (int i = 0; i < v; i++)
        {
            Vector3 pos = GetPointByZ(lastBoxPos);
            Instantiate(Boxes, pos + new Vector3(0, 5, 0), Quaternion.identity, transform);
            lastBoxPos += 30;
        }
    }
    int GetLevelIdx()
    {
        int res = GameController.Level - 1;
        if (GameController.Level > levels.Count)
        {
            res = Data.LevelIdx.I();
            if (res < 0 || GameController.IsWin)
            {
                res = Rnd.Idx(levels.Count, res);
                Data.LevelIdx.Set(res);
            }
        }
        return res;
    }
}