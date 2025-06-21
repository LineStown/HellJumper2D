using UnityEngine;

namespace SCSIA
{
    public class BottomPlatform : MonoBehaviour, IPlatformCollision
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Rigidbody2D _platformRigidbody;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void OnPlayerEnter()
        { }

        public void OnPlayerExit()
        { }

        public Rigidbody2D GetRigidbody()
        {
            return _platformRigidbody;
        }
    }
}
