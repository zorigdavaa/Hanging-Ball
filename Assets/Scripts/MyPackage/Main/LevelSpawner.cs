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
        public List<lvlSegment> BirckSegments;
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
                    case SegType.Brick: BirckSegments.Add(item); break;
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
        lvlSegment LastSpawnedSegment;
        lvlSegment SpawnSegment()
        {
            lvlSegment newSegment;
            if (LastSpawnedSegment == null)
            {
                newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
            }
            else
            {
                switch (LastSpawnedSegment.Type)
                {
                    case SegType.None:
                        newSegment = BirckSegments[Random.Range(0, BirckSegments.Count)];

                        break;
                    case SegType.Brick:
                        if (Random.value < 0.25f)
                        {
                            newSegment = BirckSegments[Random.Range(0, BirckSegments.Count)];
                        }
                        else if (Random.value < 0.5f)
                        {
                            newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
                        }
                        else if (Random.value < 0.75f)
                        {
                            newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
                        }
                        else
                        {
                            newSegment = BoosterSegments[Random.Range(0, BoosterSegments.Count)];
                        }
                        break;
                    case SegType.Obs:
                        if (Random.value < 0.25f)
                        {
                            newSegment = BirckSegments[Random.Range(0, BirckSegments.Count)];
                        }
                        else if (Random.value < 0.5f)
                        {
                            newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
                        }
                        else if (Random.value < 0.75f)
                        {
                            newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
                        }
                        else
                        {
                            newSegment = BoosterSegments[Random.Range(0, BoosterSegments.Count)];
                        }
                        break;
                    case SegType.Booster:
                        if (Random.value < 0.25f)
                        {
                            newSegment = BirckSegments[Random.Range(0, BirckSegments.Count)];
                        }
                        else if (Random.value < 0.5f)
                        {
                            newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
                        }
                        else if (Random.value < 0.75f)
                        {
                            newSegment = NoneSegments[Random.Range(0, NoneSegments.Count)];
                        }
                        else
                        {
                            newSegment = BoosterSegments[Random.Range(0, BoosterSegments.Count)];
                        }
                        break;
                    default:
                        newSegment = BirckSegments[Random.Range(0, BirckSegments.Count)];
                        break;
                }
            }
            // newSegment = AllSegments[Random.Range(0, AllSegments.Count)];
            LastSpawnedSegment = SpawnSegment(newSegment);
            return LastSpawnedSegment;
        }
        lvlSegment SpawnRandomSegment(List<lvlSegment> inComingSegments)
        {
            return SpawnSegment(inComingSegments[Random.Range(0, inComingSegments.Count)]);
        }
        lvlSegment SpawnSegment(lvlSegment segment)
        {
            lvlSegment inSegment = Instantiate(segment, lastPos, Quaternion.identity, CurrentLevel.transform);
            CurrentLevel.LevelSegments.Add(segment);
            lastPos = inSegment.End.position + Vector3.down * Random.Range(0, 2);
            return inSegment;
        }
        private void InitData()
        {
            segmentDataDict = new Dictionary<SegType, SegmentData>
            {
                { SegType.None, new SegmentData { segments = NoneSegments, probability = 0.25f } },
                { SegType.Brick, new SegmentData { segments = BirckSegments, probability = 0.25f } },
                { SegType.Obs, new SegmentData { segments = ObsSegments, probability = 0.25f } },
                { SegType.Booster, new SegmentData { segments = BoosterSegments, probability = 0.25f } }
            };
        }

        private Dictionary<SegType, SegmentData> segmentDataDict;
        lvlSegment GetNextSegment(SegType lastType)
        {
            List<SegmentData> candidates = new List<SegmentData>();
            float totalProbability = 0f;

            foreach (var kvp in segmentDataDict)
            {
                if (kvp.Key == lastType || kvp.Key == SegType.None)
                {
                    continue;
                }

                candidates.Add(kvp.Value);
                totalProbability += kvp.Value.probability;
            }

            float randomValue = Random.value * totalProbability;
            foreach (var candidate in candidates)
            {
                if (randomValue < candidate.probability)
                {
                    return GetRandomSegment(candidate.segments);
                }
                randomValue -= candidate.probability;
            }

            return GetRandomSegment(segmentDataDict[SegType.Brick].segments); // Fallback
        }
        lvlSegment GetRandomSegment(List<lvlSegment> segments)
        {
            return segments[Random.Range(0, segments.Count)];
        }
    }
}
[System.Serializable]
public class SegmentData
{
    public List<lvlSegment> segments;
    public float probability;
}
