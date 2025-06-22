using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "PointBonusConfig", menuName = "Scriptable Objects/PointBonusConfig")]
    public class PointBonusConfig : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Bonuses points")]
        [SerializeField] private List<BonusPoints> _inputBonusPoints;
        private Dictionary<EBonusType, int> _bonusPoints;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public int GetPointsByBonus(EBonusType eBonusType)
        {
            if (_bonusPoints == null)
                Repack();
            return _bonusPoints[eBonusType];
        }

        //############################################################################################
        // PRIVATE  METHODS
        //############################################################################################
        private void Repack()
        {
            _bonusPoints = new Dictionary<EBonusType, int>();
            foreach (BonusPoints value in _inputBonusPoints)
                _bonusPoints[value.bonusType] = value.bonusPoints;
        }
    }

    [System.Serializable]
    public class BonusPoints
    {
        public EBonusType bonusType;
        public int bonusPoints;
    }
}
