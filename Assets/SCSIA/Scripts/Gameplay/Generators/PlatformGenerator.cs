using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SCSIA
{
    public class PlatformGenerator : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Platform generator config")]
        [SerializeField] private PlatformGeneratorConfig _platformGeneratorConfig;

        [Space]
        [SerializeField] private Transform _platformPoolParent;
        [SerializeField] private Transform _playerTarget;
        [SerializeField] private Transform _bottomTarget;

        private int _playerStage;

        private float _screenMinX;
        private float _screenMaxX;

        private List<Queue<BasePlatform>> _platformPool;
        private List<List<BasePlatform>> _platformOnline;

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Awake()
        {
            Initialization();
        }

        private void FixedUpdate()
        {
            UpdateOnlinePlatformList();
        }

        private void Initialization()
        {
            // current player stage (bottom platform)
            _playerStage = 0;
            // get screen size
            _screenMinX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            _screenMaxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            // create pools for platforms
            _platformPool = new List<Queue<BasePlatform>>();
            for (int i = 0; i < _platformGeneratorConfig.PlatformPrefabs.Count; i++)
                _platformPool.Add(new Queue<BasePlatform>());
            _platformOnline = new List<List<BasePlatform>>();
            // update online platform list
            UpdateOnlinePlatformList();
        }

        private void UpdateOnlinePlatformList()
        {
            // get player position
            int playerStage = 0;
            for (int i = _platformOnline.Count - 1; i >= 0; i--)
                if (_playerTarget.position.y > _platformOnline[i].First().transform.position.y)
                {
                    playerStage = _platformOnline[i].First().Stage;
                    break;
                }
            // update online platform list
            if (_playerStage != playerStage || _platformOnline.Count == 0)
            {
                _playerStage = playerStage;
                GameData.SetStage(playerStage);
                // calculate
                int countPlatformsUp = (_platformOnline.Count == 0) ? 0 : _platformOnline.Last().First().Stage - _playerStage;
                int countPlatformsDown = (_platformOnline.Count == 0) ? 0 : _playerStage - _platformOnline.First().First().Stage;
                // drop
                if (countPlatformsUp > _platformGeneratorConfig.MaxSpawnByDirection)
                    DropPlatform(countPlatformsUp - _platformGeneratorConfig.MaxSpawnByDirection, 1);
                if (countPlatformsDown > _platformGeneratorConfig.MaxSpawnByDirection)
                    DropPlatform(countPlatformsDown - _platformGeneratorConfig.MaxSpawnByDirection, -1);
                // spawn
                if (countPlatformsUp < _platformGeneratorConfig.MaxSpawnByDirection)
                    GeneratePlatform(_platformGeneratorConfig.MaxSpawnByDirection - countPlatformsUp, 1);
                if (countPlatformsDown < _platformGeneratorConfig.MaxSpawnByDirection)
                    GeneratePlatform(_platformGeneratorConfig.MaxSpawnByDirection - countPlatformsDown, -1);
            }
        }

        private void GeneratePlatform(int count, int direction)
        {
            for (int i = 0; i < count; i++)
            {
                // platform can be spawed between 0 and maxStage
                int nextStage = (_platformOnline.Count() > 0) ? (((direction == 1) ? _platformOnline.Last().First().Stage : _platformOnline.First().First().Stage) + direction) : 1;
                if (nextStage < 1 || nextStage > _platformGeneratorConfig.MaxStage)
                    return;
                // generate count platform by this stage. result count can be lower if stage does not have free space
                int maxPlatformsByCurrentStage = Random.Range(_platformGeneratorConfig.MinPlatformsByStage, _platformGeneratorConfig.MaxPlatformsByStage + 1);
                List<BasePlatform> platformGroup = new List<BasePlatform>();
                while (true)
                {
                    // stage full
                    if (platformGroup.Count() == maxPlatformsByCurrentStage)
                        break;
                    // prepare new platform
                    PlatformPlacePointInfo platformPlacePointInfo = CalculatePlatformPlaceInfo(platformGroup);
                    BasePlatform platform = GetPlatformFromPool(Random.Range(0, _platformGeneratorConfig.PlatformPrefabs.Count()));
                    platform.SetRandomSkin();
                    if (!platform.CorrectPlatformPlacePointInfo(ref platformPlacePointInfo))
                    {
                        ReturnPlatformToPool(platform);
                        if (platformGroup.Count() == 0)
                            continue;
                        else
                            break;
                    }
                    // generate platform X
                    float platformX = Random.Range(platformPlacePointInfo.minX, platformPlacePointInfo.maxX);
                    // generate platform Y
                    float platformY = _bottomTarget.position.y + _platformGeneratorConfig.PlatformY * nextStage + Random.Range(0, _platformGeneratorConfig.PlatformYDiff) * direction;
                    // place paltform
                    platform.transform.position = new Vector3(platformX, platformY, 0);
                    platform.Stage = nextStage;
                    // spawn type
                    SpawnType spawnType = GetSpawnType();
                    if (spawnType == SpawnType.Bonus)
                        platform.SetRandomBonus();
                    else if (spawnType == SpawnType.Enemy)
                        platform.SetRandomEnemy();
                    // activate
                    platform.gameObject.SetActive(true);
                    platformGroup.Add(platform);
                }
                if (direction == 1)
                    _platformOnline.Add(platformGroup);
                else
                    _platformOnline.Insert(0, platformGroup);
            }
        }

        // drop platforms
        private void DropPlatform(int count, int direction)
        {
            List<BasePlatform> platformGroup;
            for (int i = 0; i < count; i++)
            {
                if (_platformOnline.Count() == 0)
                    return;
                platformGroup = (direction == 1) ? _platformOnline.Last() : _platformOnline.First();
                foreach (BasePlatform p in platformGroup)
                    ReturnPlatformToPool(p);
                platformGroup.Clear();
                _platformOnline.RemoveAt((direction == 1) ? _platformOnline.Count() - 1 : 0);
            }
        }

        private BasePlatform GetPlatformFromPool(int platformType)
        {
            // return first free platform
            if (_platformPool[platformType].Count > 0)
                return _platformPool[platformType].Dequeue();
            // create new platform to pool if queue for this type is empty
            BasePlatform platform = Instantiate(_platformGeneratorConfig.PlatformPrefabs[platformType], Vector3.zero, Quaternion.identity, _platformPoolParent);
            platform.PlatformType = platformType;
            platform.gameObject.SetActive(false);
            return platform;
        }

        private void ReturnPlatformToPool(BasePlatform basePlatform)
        {
            basePlatform.gameObject.SetActive(false);
            _platformPool[basePlatform.PlatformType].Enqueue(basePlatform);
        }

        private PlatformPlacePointInfo CalculatePlatformPlaceInfo(List<BasePlatform> platformGroup)
        {
            PlatformPlacePointInfo result = new PlatformPlacePointInfo(0, 0, 0);
            // case empty stage
            if (platformGroup.Count() == 0)
                result.Set(_screenMinX, _screenMaxX);
            else
            {
                // create list with points
                List<PlatformPlacePointInfo> testList = new List<PlatformPlacePointInfo>();
                // first and last point on the screen
                testList.Add(new PlatformPlacePointInfo(_screenMinX, _screenMinX, 0));
                testList.Add(new PlatformPlacePointInfo(_screenMaxX, _screenMaxX, 0));
                // point from placed platforms
                foreach (BasePlatform p in platformGroup)
                    testList.Add(p.GetPlatformPlacePointInfo());
                // sort list
                testList.Sort((a, b) => a.minX.CompareTo(b.minX));
                // calculate the biggest place
                PlatformPlacePointInfo tmpPlace = new PlatformPlacePointInfo(0, 0, 0);
                for (int i = 1; i < testList.Count(); i++)
                {
                    tmpPlace.Set(testList[i - 1].maxX, testList[i].minX);
                    if (result.width < tmpPlace.width)
                        result = tmpPlace;
                }
            }
            return result;
        }

        private SpawnType GetSpawnType()
        {
            int totalChance = _platformGeneratorConfig.ChanceSpawnBonusOnPlatform;
            totalChance += _platformGeneratorConfig.ChanceSpawnEnemyOnPlatform;
            totalChance += _platformGeneratorConfig.ChanceSpawnNothingOnPlatform;
            int chance = Random.Range(0, totalChance);

            if (chance < _platformGeneratorConfig.ChanceSpawnBonusOnPlatform)
                return SpawnType.Bonus;
            else if (chance < _platformGeneratorConfig.ChanceSpawnBonusOnPlatform + _platformGeneratorConfig.ChanceSpawnEnemyOnPlatform)
                return SpawnType.Enemy;
            else
                return SpawnType.Nothing;
        }
    }
}