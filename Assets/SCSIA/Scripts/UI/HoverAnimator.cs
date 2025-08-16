using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SCSIA
{
    public class HoverAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Hover Settings")]
        [SerializeField] private Image _image;
        [SerializeField] private Image _hoverImage;
        [SerializeField] private float _fadeSpeed = 10.0f;

        [Header("Scale Settings")]
        [SerializeField] private Vector3 _normalScale = Vector3.one;
        [SerializeField] private Vector3 _hoverScale = Vector3.one * 1.1f;
        [SerializeField] private float _scaleSpeed = 10f;

        [Header("Sound Settings")]
        [SerializeField] private AudioClip _hoverSound;

        private float _targetFade;
        private Vector3 _targetScale;
       
        private void Awake()
        {
            _targetFade = 0;
            transform.localScale = _normalScale;
            _targetScale = _normalScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _targetFade = 1;
            _targetScale = _hoverScale;
            Bootstrap.AudioManager.PlaySFX(_hoverSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _targetFade = 0;
            _targetScale = _normalScale;
        }

        private void Update()
        {
            _hoverImage.fillAmount = Mathf.Lerp(_hoverImage.fillAmount, _targetFade, Time.deltaTime * _fadeSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime * _scaleSpeed);
        }
    }
}