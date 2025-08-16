using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SCSIA
{
    public abstract class BasePlatform : MonoBehaviour, IPlatformCollision
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Platform config")]
        [SerializeField] protected BasePlatformConfig _platformConfig;

        [Header("Platform spawn point")]
        [SerializeField] protected Transform _platformRendererSpawnPoint;
        [SerializeField] protected Transform _platformBonusSpawnPoint;
        [SerializeField] protected Transform _platformEnemySpawnPoint;

        [Header("Platform properties")]
        [SerializeField] protected Rigidbody2D _platformRigidbody;

        [Header("Platform effects")]
        [SerializeField] protected List<BaseRendererEffectAction> _effectActionsOnPlayerCollisions;

        protected int _platformRendererType;
        protected float _platformRendererWidth;
        protected float _platformMD;
        protected const float _widthBetween = 0.1f; 

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public int Stage { set; get; } = 0;
        public int PlatformType { set; get; } = 0;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public virtual void SetRandomSkin()
        {
            ClearPlatform();
            int platformRendererType = Random.Range(0, _platformConfig.PlatformRendererPrefabs.Count());
            if (_platformRendererType != platformRendererType)
            {
                // destroy old skin
                foreach (Transform child in _platformRendererSpawnPoint)
                    Destroy(child.gameObject);
                // create new skin
                Instantiate(_platformConfig.PlatformRendererPrefabs[platformRendererType], _platformRendererSpawnPoint.position, Quaternion.identity, _platformRendererSpawnPoint);
                _platformRendererWidth = _platformRendererSpawnPoint.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
                _platformMD = _platformRendererWidth * (0.5f + _widthBetween);

                _platformRendererType = platformRendererType;
            }
        }

        public virtual void SetRandomBonus()
        {
            int platformBonusType = Random.Range(0, _platformConfig.PlatformBonusPrefabs.Count());
            Instantiate(_platformConfig.PlatformBonusPrefabs[platformBonusType], _platformBonusSpawnPoint.position, Quaternion.identity, _platformBonusSpawnPoint);
        }

        public virtual void SetRandomEnemy()
        {
            int platformEnemyType = Random.Range(0, _platformConfig.PlatformEnemyPrefabs.Count());
            Instantiate(_platformConfig.PlatformEnemyPrefabs[platformEnemyType], _platformEnemySpawnPoint.position, Quaternion.identity, _platformEnemySpawnPoint);
        }

        public virtual bool CorrectPlatformPlacePointInfo(ref PlatformPlacePointInfo platformPlacePointInfo)
        {
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + _platformMD, platformPlacePointInfo.maxX - _platformMD);
            return platformPlacePointInfo.width > 0;       
        }

        public virtual PlatformPlacePointInfo GetPlatformPlacePointInfo()
        {
            return new PlatformPlacePointInfo(this.gameObject.transform.position.x - _platformRendererWidth / 2f, this.gameObject.transform.position.x + _platformRendererWidth / 2f, _platformRendererWidth);
        }

        public void OnPlayerEnter()
        {
            foreach (var action in _effectActionsOnPlayerCollisions)
                action.StartExecute(_platformRendererSpawnPoint.GetComponentInChildren<SpriteRenderer>());
        }

        public void OnPlayerExit()
        {
            foreach (var action in _effectActionsOnPlayerCollisions)
                action.StopExecute();
        }
        
        public Rigidbody2D GetRigidbody()
        {
            return _platformRigidbody;
        }

        //############################################################################################
        // PROTECTED METHODS
        //############################################################################################
        protected virtual void Awake()
        {
            Initialization();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Initialization()
        {
            _platformRendererType = -1;
        }

        private void ClearPlatform()
        {
            DropBonus();
            DropEnemy();
        }

        private void DropBonus()
        {
            foreach (Transform child in _platformBonusSpawnPoint)
                Destroy(child.gameObject);

        }

        private void DropEnemy()
        {
            foreach (Transform child in _platformEnemySpawnPoint)
                Destroy(child.gameObject);
        }
    }
}
