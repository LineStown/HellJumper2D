using UnityEngine;

namespace SCSIA
{
    public class FollowCamera : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Target")]
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _followSpeed;

        [Space]
        [Header("Follow Axis")]
        [SerializeField] private FollowCameraAxis _x;
        [SerializeField] private FollowCameraAxis _y;
        [SerializeField] private FollowCameraAxis _z;

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Awake()
        {
            // set start place
            transform.position = GetFollowTargetPosition();
        }

        private void Update()
        {
            // follow
            transform.position = Vector3.Lerp(transform.position, GetFollowTargetPosition(), _followSpeed * Time.deltaTime);
        }

        private Vector3 GetFollowTargetPosition()
        {
            var followTargetPosition = new Vector3(
                _x.follow ? Mathf.Clamp(_followTarget.position.x, _x.minV, _x.maxV) : transform.position.x,
                _y.follow ? Mathf.Clamp(_followTarget.position.y, _y.minV, _y.maxV) : transform.position.y,
                _z.follow ? Mathf.Clamp(_followTarget.position.z, _z.minV, _z.maxV) : transform.position.z
            );
            return followTargetPosition;
        }
    }

    [System.Serializable]
    public class FollowCameraAxis
    {
        public bool follow;
        public float minV;
        public float maxV;
    }
}