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
        public Rigidbody2D GetRigidbody()
        {
            return _platformRigidbody;
        }
    }
}
