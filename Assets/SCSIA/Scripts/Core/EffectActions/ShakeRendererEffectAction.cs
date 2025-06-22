using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SCSIA
{
    public class ShakeRendererEffectAction : BaseRendererEffectAction
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Shake settings")]
        [SerializeField] protected float _shakeIntensity = 0.05f;
        [SerializeField] protected float _shakeFrequency = 20f;

        protected Vector3 _originalPosition;

        //############################################################################################
        // PROTECTED  METHODS
        //############################################################################################
        protected override void StartInternalExecute()
        {
            // save original position
            _originalPosition = _spriteRenderer.transform.position;
        }

        protected override void StopInternalExecute()
        {
            // load original position
            _spriteRenderer.transform.position = _originalPosition;
        }
        
        protected void FixedUpdate()
        {
            if (_active)
            {
                float time = Time.time;
                float shakeX = Mathf.PerlinNoise(time * _shakeFrequency, 0f) - 0.5f;
                float shakeY = Mathf.PerlinNoise(0f, time * _shakeFrequency) - 0.5f;
                Vector3 shakeOffset = new Vector3(shakeX, shakeY, 0f) * _shakeIntensity;

                _spriteRenderer.transform.position = _originalPosition + shakeOffset;
            }
        }
    }
}
