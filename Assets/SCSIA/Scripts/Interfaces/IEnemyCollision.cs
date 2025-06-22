using UnityEngine;

namespace SCSIA
{
    public interface IEnemyCollision
    {
        public void OnPlayerEnter();
        public void OnPlayerExit();
    }
}
