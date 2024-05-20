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
            InitData();
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
            List<SegmentData> candidates = new List<SegmentData>();
            float totalProbability = 0f;

            foreach (var kvp in segmentDataDict)
            {
                // if (kvp.Key == lastType || kvp.Key == SegType.None)
                // {
                //     continue;
                // }

                candidates.Add(kvp.Value);
                totalProbability += kvp.Value.probability;
            }

            float randomValue = Random.value * totalProbability;
            foreach (var candidate in candidates)
            {
                if (randomValue < candidate.probability)
                {
                    return SpawnRandomSegment(candidate.segments);
                }
                randomValue -= candidate.probability;
            }

            return SpawnRandomSegment(segmentDataDict[SegType.Brick].segments); // Fallback
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
        private Dictionary<SegType, SegmentData> segmentDataDict;
        private void InitData()
        {
            segmentDataDict = new Dictionary<SegType, SegmentData>
            {
                { SegType.None, new SegmentData { segments = NoneSegments, probability = 0.35f } },
                { SegType.Brick, new SegmentData { segments = BrickSegments, probability = 0.35f } },
                { SegType.Obs, new SegmentData { segments = ObsSegments, probability = 0.2f } },
                { SegType.Booster, new SegmentData { segments = BoosterSegments, probability = 0.1f } }
            };
        }


    }
    [System.Serializable]
    public class SegmentData
    {
        public List<lvlSegment> segments;
        public float probability;
    }
}