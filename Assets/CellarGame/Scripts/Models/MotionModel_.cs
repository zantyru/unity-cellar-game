using System;
using UnityEngine;


namespace CellarGame
{
    public sealed class MotionModel_ : Model<IEntityInterface>
    {
        #region Fields

		private const string MOUSE_X_INPUT_NAME = "Mouse X";
		private const string MOUSE_Y_INPUT_NAME = "Mouse Y";
		public float MouseXSensitivity = 2.0f;
		public float MouseYSensitivity = 2.0f;
		public bool ClampVerticalRotation = true;
		public float MinimumX = -90.0f;
		public float MaximumX = 90.0f;
		public bool Smooth;
		public float SmoothTime = 5.0f;
		private float _speedMove = 10.0f;
		private float _jumpPower = 10.0f;
		private Vector3 _input;
		private Vector3 _moveVector;
		private Transform _head;
		private Transform _instance;
		private Quaternion _characterTargetRotation;
		private Quaternion _cameraTargetRotation;
		private CharacterController _characterController;

		#endregion


		public override Type ModelType => typeof(MotionModel_); //@DEBUG


        #region ClassLifeCycle

		public void SetCharacterController(CharacterController instance)
		{
			_instance = instance.transform;
			_characterController = instance;
			_head = Camera.main.transform;

			_characterTargetRotation = _instance.localRotation;
			_cameraTargetRotation = _head.localRotation;
		}

		#endregion


		#region IMotor

		public void Move()
		{
			MoveObject();
			//ComputeGravity();

			LookRotation(_instance, _head);
		}

		#endregion


		#region Methods

		private void MoveObject()
		{
			if (_characterController.isGrounded)
			{
				Vector3 desiredMove = _instance.forward * _input.z + _instance.right * _input.x;
				_moveVector.x = desiredMove.x * _speedMove;
				_moveVector.z = desiredMove.z * _speedMove;
			}

			//_moveVector.y = _gravityForce;
			_characterController.Move(_moveVector * Time.deltaTime);
		}

		private void LookRotation(Transform character, Transform camera)
		{
			float desiredYRotation = Input.GetAxis(MOUSE_X_INPUT_NAME) * MouseXSensitivity;
			float desiredXRotation = Input.GetAxis(MOUSE_Y_INPUT_NAME) * MouseYSensitivity;

			_characterTargetRotation *= Quaternion.Euler(0.0f, desiredYRotation, 0.0f);
			_cameraTargetRotation *= Quaternion.Euler(-desiredXRotation, 0.0f, 0.0f);

			if (ClampVerticalRotation)
			{
				_cameraTargetRotation = ClampRotationAroundXAxis(_cameraTargetRotation);
			}

			if (Smooth)
			{
				float smoothTimeInFrame = SmoothTime * Time.deltaTime;
				character.localRotation = Quaternion.Slerp(
					character.localRotation,
					_characterTargetRotation,
					smoothTimeInFrame
				);
				camera.localRotation = Quaternion.Slerp(
					camera.localRotation,
					_cameraTargetRotation,
					smoothTimeInFrame
				);
			}
			else
			{
				character.localRotation = _characterTargetRotation;
				camera.localRotation = _cameraTargetRotation;
			}
		}

		private Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;
			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
			angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}

		#endregion
    }
}