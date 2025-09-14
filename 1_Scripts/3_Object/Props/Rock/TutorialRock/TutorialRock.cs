/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 15.
 *  Information :   Tutorial Rock
 */

namespace MainSystem
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // Data Field
    {
        #region Const Value
        /// <summary> 바위 Life Time </summary>
        private const float LIFE_TIME_ROCK = 300.0f;
        /// <summary> 플레이어를 향해 바위가 날아가는 스피드 </summary>
        private const float MOVE_SPEED_ROCK_MOVE_TO_PLAYER = 10f;
        /// <summary> 바위 회전 속도 </summary>
        private const float ROTATION_SPEED_ROCK = 360.0f;
        #endregion

        /// <summary> 바위의 Rigidbody </summary>
        [SerializeField] private Rigidbody RigidbodyRock = default;
        /// <summary> Rock Model Transform </summary>
        [SerializeField] private Transform TransformRockModel = default;
        /// <summary> 바위가 살아있는 시간 </summary>
        private float LifeTimeRock = 0.0f;
        /// <summary> 바위 멈춤 플래그 </summary>
        private bool IsStopRock = false;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // Main
    {
        private void Update()
        {
            CheckRockLife(); // 바위의 Life Time 체크

            MoveOrStopRock(); // 플레이어에 닿아 멈춘 상태가 아니면 이동
        }
    }

    /// <summary>
    /// 바위 LifeCycle
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // 바위 LifeCycle
    {
        /// <summary>
        /// 바위의 Life 체크
        /// </summary>
        private void CheckRockLife()
        {
            LifeTimeRock += Time.deltaTime;
            if (LifeTimeRock > LIFE_TIME_ROCK) // 바위의 Life Time이 넘을 시 비활성화
            {
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 바위 움직임
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // 바위 움직임
    {
        /// <summary>
        /// 바위 움직임 설정
        /// </summary>
        public void SetRockMove()
        {
            IsStopRock = false;
        }

        /// <summary>
        /// 바위 멈춤 설정
        /// </summary>
        public void SetRockStop()
        {
            IsStopRock = true;
        }

        /// <summary>
        /// 바위 움직일 지 멈출 지
        /// </summary>
        private void MoveOrStopRock()
        {
            // 바위 항상 회전
            TransformRockModel.Rotate(Vector3.left, ROTATION_SPEED_ROCK * Time.deltaTime, Space.Self);

            // 플레이어에 닿아 멈춘 상태가 아니면 이동 & 회전
            if (IsStopRock.Equals(false))
            {
                // 앞으로 이동
                RigidbodyRock.isKinematic = false;
                transform.Translate(Vector3.back * MOVE_SPEED_ROCK_MOVE_TO_PLAYER * Time.deltaTime, Space.World);
            }
            else
            {
                RigidbodyRock.isKinematic = true;
            }
        }
    }
}