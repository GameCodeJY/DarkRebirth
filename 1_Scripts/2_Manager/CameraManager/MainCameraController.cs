/*
 *  Coder       :   JY
 *  Last Update :   2025. 02. 24.
 *  Information :   Main Camera Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Cinemachine;
    using Unity.Mathematics;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MainCameraController : MonoBehaviour // Data Field
    {
        #region Const Value
        // Quarter View �� �⺻ ������ ��
        private Vector3 DISTANCE_FROM_MAIN_PLAYER_QUATER_VIEW = new Vector3(5.826f, 8.999f, -8.058f);
        #endregion

        #region Member Value
        /// <summary> Camera Manager </summary>
        private CameraManager _cameraManager = default;
        /// <summary> Instance Manager </summary>
        private PlayerManager _playerManager = default;
        /// <summary> Cinemachine Virtual Camera </summary>
        public CinemachineVirtualCamera CinemachineVirtualCamera = default;
        /// <summary> Cinemachine Brain </summary>
        public CinemachineBrain CinemachineBrain = default;

        /// <summary> �ʱ� ī�޶� ȸ���� </summary>
        private Quaternion _quaternionCameraRotation = default;
        /// <summary> Camera Degree Rotion Value </summary>
        private float _rotationCameraDegree = 0.0f;

        Coroutine _cameraShakingCoroutine = null;

        private float _shakingStartMagnitude = 0.2f;
        private float _shakingDuration = 0.2f;
        private Transform _cameraTarget = null;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class MainCameraController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            _playerManager = MainSystem.Instance.PlayerManager;
            _cameraManager = MainSystem.Instance.CameraManager;
            _cameraTarget = MainSystem.Instance.PlayerManager.MainPlayer.transform;

            _cameraManager.SignUpMainCameraController(this);
        }

        private void Initialize()
        {
            _quaternionCameraRotation = Quaternion.Euler(40.224f, -41.71f, 0.0f);
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class MainCameraController : MonoBehaviour // Main
    {
        private void Awake()
        {
            Allocate();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            // �ʱ� ȸ���� ���� (�ʿ� ��)
            transform.rotation = _quaternionCameraRotation;
        }

        private void Update()
        {
            if (_cameraManager.IsQuaterView.Equals(false) || _cameraShakingCoroutine != null)
                return;

            Vector3 nextPosition = GetQuarterViewCameraPositionByTarget();
            transform.position = nextPosition;

            // ī�޶� �׻� �÷��̾ �ٶ󺸵��� ��
            transform.LookAt(_cameraTarget.position);
        }
    }

    /// <summary>
    /// Set, Get
    /// </summary>
    public partial class MainCameraController : MonoBehaviour // Set, Get
    {
        public void SetCameraTarget(Transform target)
        {
            _cameraTarget = target;
        }

        public void SetCameraRotation(float rotationCameraDegree)
        {
            _rotationCameraDegree = rotationCameraDegree;
        }

        private Vector3 GetQuarterViewCameraPositionByTarget()
        {
            Vector3 nextPosition = Vector3.zero;

            // Y�� ���� -90�� ȸ��(�ð����) ����
            Quaternion rotation90 = Quaternion.Euler(0.0f, _rotationCameraDegree, 0.0f);
            // ȸ���� �������� �÷��̾� ��ġ�� ����
            nextPosition = _cameraTarget.position + rotation90 * DISTANCE_FROM_MAIN_PLAYER_QUATER_VIEW;

            return nextPosition;
        }
    }

    /// <summary>
    /// Camera Shaking
    /// </summary>
    public partial class MainCameraController : MonoBehaviour // Camera Shaking
    {
        public void StartCameraShaking(Vector3 direction, float shakingDuration, float shakingStartMagnitude)
        {
            if(_cameraShakingCoroutine != null)
            {
                StopCoroutine(_cameraShakingCoroutine);
            }

            _cameraShakingCoroutine = StartCoroutine(CameraShaking(direction.normalized, shakingDuration, shakingStartMagnitude));
        }

        public IEnumerator CameraShaking(Vector3 direction, float shakingDuration = 0.2f, float shakingStartMagnitude = 0.2f)
        {
            float time = 0f;
            direction.y = 0;

            while (shakingDuration > time)
            {
                time += Time.deltaTime;

                float percent = Mathf.Clamp01(1f - (time / _shakingDuration));
                float magnitude = math.lerp(0f, shakingStartMagnitude, percent);

                // �÷��̾ �������� ����
                Vector3 nextPosition = GetQuarterViewCameraPositionByTarget();
                transform.position = nextPosition + direction * magnitude;

                // ī�޶� �׻� �÷��̾ �ٶ󺸵��� ��
                transform.LookAt(_playerManager.MainPlayer.transform.position);

                // �������� �ݴ�������� ����ŷ
                direction = -direction;

                yield return null;
            }

            _cameraShakingCoroutine = null;
        }
    }
}