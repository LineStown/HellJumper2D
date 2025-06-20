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
        [SerializeField] private float _playerJumpfForce;
        [SerializeField] private float _playerRunSpeed;
        [SerializeField] private PointBonusConfig _pointBonusConfig;

        private InputSystem _inputSystem;
        private Vector3 _playerLeftTurn;
        private Vector3 _playerRightTurn;
        private Vector2 _playerMove;
        private bool _playerJump;
        private Rigidbody2D _platformRigidbody;
        
        private float _screenMinX;
        private float _screenMaxX;

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Awake()
        {
            // input system
            _inputSystem = new InputSystem();
            // direction
            _playerLeftTurn = new Vector3(-_playerRigitbody.transform.localScale.x, _playerRigitbody.transform.localScale.y, _playerRigitbody.transform.localScale.z);
            _playerRightTurn = new Vector3(_playerRigitbody.transform.localScale.x, _playerRigitbody.transform.localScale.y, _playerRigitbody.transform.localScale.z);
            // jump
            _playerJump = false;
            // get screen size
            _screenMinX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            _screenMaxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }

        private void OnEnable()
        {
            _inputSystem.Player.Enable();
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnMove;
            _inputSystem.Player.Jump.performed += OnJump;
        }

        private void OnDisable()
        {
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnMove;
            _inputSystem.Player.Jump.performed -= OnJump;
            _inputSystem.Player.Disable();
        }

        private void FixedUpdate()
        {
            // run
            _playerRigitbody.linearVelocityX = _playerMove.x * _playerRunSpeed + ((_platformRigidbody) ? _platformRigidbody.linearVelocityX : 0);

            // left <> right
            if (_playerRigitbody.position.x < _screenMinX || _playerRigitbody.position.x > _screenMaxX)
                _playerRigitbody.position = new Vector2(_playerRigitbody.position.x * -0.99f, _playerRigitbody.position.y);

            // jump
            if (_playerJump)
            {
                _playerRigitbody.AddForce(Vector3.up * _playerJumpfForce, ForceMode2D.Impulse);
                _playerJump = false;
            }

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
                bonus.OnPlayerEnter();
            }
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
            if (_platformRigidbody && !_playerJump)
                _playerJump = true;
        }
    }
}
