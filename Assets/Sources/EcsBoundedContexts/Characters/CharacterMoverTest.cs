using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.EcsBoundedContexts.Characters
{
    public class CharacterMoverTest : MonoBehaviour
    {
        public enum PlayerState
        {
            Idle,       // Покой
            Moving,     // Движение
            JumpStart,  // Начало прыжка
            Jumping,    // В воздухе после прыжка
            Falling,    // Падение (сошёл с уступа)
            Landing     // Приземление
        }
        
        [Header("Скорость движения")] [SerializeField]
        private float moveSpeed = 7f;

        [SerializeField] private float airControlMultiplier = 0.7f;
        [SerializeField] private float rotationSmoothTime = 0.1f;

        [Header("Прыжок")] [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float jumpDuration = 0.5f;
        [SerializeField] private float fallSpeed = 15f;
        [SerializeField] private float gravity = 25f;
        [SerializeField] private float groundCheckOffset = 0.1f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Камера")] [SerializeField] private Transform cameraTransform;

        // Input System
        private InputSystem_Actions _controls;

        // Компоненты
        private CharacterController _controller;

        // Состояние
        private PlayerState _currentState;
        private float _rotationVelocity;
        private bool _isGrounded;
        private float _jumpTimer;
        private float _verticalVelocity;
        private Vector3 _jumpStartPosition;

        // Ввод
        private Vector2 _moveInput;
        private bool _jumpPressed;

        // Параметры прыжка
        private float _jumpVelocity;
        private float _gravityValue;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            if (cameraTransform == null)
                cameraTransform = Camera.main.transform;

            _controls = new InputSystem_Actions();
            _controls.Player.Move.performed += OnMove;
            _controls.Player.Move.canceled += OnMove;
            _controls.Player.Jump.performed += OnJump;

            CalculateJumpParameters();
            SetState(PlayerState.Idle);
        }

        private void CalculateJumpParameters()
        {
            float timeToApex = jumpDuration / 2f;
            _gravityValue = (2f * jumpHeight) / (timeToApex * timeToApex);
            _jumpVelocity = _gravityValue * timeToApex;
        }

        private void OnValidate()
        {
            float timeToApex = jumpDuration / 2f;
            _gravityValue = (2f * jumpHeight) / (timeToApex * timeToApex);
            _jumpVelocity = _gravityValue * timeToApex;
        }

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void OnMove(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector2>();
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) _jumpPressed = true;
        }

        private void Update()
        {
            CheckGround();

            switch (_currentState)
            {
                case PlayerState.Idle:
                    UpdateIdle();
                    break;
                case PlayerState.Moving:
                    UpdateMoving();
                    break;
                case PlayerState.JumpStart:
                    UpdateJumpStart();
                    break;
                case PlayerState.Jumping:
                    UpdateJumping();
                    break;
                case PlayerState.Falling:
                    UpdateFalling();
                    break;
                case PlayerState.Landing:
                    UpdateLanding();
                    break;
            }

            _jumpPressed = false;
        }

        // ==================== УПРАВЛЕНИЕ СОСТОЯНИЯМИ ====================

        private void SetState(PlayerState newState)
        {
            // Выход из предыдущего состояния
            switch (_currentState)
            {
                case PlayerState.JumpStart:
                    break;
                case PlayerState.Jumping:
                    break;
                case PlayerState.Falling:
                    break;
                case PlayerState.Landing:
                    break;
            }

            // Вход в новое состояние
            switch (newState)
            {
                case PlayerState.Idle:
                    _verticalVelocity = -2f;
                    break;
                case PlayerState.Moving:
                    _verticalVelocity = -2f;
                    break;
                case PlayerState.JumpStart:
                    _jumpTimer = 0f;
                    _jumpStartPosition = transform.position;
                    _verticalVelocity = _jumpVelocity;
                    break;
                case PlayerState.Jumping:
                    break;
                case PlayerState.Falling:
                    _verticalVelocity = 0f;
                    break;
                case PlayerState.Landing:
                    _verticalVelocity = -2f;
                    break;
            }

            _currentState = newState;
            Debug.Log("Состояние: " + _currentState);
        }

        // ==================== ПРОВЕРКА ЗЕМЛИ ====================

        private void CheckGround()
        {
            Vector3 spherePosition = transform.position - Vector3.up * groundCheckOffset;
            float sphereRadius = _controller.radius * 0.9f;

            bool wasGrounded = _isGrounded;
            _isGrounded = Physics.CheckSphere(spherePosition, sphereRadius, groundLayer);

            // Приземление
            if (_isGrounded && !wasGrounded)
            {
                if (_currentState == PlayerState.Jumping || _currentState == PlayerState.Falling)
                {
                    SetState(PlayerState.Landing);
                }
            }

            // Потеряли землю во время ходьбы или покоя
            if (!_isGrounded && wasGrounded)
            {
                if (_currentState == PlayerState.Idle || _currentState == PlayerState.Moving)
                {
                    SetState(PlayerState.Falling);
                }
            }
        }

        // ==================== СОСТОЯНИЕ: ПОКОЙ ====================

        private void UpdateIdle()
        {
            // Проверяем ввод
            if (_jumpPressed)
            {
                SetState(PlayerState.JumpStart);
                return;
            }

            if (_moveInput.magnitude > 0.1f)
            {
                SetState(PlayerState.Moving);
                return;
            }

            ApplyGravity();
        }

        // ==================== СОСТОЯНИЕ: ДВИЖЕНИЕ ====================

        private void UpdateMoving()
        {
            // Проверяем ввод
            if (_jumpPressed)
            {
                SetState(PlayerState.JumpStart);
                return;
            }

            if (_moveInput.magnitude < 0.1f)
            {
                SetState(PlayerState.Idle);
                return;
            }

            // Горизонтальное движение
            MoveHorizontally(moveSpeed);
            ApplyGravity();
        }

        // ==================== СОСТОЯНИЕ: НАЧАЛО ПРЫЖКА ====================

        private void UpdateJumpStart()
        {
            _jumpTimer += Time.deltaTime;

            if (_jumpTimer < jumpDuration)
            {
                // Движение по параболе
                float verticalOffset = (_jumpVelocity * _jumpTimer) - (0.5f * _gravityValue * _jumpTimer * _jumpTimer);
                float targetY = _jumpStartPosition.y + verticalOffset;
                float deltaY = targetY - transform.position.y;

                _controller.Move(new Vector3(0, deltaY, 0));
                MoveHorizontally(moveSpeed * airControlMultiplier);
            }
            else
            {
                // Прыжок завершён, переходим в фазу прыжка (ожидание падения)
                SetState(PlayerState.Jumping);
            }
        }

        // ==================== СОСТОЯНИЕ: ПРЫЖОК (ВЕРШИНА) ====================

        private void UpdateJumping()
        {
            // Начинаем падать
            _verticalVelocity -= gravity * Time.deltaTime;
            _verticalVelocity = Mathf.Max(_verticalVelocity, -fallSpeed);
            _controller.Move(new Vector3(0, _verticalVelocity * Time.deltaTime, 0));
            MoveHorizontally(moveSpeed * airControlMultiplier);

            // Если достигли земли — приземляемся (обрабатывается в CheckGround)
        }

        // ==================== СОСТОЯНИЕ: ПАДЕНИЕ ====================

        private void UpdateFalling()
        {
            _verticalVelocity -= gravity * Time.deltaTime;
            _verticalVelocity = Mathf.Max(_verticalVelocity, -fallSpeed);
            _controller.Move(new Vector3(0, _verticalVelocity * Time.deltaTime, 0));
            MoveHorizontally(moveSpeed * airControlMultiplier);

            // Если достигли земли — приземляемся (обрабатывается в CheckGround)
        }

        // ==================== СОСТОЯНИЕ: ПРИЗЕМЛЕНИЕ ====================

        private void UpdateLanding()
        {
            // Короткая задержка или анимация приземления
            // Сразу переходим в покой или движение
            if (_moveInput.magnitude > 0.1f)
            {
                SetState(PlayerState.Moving);
            }
            else
            {
                SetState(PlayerState.Idle);
            }
        }

        // ==================== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ====================

        private void MoveHorizontally(float speed)
        {
            if (_moveInput.magnitude < 0.1f) return;

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = forward * _moveInput.y + right * _moveInput.x;

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationVelocity,
                rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 move = moveDirection * (speed * Time.deltaTime);
            _controller.Move(move);
        }

        private void ApplyGravity()
        {
            Vector3 verticalMove = Vector3.up * (_verticalVelocity * Time.deltaTime);
            _controller.Move(verticalMove);
        }

        private void OnDrawGizmosSelected()
        {
            if (_controller == null) return;
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Vector3 spherePosition = transform.position - Vector3.up * groundCheckOffset;
            Gizmos.DrawWireSphere(spherePosition, _controller.radius * 0.9f);
        }
    }
}