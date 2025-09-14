/*
 *  Coder       :   JY
 *  Last Update :   2025. 03. 17.
 *  Information :   Player Skill Manager
 */

namespace MainSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class ActionManager : MonoBehaviour // Data Field
    {
        #region Member Value
        /// <summary> Q Skill Active Flag </summary>
        public bool IsActiveSkillQ { get; set; } = false;
        /// <summary> E Skill Active Flag </summary>
        public bool IsActiveSkillE = false;
        /// <summary> SpaceBar Skill Active Flag </summary>
        public bool IsActiveSkillSpaceBar = false;
        /// <summary> R Skill Active Flag </summary>
        public bool IsActiveSkillR = false;
        #endregion

        #region Hit Wall
        /// <summary> Wall Raycast Hit </summary>
        private RaycastHit HitWall = default;
        /// <summary> Wall Hit Flag </summary>
        public bool IsHitWall = false;
        /// <summary> 벽에 가까운 위치 </summary>
        public Vector3 TargetPositionTowardWall = default;
        #endregion

        #region Dash Skill
        /// <summary> Target Rotation </summary>
        public float TargetRotation = 0.0f;
        /// <summary> Vertical Velocity </summary>
        public float VerticalVelocity = default;
        #endregion

        #region Spear Jump Skill
        /// <summary> Spear Jump Start Flag </summary>
        public bool IsStartSpearJump = false;
        /// <summary> Spear Jump Time </summary>
        public float TimeSpearJump = 0.0f;
        /// <summary> Spear Jump 이동 시간 </summary>
        public float TIME_SPEAR_JUMP_MOVE { get; private set; } = 0.93f;
        #endregion

        #region Cool Time
        /// <summary> Space Bar Cool Time </summary>
        public float CoolTimeSpaceBar = 0.0f;
        /// <summary> Space Bar Cool Time </summary>
        public float CoolTimeQKey = 0.0f;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class ActionManager : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
        }

        public void Initialize()
        {
            Allocate();
        }
    }

    /// <summary>
    /// Skill
    /// </summary>
    public partial class ActionManager : MonoBehaviour // Skill
    {
        /// <summary>
        /// 마우스 방향으로 회전할 Quaternion을 계산합니다.
        /// Floor 컴포넌트를 가진 오브젝트에 Raycast가 닿으면, 해당 지점을 기준으로 회전합니다.
        /// </summary>
        /// <returns>계산된 Quaternion (충돌한 Floor가 없으면 현재 회전값 반환)</returns>
        public Quaternion RotateToMousePoint()
        {
            // 카메라 유효성 검사
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Camera.main이 없습니다!");
                return Quaternion.identity;
            }

            // 마우스 위치를 기준으로 Ray 생성
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // 디버그용 레이 그리기 (Scene 뷰에서 1초 동안 표시)
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1.0f);

            // 최대 거리 지정
            float maxDistance = 100f;
            // Ray가 충돌한 모든 오브젝트 정보를 가져옴
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance);

            // 거리 순으로 정렬 (가장 가까운 충돌부터 처리)
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            // 플레이어의 현재 Transform 참조 (MainPlayer의 Transform)
            Transform playerTransform = MainSystem.Instance.PlayerManager.MainPlayer.transform;
            // 기본 회전값은 현재 회전값 (충돌 Floor가 없을 경우)
            Quaternion targetRotation = playerTransform.rotation;

            // 충돌한 오브젝트들 중 Floor 컴포넌트를 가진 오브젝트를 찾음
            foreach (RaycastHit hit in hits)
            {
                // 만약 해당 오브젝트에 Floor 컴포넌트가 없다면 건너뜁니다.
                if (hit.collider.gameObject.GetComponent<Floor>() == null)
                    continue;

                //Debug.Log(hit.collider.name);

                // Floor 오브젝트를 찾은 경우, 충돌 지점을 타겟 위치로 사용
                Vector3 targetPosition = hit.point;
                // 플레이어의 높이를 유지 (수평 회전만 적용)
                targetPosition.y = playerTransform.position.y;

                // 플레이어의 위치에서 타겟 위치로의 방향 벡터 계산
                Vector3 direction = targetPosition - playerTransform.position;
                // 방향 벡터가 충분히 크면 회전값 계산
                if (direction.sqrMagnitude > 0.001f)
                {
                    targetRotation = Quaternion.LookRotation(direction);
                }
                // 첫 번째로 확인된 Floor 충돌만 사용
                break;
            }

            return targetRotation;
        }

        public Vector3 GetMousePointPosition()
        {
            // 마우스 위치를 기준으로 Ray 생성
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Ray가 충돌한 모든 오브젝트를 가져옴
            RaycastHit[] hits = Physics.RaycastAll(ray);
            Vector3 targetPosition = default;
            // Floor 레이어를 찾기
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Floor>() != null)
                {
                    // Floor 레이어를 찾은 경우 처리
                    targetPosition = hit.point;
                    targetPosition.y = MainSystem.Instance.PlayerManager.MainPlayer.transform.position.y; // 높이 고정
                    break;
                }
            }
            return targetPosition;
        }

        /// <summary>
        /// 진행 방향에 벽이 있는 지 체크 [ 벽이 있다면 그 앞까지만 데쉬 (못 넘음) ]
        /// </summary>
        public void CheckWallByDirection(Quaternion quaternionByTowardDirection, float Distance = 10f)
        {
            BasePlayer MainPlayer = MainSystem.Instance.PlayerManager.MainPlayer;
            IsHitWall = false;

            // 장벽이 있는 지 체크 & 충돌 지점 확인
            Ray ray = new Ray(MainPlayer.transform.position, (quaternionByTowardDirection * Vector3.forward).normalized); // 마우스 방향으로 Ray를 쏨
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, Distance); // (10미터 기준)
            Array.Sort(raycastHits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance)); // 거리 순으로 정렬 (hit된 Collider들을 거리 순으로 정렬)

            // hit 된 여러 Collider 중에 맨 처음에 닿은 Wall 판별하기
            foreach (RaycastHit hit in raycastHits)
            {
                Wall wallComponent = hit.collider.GetComponent<Wall>();
                if (wallComponent != null)
                {
                    IsHitWall = true;
                    HitWall = hit;

                    // 목표 위치와 현재 위치 간의 방향 벡터 계산
                    Vector3 direction = HitWall.point - MainPlayer.transform.position;

                    // 목표 지점까지의 거리 계산
                    float targetPositionDistance = 0.3f; // 플레이어 반지름만큼 뺀 거리까지 가기

                    // 목표 지점보다 stopDistance 만큼 덜 간 위치 계산
                    TargetPositionTowardWall = HitWall.point - direction.normalized * targetPositionDistance;
                    break;
                }
            }

            Debug.DrawRay(MainPlayer.transform.position, (quaternionByTowardDirection * Vector3.forward).normalized * 10.0f, Color.red, 10.0f);
        }

        public Vector3 GetTargetPositionWithWallCheck(Quaternion dashRotation, float distance)
        {
            CheckWallByDirection(dashRotation, distance);
            if (IsHitWall)
            {
                IsHitWall = false;
                return TargetPositionTowardWall;
            }
            else
            {
                BasePlayer mainPlayer = MainSystem.Instance.PlayerManager.MainPlayer;

                return mainPlayer.transform.position + (mainPlayer.transform.forward * distance);
            }
        }
    }
}