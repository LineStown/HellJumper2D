using System.Collections.Generic;
using UnityEngine;

namespace SCSIA
{
    [CreateAssetMenu(fileName = "BonusConfig", menuName = "Scriptable Objects/BonusConfig")]
    public class BonusConfig : ScriptableObject
    {
        [Header("Bonuses points")]
        [SerializeField] private List<BonusPoints> _inputBonusPoints;
        private Dictionary<EBonusType, int> _bonusPoints;

        public int GetPointsByBonus(EBonusType eBonusType)
        {
            if (_bonusPoints == null)
                Repack();
            return _bonusPoints[eBonusType];
        }

        private void Repack()
        {
            _bonusPoints = new Dictionary<EBonusType, int>();
            foreach (BonusPoints value in _inputBonusPoints)
                _bonusPoints[value.bonusType] = value.bonuPpoints;
        }
    }

    [System.Serializable]
    public class BonusPoints
    {
        public EBonusType bonusType;
        public int bonuPpoints;
    }
}
