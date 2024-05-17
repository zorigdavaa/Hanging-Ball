using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ZPackage.Utility;

namespace ZPackage
{
    public class LevelSpawner : GenericSingleton<LevelSpawner>
    {
        public List<Level> levels;
        [SerializeField] GameObject Finish;
        public List<lvlSegment> Segments;
        private void Awake()
        {
            Z.GM.GameStart += OnGameStart;
        }

        private void OnGameStart(object sender, System.EventArgs e)
        {
            Init();
        }

        // Vector3 lastCubePos;
        public void Init()
        {
            LoadLevel();
            // LeaderBoardData.SetDatas();
        }
        public void LoadLevel()
        {
            // InstantiateRoad(200);
            // InstantiateBoxes(20);
            // InstantiateFinish();
            for (int i = 0; i < 10; i++)
            {
                SpawnSegment();
            }
        }

        private void InstantiateFinish()
        {
            Instantiate(Finish, lastPos, Quaternion.identity, transform);
        }
        Vector3 lastPos;
        List<lvlSegment> CurrentSegments = new List<lvlSegment>();
        void SpawnSegment()
        {
            lvlSegment newSegment = Segments[Random.Range(0, Segments.Count)];
            lvlSegment inSegment = Instantiate(newSegment, lastPos, Quaternion.identity, transform);
            CurrentSegments.Add(newSegment);
            lastPos = inSegment.End.position + Vector3.up * Random.Range(-2, 2);
        }
    }
}

