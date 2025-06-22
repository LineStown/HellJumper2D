using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SCSIA
{
    public class ColorRendererEffectAction : BaseRendererEffectAction
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Color Pulse")]
        [SerializeField] private float _colorPulseSpeed = 2f;

        protected Color _originalcolor;
        protected Color _targetColor;
        protected float _elapsed;

        //############################################################################################
        // PROTECTED  METHODS
        //############################################################################################
        protected override void StartInternalExecute()
        {
            // save original color
            _originalcolor = _spriteRenderer.color;
            _targetColor = Color.HSVToRGB(Random.value, 0.8f, 1f);
            _elapsed = 0f;
        }

        protected override void StopInternalExecute()
        {
            // load original color
            _spriteRenderer.color = _originalcolor;
        }
        
        protected void Update()
        {
            if (_active)
            {
                _elapsed += Time.deltaTime;
                float colorT = (Mathf.Sin(_elapsed * _colorPulseSpeed) + 1f) * 0.5f;
                _spriteRenderer.color = Color.Lerp(_originalcolor, _targetColor, colorT);
            }
        }
    }
}
