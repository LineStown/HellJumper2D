using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "LevelGeneratorConfig", menuName = "Scriptable Objects/Generator/LevelGeneratorConfig")]
    public class LevelGeneratorConfig : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Timer")]
        [SerializeField] private int _levelTimer;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public int LevelTimer => _levelTimer;
    }
}
