using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "PlatformGeneratorConfig", menuName = "Scriptable Objects/PlatformGeneratorConfig")]
    public class PlatformGeneratorConfig : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Available platform array")]
        [SerializeField] private List<BasePlatform> _platformPrefabs;

        [Header("Config")]
        [SerializeField] private int _maxStage = 20;
        [SerializeField] private int _maxSpawnByDirection = 5;
        [SerializeField] private float _platformY = 3.0f;
        [SerializeField] private float _platformYDiff = 0.5f;
        [SerializeField] private int _minPlatformsByStage = 1;
        [SerializeField] private int _maxPlatformsByStage = 6;

        [Header("Spawn chance")]
        [Range(0, 100)][SerializeField] private int _chanceSpawnBonusOnPlatform = 25;        
        [Range(0, 100)][SerializeField] private int _chanceSpawnEnemyOnPlatform = 25;
        [Range(0, 100)][SerializeField] private int _chanceSpawnNothingOnPlatform = 25;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public IReadOnlyList<BasePlatform> PlatformPrefabs => _platformPrefabs;
        public int MaxStage => _maxStage;
        public int MaxSpawnByDirection => _maxSpawnByDirection;
        public float PlatformY => _platformY;
        public float PlatformYDiff => _platformYDiff;
        public int MinPlatformsByStage => _minPlatformsByStage;
        public int MaxPlatformsByStage => _maxPlatformsByStage;
        public int ChanceSpawnBonusOnPlatform => _chanceSpawnBonusOnPlatform;
        public int ChanceSpawnEnemyOnPlatform => _chanceSpawnEnemyOnPlatform;
        public int ChanceSpawnNothingOnPlatform => _chanceSpawnNothingOnPlatform;
    }
}