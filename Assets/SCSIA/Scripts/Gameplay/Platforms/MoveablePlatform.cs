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
        private PlatformPlacePointInfo _platformPlacePointInfo;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        private MoveablePlatformConfig PlatformConfig => _platformConfig as MoveablePlatformConfig;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public override bool CorrectPlatformPlacePointInfo(ref PlatformPlacePointInfo platformPlacePointInfo)
        {
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + _platformMD, platformPlacePointInfo.maxX - _platformMD);
            float minX = Random.Range(0, platformPlacePointInfo.width - _platformRendererWidth);
            float maxX = Random.Range(minX + _platformRendererWidth, platformPlacePointInfo.width);
            platformPlacePointInfo.Set(platformPlacePointInfo.minX + minX, platformPlacePointInfo.minX + maxX);
            _minX = platformPlacePointInfo.minX;
            _maxX = platformPlacePointInfo.maxX;
            _platformPlacePointInfo.Set(platformPlacePointInfo.minX - _platformMD, platformPlacePointInfo.maxX + _platformMD);
            _speed = Random.Range(PlatformConfig.MinSpeed, PlatformConfig.MaxSpeed);
            _direction = Random.Range(0, 2) * 2 - 1;
            return platformPlacePointInfo.width >= _platformRendererWidth; 
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
