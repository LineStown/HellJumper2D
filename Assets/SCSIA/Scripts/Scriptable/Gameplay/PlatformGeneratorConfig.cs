using SCSIA;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "PlatformGeneratorConfig", menuName = "Scriptable Objects/PlatformGeneratorConfig")]
    public class PlatformGeneratorConfig : ScriptableObject
    {
        [Header("Available platform array")]
        public BasePlatform[] platformPrefabs;

        [Header("Config")]
        public int maxStage = 20;
        public int maxSpawnByDirection = 5;
        public float platformMinYFromPrevious = 2.5f;
        public float platformMaxYFromPrevious = 3.0f;
        public int minPlatformsByStage = 1;
        public int maxPlatformsByStage = 6;

        [Header("Chance spawn moavable platform")]
        [Range(0, 100)] public int chanceSpawnMoveablePlatform = 30;

        [Header("Chance spawn bonus on platform")]
        [Range(0, 100)] public int chanceSpawnBonusonPlatform = 50;
    }
}