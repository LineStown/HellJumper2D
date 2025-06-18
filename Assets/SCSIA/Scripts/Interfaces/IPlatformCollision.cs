using UnityEngine;

namespace SCSIA
{
    public interface IPlatformCollision
    {
        public void OnPlayerEnter();
        public void OnPlayerExit();
        public Rigidbody2D GetRigidbody();
    }
}
