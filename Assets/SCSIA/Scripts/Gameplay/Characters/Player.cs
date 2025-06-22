using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SCSIA
{
    public class Player : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Rigidbody2D _playerRigitbody;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private float _playerJumpfForce;
        [SerializeField] private float _playerRunSpeed;
        [SerializeField] private PointBonusConfig _pointBonusConfig;
        [SerializeField] private float _playerNoJumpTime = 2f;

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioJumpOn;
        [SerializeField] private AudioClip _audioJumpOff;
        [SerializeField] private AudioClip _audioBonus;

        private InputSystem _inputSystem;
        private Vector3 _playerLeftTurn;
        private Vector3 _playerRightTurn;
        private Vector2 _playerMove;
        private bool _playerJump;
        private bool _playerNoJump;
        private Rigidbody2D _platformRigidbody;
        
        private float _screenMinX;
        private float _screenMaxX;

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Awake()
        {
            Initialization();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void FixedUpdate()
        {
            // left <> right
            if (_playerRigitbody.position.x < _screenMinX || _playerRigitbody.position.x > _screenMaxX)
                _playerRigitbody.position = new Vector2(_playerRigitbody.position.x * -0.99f, _playerRigitbody.position.y);

            // jump
            if (_playerJump)
            {
                _playerRigitbody.AddForce(Vector3.up * _playerJumpfForce, ForceMode2D.Impulse);
                _playerJump = false;
                _audioSource.PlayOneShot(_audioJumpOn);
            }

            // falling
            if (_playerRigitbody.linearVelocityY < -10f)
                _playerRigitbody.linearVelocityY = -10f;

            // run
            _playerRigitbody.linearVelocityX = _playerMove.x * _playerRunSpeed + ((_platformRigidbody) ? _platformRigidbody.linearVelocityX : 0);

            _playerAnimator.SetBool("Jump", !_platformRigidbody);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_platformRigidbody)
                return;

            IPlatformCollision platform = collision.gameObject.GetComponentInParent<IPlatformCollision>();
            if (platform != null)
                foreach (ContactPoint2D contact in collision.contacts)
                    if (contact.normal.y > 0.5f)
                    {
                        _platformRigidbody = platform.GetRigidbody();
                        platform.OnPlayerEnter();
                        _audioSource.PlayOneShot(_audioJumpOff);
                        break;
                    }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!_platformRigidbody)
                return;
            
            IPlatformCollision platform = collision.gameObject.GetComponentInParent<IPlatformCollision>();
            if (platform != null && _platformRigidbody == platform.GetRigidbody())
            {
                _platformRigidbody = null;
                platform.OnPlayerExit();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IBonusCollision bonus = collision.gameObject.GetComponentInParent<IBonusCollision>();
            if(bonus != null)
            {
                Debug.Log($"Player got bonus: type {bonus.GetBonusType()} points {_pointBonusConfig.GetPointsByBonus(bonus.GetBonusType())}");
                GameData.AddScore(_pointBonusConfig.GetPointsByBonus(bonus.GetBonusType()));
                bonus.OnPlayerEnter();
                _audioSource.PlayOneShot(_audioBonus);
            }
            IEnemyCollision enemy = collision.gameObject.GetComponentInParent<IEnemyCollision>();
            if(enemy != null && !_playerNoJump)
                StartCoroutine(PlayerNoJump(_playerNoJumpTime));
        }

        private void Initialization()
        {
            // input system
            _inputSystem = new InputSystem();
            // direction
            _playerLeftTurn = new Vector3(-_playerRigitbody.transform.localScale.x, _playerRigitbody.transform.localScale.y, _playerRigitbody.transform.localScale.z);
            _playerRightTurn = new Vector3(_playerRigitbody.transform.localScale.x, _playerRigitbody.transform.localScale.y, _playerRigitbody.transform.localScale.z);
            // jump
            _playerJump = false;
            _playerNoJump = false;
            // get screen size
            _screenMinX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            _screenMaxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }

        private void SubscribeEvents()
        {
            _inputSystem.Player.Enable();
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnMove;
            _inputSystem.Player.Jump.performed += OnJump;
        }

        private void UnsubscribeEvents()
        {
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnMove;
            _inputSystem.Player.Jump.performed -= OnJump;
            _inputSystem.Player.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            // get current move
            _playerMove = context.ReadValue<Vector2>();
            // turn player
            if (_playerMove.x != 0)
                _playerRigitbody.transform.localScale = (_playerMove.x < 0) ? _playerLeftTurn : _playerRightTurn;
            // enable animation run
            _playerAnimator.SetBool("Run", _playerMove.x != 0);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (_platformRigidbody && !_playerJump && !_playerNoJump)
                _playerJump = true;
        }

        private IEnumerator PlayerNoJump(float seconds)
        {
            _playerNoJump = true;
            //Color oldColor= _playerSpriteRenderer.color;
            _playerSpriteRenderer.color = Color.red;
            yield return new WaitForSeconds(seconds);
            _playerNoJump = false;
            _playerSpriteRenderer.color = Color.white;
        }
    }
}
