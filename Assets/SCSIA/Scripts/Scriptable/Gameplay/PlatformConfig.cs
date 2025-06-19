using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "PlatformConfig", menuName = "Scriptable Objects/PlatformConfig")]
    public class PlatformRendererConfig : ScriptableObject
    {
        [Header("Platform renderer prefabs")]
        [SerializeField] public GameObject[] _platformRendererPrefabs;

        [Header("Platform bonus prefabs")]
        [SerializeField] public GameObject[] _platformBonusPrefabs;

        [Header("Platform enemy prefabs")]
        [SerializeField] public GameObject[] _platformEnemyPrefabs;
    }
}