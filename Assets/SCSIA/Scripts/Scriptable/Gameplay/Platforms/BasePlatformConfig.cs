using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    public abstract class BasePlatformConfig : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Platform renderer prefabs")]
        [SerializeField] private List<GameObject> _platformRendererPrefabs;
        [Header("Platform bonus prefabs")]
        [SerializeField] private List<GameObject> _platformBonusPrefabs;
        [Header("Platform enemy prefabs")]
        [SerializeField] private List<GameObject> _platformEnemyPrefabs;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public IReadOnlyList<GameObject> PlatformRendererPrefabs => _platformRendererPrefabs;
        public IReadOnlyList<GameObject> PlatformBonusPrefabs => _platformBonusPrefabs;
        public IReadOnlyList<GameObject> PlatformEnemyPrefabs => _platformEnemyPrefabs;
    }
}