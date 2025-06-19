using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "PlatformConfig", menuName = "Scriptable Objects/PlatformConfig")]
    public class PlatformConfig : ScriptableObject
    {
        [Header("Platform renderer prefabs")]
        [SerializeField] public List<GameObject> _platformRendererPrefabs;

        [Header("Platform bonus prefabs")]
        [SerializeField] public List<GameObject> _platformBonusPrefabs;

        [Header("Platform enemy prefabs")]
        [SerializeField] public List<GameObject> _platformEnemyPrefabs;
    }
}