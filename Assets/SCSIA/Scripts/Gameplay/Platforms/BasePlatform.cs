using UnityEngine;

namespace SCSIA
{
    public abstract class BasePlatform : MonoBehaviour, IPlatformCollision
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Platform renderer")]
        [SerializeField] protected PlatformVisualConfig platformVisualConfig;
        [SerializeField] protected Transform _platformRendererSpawnPoint;

        [Header("Platform bonus")]
        [SerializeField] protected Transform _platformBonusSpawnPoint;

        [Header("Platform enemy")]
        [SerializeField] protected Transform _platformEnemySpawnPoint;

        [Header("Platform properties")]
        [SerializeField] protected Rigidbody2D _platformRigidbody;

        private int _platformRendererType;
        protected float _platformRendererWidth;

        protected PlatformPlacePointInfo _platformPlacePointInfo;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public int Stage { set; get; } = 0;
        public int PlatformType { set; get; } = 0;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public void SetRandomSkin()
        {
            int platformRendererType = Random.Range(0, platformVisualConfig._platformRendererPrefabs.Length);
            if (_platformRendererType != platformRendererType)
            {
                // destroy old skin
                foreach (Transform child in _platformRendererSpawnPoint)
                    Destroy(child.gameObject);
                // create new skin
                Instantiate(platformVisualConfig._platformRendererPrefabs[platformRendererType], _platformRendererSpawnPoint.position, Quaternion.identity, _platformRendererSpawnPoint);
                _platformRendererWidth = _platformRendererSpawnPoint.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
                _platformRendererType = platformRendererType;
            }
        }

        public virtual bool CorrectPlatformPlacePointInfo(ref PlatformPlacePointInfo platformPlacePointInfo)
        {
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + _platformRendererWidth / 2f, platformPlacePointInfo.maxX - _platformRendererWidth / 2f);
            _platformPlacePointInfo = platformPlacePointInfo;
            _platformPlacePointInfo.Set(_platformPlacePointInfo.minX - _platformRendererWidth / 2f, _platformPlacePointInfo.maxX + _platformRendererWidth / 2f);
            return platformPlacePointInfo.width > _platformRendererWidth;           
        }

        public virtual PlatformPlacePointInfo GetPlatformPlacePointInfo()
        {
            return new PlatformPlacePointInfo(this.gameObject.transform.position.x - _platformRendererWidth / 2f, this.gameObject.transform.position.x + _platformRendererWidth / 2f, _platformRendererWidth);
        }

        public virtual void OnPlayerEnter()
        { }
        public virtual void OnPlayerExit()
        { }
        public Rigidbody2D GetRigidbody()
        {
            return _platformRigidbody;
        }

        //############################################################################################
        // PROTECTED METHODS
        //############################################################################################
        protected virtual void Awake()
        {
            _platformRendererType = -1;
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
    }
}
