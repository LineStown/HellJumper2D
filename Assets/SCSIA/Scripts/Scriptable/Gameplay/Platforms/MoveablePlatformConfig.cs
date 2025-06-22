using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "MoveablePlatformConfig", menuName = "Scriptable Objects/Platform/MoveablePlatformConfig")]
    public class MoveablePlatformConfig : BasePlatformConfig
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Platform speed")]
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public float MinSpeed => _minSpeed;
        public float MaxSpeed => _maxSpeed;
    }
}