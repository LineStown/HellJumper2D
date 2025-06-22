using UnityEngine;

namespace SCSIA
{
    public interface IBonusCollision
    {
        public void OnPlayerEnter();
        public void OnPlayerExit();
        public EBonusType GetBonusType();
    }
}
