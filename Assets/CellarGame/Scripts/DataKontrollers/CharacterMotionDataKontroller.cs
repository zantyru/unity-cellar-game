using UnityEngine;
using Sadako;


namespace CellarGame
{
    public sealed class CharacterMotionDataKontroller : DataKontroller
    {
        #region Fields

		private const string HORIZONTAL_AXIS_NAME = "Horizontal";
		private const string VERTICAL_AXIS_NAME = "Vertical";
        private const string MOUSE_X_INPUT_NAME = "Mouse X";
		private const string MOUSE_Y_INPUT_NAME = "Mouse Y";
		private const float GRAVITY_FORCE_REDUCTION = 30.0f;
		private const float GRAVITY_FORCE_IF_GROUNDED = -1.0f;
		public float MouseXSensitivity = 2.0f;
		public float MouseYSensitivity = 2.0f;
		public bool ClampVerticalRotation = true;
		public float MinimumX = -90.0f;
		public float MaximumX = 90.0f;
		public bool Smooth;
		public float SmoothTime = 5.0f;
		private float _speedMove = 10.0f;
		private float _jumpPower = 10.0f;
		private float _gravityForce;
		private Vector3 _moveVector;
        private readonly Transform _headTransform = default;
        private readonly Transform _bodyTransform = default;
        private Quaternion _headQuaternion = default;
        private Quaternion _bodyQuaternion = default;
        private readonly CharacterController _characterController = default;

        #endregion


        #region ClassLifeCycle
        
        public CharacterMotionDataKontroller(GameObject owner) : base(owner)
        {
            _characterController = owner.GetComponent<CharacterController>();
            _headTransform = Camera.main.transform;
            _bodyTransform = _characterController.transform;
            _headQuaternion = _headTransform.localRotation;
            _bodyQuaternion = _bodyTransform.localRotation;
        }

        #endregion


        #region Methods

        public void Move(float deltaTime)
		{
			MoveCharacter(deltaTime);
			ApplyGravity(deltaTime);
			LookRotation(_bodyTransform, _headTransform, deltaTime);
		}

        private void MoveCharacter(float deltaTime)
		{
			if (_characterController.isGrounded)
			{
                Vector3 input = new Vector3(
					Input.GetAxis(HORIZONTAL_AXIS_NAME),
					0.0f,
					Input.GetAxis(VERTICAL_AXIS_NAME)
				);
				Vector3 desiredMove = _bodyTransform.forward * input.z + _bodyTransform.right * input.x;
				_moveVector.x = desiredMove.x * _speedMove;
				_moveVector.z = desiredMove.z * _speedMove;
			}

			_moveVector.y = _gravityForce;
			_characterController.Move(_moveVector * deltaTime);
		}

		private void ApplyGravity(float deltaTime)
		{
			if (!_characterController.isGrounded)
			{
				_gravityForce -= GRAVITY_FORCE_REDUCTION * deltaTime;
			}
			else
			{
				_gravityForce = GRAVITY_FORCE_IF_GROUNDED;
			}

			if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded) _gravityForce = _jumpPower;
		}

		private void LookRotation(Transform character, Transform camera, float deltaTime)
		{
			float desiredYRotation = Input.GetAxis(MOUSE_X_INPUT_NAME) * MouseXSensitivity;
			float desiredXRotation = Input.GetAxis(MOUSE_Y_INPUT_NAME) * MouseYSensitivity;

			_bodyQuaternion *= Quaternion.Euler(0.0f, desiredYRotation, 0.0f);
			_headQuaternion *= Quaternion.Euler(-desiredXRotation, 0.0f, 0.0f);

			if (ClampVerticalRotation)
			{
				_headQuaternion = _headQuaternion.ClampRotationAroundXAxis(MinimumX, MaximumX);
			}

			if (Smooth)
			{
				float smoothTimeInFrame = SmoothTime * deltaTime;
				character.localRotation = Quaternion.Slerp(
					character.localRotation,
					_bodyQuaternion,
					smoothTimeInFrame
				);
				camera.localRotation = Quaternion.Slerp(
					camera.localRotation,
					_headQuaternion,
					smoothTimeInFrame
				);
			}
			else
			{
				character.localRotation = _bodyQuaternion;
				camera.localRotation = _headQuaternion;
			}
		}

		#endregion
    }
}