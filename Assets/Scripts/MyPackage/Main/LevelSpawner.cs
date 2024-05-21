using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using ZPackage.Utility;

namespace ZPackage
{
    public class LevelSpawner : GenericSingleton<LevelSpawner>
    {
        public List<Level> levels;
        [SerializeField] lvlSegment Finish;
        public List<lvlSegment> AllSegments;
        public List<lvlSegment> NoneSegments;
        public List<lvlSegment> ObsSegments;
        public List<lvlSegment> BrickSegments;
        public List<lvlSegment> BoosterSegments;
        WeightedRandomBag<List<lvlSegment>> Bag;
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
            foreach (var item in AllSegments)
            {
                switch (item.Type)
                {
                    case SegType.None: NoneSegments.Add(item); break;
                    case SegType.Brick: BrickSegments.Add(item); break;
                    case SegType.Obs: ObsSegments.Add(item); break;
                    case SegType.Booster: BoosterSegments.Add(item); break;
                    default: break;
                }
            }
            Bag = new WeightedRandomBag<List<lvlSegment>>();
            Bag.Add(NoneSegments, 40);
            Bag.Add(BrickSegments, 30);
            Bag.Add(ObsSegments, 20);
            Bag.Add(BoosterSegments, 10);
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
            SpawnRandomSegment(NoneSegments);
            for (int i = 0; i < SegmentLength; i++)
            {
                SpawnSegment(LastSpawnedSegment.Type);
            }
            InstantiateFinish();
        }

        private void InstantiateFinish()
        {
            SpawnSegment(Finish);
        }
        Vector3 lastPos;
        lvlSegment LastSpawnedSegment;
        lvlSegment SpawnSegment(SegType lastType)
        {
            return SpawnRandomSegment(Bag.GetRandom());
        }


        lvlSegment SpawnRandomSegment(List<lvlSegment> inComingSegments)
        {
            return SpawnSegment(inComingSegments[Random.Range(0, inComingSegments.Count)]);
        }
        lvlSegment SpawnSegment(lvlSegment segment)
        {
            lvlSegment inSegment = Instantiate(segment, lastPos, Quaternion.identity, CurrentLevel.transform);
            CurrentLevel.LevelSegments.Add(segment);
            LastSpawnedSegment = inSegment;
            lastPos = inSegment.End.position + Vector3.down * Random.Range(0, 2);
            return inSegment;
        }
    }
}