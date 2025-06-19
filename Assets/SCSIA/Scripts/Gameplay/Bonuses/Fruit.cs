using UnityEngine;

namespace SCSIA
{
    public class Bonus : MonoBehaviour, IBonusCollision
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private EBonusType _eBonusType;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public void OnPlayerEnter()
        {
            Destroy(gameObject);
        }
        public void OnPlayerExit()
        { }
        public EBonusType GetBonusType()
        {
            return _eBonusType;
        }
    }
}
