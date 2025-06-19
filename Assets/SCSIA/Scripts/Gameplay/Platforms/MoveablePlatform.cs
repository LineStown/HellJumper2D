using UnityEngine;

namespace SCSIA
{
    public class MoveablePlatform : BasePlatform
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        private float _minX;
        private float _maxX;
        private float _speed;
        private int _direction;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        private MoveablePlatformConfig PlatformConfig => _platformConfig as MoveablePlatformConfig;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public override bool CorrectPlatformPlacePointInfo(ref PlatformPlacePointInfo platformPlacePointInfo)
        {
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + _platformRendererWidth / 2f, platformPlacePointInfo.maxX - _platformRendererWidth / 2f);
            float minX = Random.Range(_platformRendererWidth / 4f, platformPlacePointInfo.width - _platformRendererWidth);
            float maxX = Random.Range(minX + _platformRendererWidth, platformPlacePointInfo.width - _platformRendererWidth / 4f);
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + minX, platformPlacePointInfo.minX + maxX);
            _platformPlacePointInfo = platformPlacePointInfo;
            _minX = _platformPlacePointInfo.minX;
            _maxX = _platformPlacePointInfo.maxX;
            _platformPlacePointInfo.Set(_platformPlacePointInfo.minX - _platformRendererWidth / 2f, _platformPlacePointInfo.maxX + _platformRendererWidth / 2f);
            _speed = Random.Range(PlatformConfig.MinSpeed, PlatformConfig.MaxSpeed);
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
