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
        [SerializeField] lvlSegment Finish;
        public List<lvlSegment> AllSegments;
        public List<lvlSegment> ObsSegment;
        public List<lvlSegment> NoneSegment;
        public Level CurrentLevel;
        public int SegmentLength = 60;
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
            Random.InitState(Z.GM.Level);
            if (CurrentLevel == null)
            {
                CurrentLevel = new GameObject("level " + Z.GM.Level).AddComponent<Level>();
                CurrentLevel.transform.parent = transform;
            }
            for (int i = 0; i < SegmentLength; i++)
            {
                SpawnSegment();
            }
            InstantiateFinish();
        }

        private void InstantiateFinish()
        {
            SpawnSegment(Finish);
        }
        Vector3 lastPos;

        lvlSegment SpawnSegment()
        {
            lvlSegment newSegment = AllSegments[Random.Range(0, AllSegments.Count)];
            return SpawnSegment(newSegment);
        }
        lvlSegment SpawnSegment(lvlSegment segment)
        {
            lvlSegment inSegment = Instantiate(segment, lastPos, Quaternion.identity, CurrentLevel.transform);
            CurrentLevel.LevelSegments.Add(segment);
            lastPos = inSegment.End.position + Vector3.down * Random.Range(0, 2);
            return inSegment;
        }
    }
}

