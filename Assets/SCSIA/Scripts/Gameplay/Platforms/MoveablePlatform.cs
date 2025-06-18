using UnityEngine;

namespace SCSIA
{
    public class MoveablePlatform : BasePlatform
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Platform Speed")]
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private float _minX;
        private float _maxX;
        private float _speed;
        private int _direction;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public override bool CorrectPlatformPlacePointInfo(ref PlatformPlacePointInfo platformPlacePointInfo)
        {
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + _platformRendererWidth / 2f, platformPlacePointInfo.maxX - _platformRendererWidth / 2f);
            float minX = Random.Range(0, platformPlacePointInfo.width - _platformRendererWidth);
            float maxX = Random.Range(minX + _platformRendererWidth, platformPlacePointInfo.width);
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + minX, platformPlacePointInfo.minX + maxX);
            _platformPlacePointInfo = platformPlacePointInfo;
            _minX = _platformPlacePointInfo.minX;
            _maxX = _platformPlacePointInfo.maxX;
            _platformPlacePointInfo.Set(_platformPlacePointInfo.minX - _platformRendererWidth / 2f, _platformPlacePointInfo.maxX + _platformRendererWidth / 2f);
            _speed = Random.Range(_minSpeed, _maxSpeed);
            _direction = Random.Range(0, 2) * 2 - 1;
            return platformPlacePointInfo.width > _platformRendererWidth;
        }

        public override PlatformPlacePointInfo GetPlatformPlacePointInfo()
        {
            return _platformPlacePointInfo;
        }

        //############################################################################################
        // PRIVATE  METHODS
        //############################################################################################
        // fixed update
        private void FixedUpdate()
        {
            _platformRigidbody.linearVelocityX = _direction * _speed;
            if (_platformRigidbody.position.x <= _minX)
                _direction = 1;
            if (_platformRigidbody.position.x >= _maxX)
                _direction = -1;
        }
    }
}
