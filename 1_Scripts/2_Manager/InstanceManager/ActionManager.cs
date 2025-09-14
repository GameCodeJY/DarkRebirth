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
        /// <summary> ���� ����� ��ġ </summary>
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
        /// <summary> Spear Jump �̵� �ð� </summary>
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
        /// ���콺 �������� ȸ���� Quaternion�� ����մϴ�.
        /// Floor ������Ʈ�� ���� ������Ʈ�� Raycast�� ������, �ش� ������ �������� ȸ���մϴ�.
        /// </summary>
        /// <returns>���� Quaternion (�浹�� Floor�� ������ ���� ȸ���� ��ȯ)</returns>
        public Quaternion RotateToMousePoint()
        {
            // ī�޶� ��ȿ�� �˻�
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Camera.main�� �����ϴ�!");
                return Quaternion.identity;
            }

            // ���콺 ��ġ�� �������� Ray ����
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // ����׿� ���� �׸��� (Scene �信�� 1�� ���� ǥ��)
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1.0f);

            // �ִ� �Ÿ� ����
            float maxDistance = 100f;
            // Ray�� �浹�� ��� ������Ʈ ������ ������
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance);

            // �Ÿ� ������ ���� (���� ����� �浹���� ó��)
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            // �÷��̾��� ���� Transform ���� (MainPlayer�� Transform)
            Transform playerTransform = MainSystem.Instance.PlayerManager.MainPlayer.transform;
            // �⺻ ȸ������ ���� ȸ���� (�浹 Floor�� ���� ���)
            Quaternion targetRotation = playerTransform.rotation;

            // �浹�� ������Ʈ�� �� Floor ������Ʈ�� ���� ������Ʈ�� ã��
            foreach (RaycastHit hit in hits)
            {
                // ���� �ش� ������Ʈ�� Floor ������Ʈ�� ���ٸ� �ǳʶݴϴ�.
                if (hit.collider.gameObject.GetComponent<Floor>() == null)
                    continue;

                //Debug.Log(hit.collider.name);

                // Floor ������Ʈ�� ã�� ���, �浹 ������ Ÿ�� ��ġ�� ���
                Vector3 targetPosition = hit.point;
                // �÷��̾��� ���̸� ���� (���� ȸ���� ����)
                targetPosition.y = playerTransform.position.y;

                // �÷��̾��� ��ġ���� Ÿ�� ��ġ���� ���� ���� ���
                Vector3 direction = targetPosition - playerTransform.position;
                // ���� ���Ͱ� ����� ũ�� ȸ���� ���
                if (direction.sqrMagnitude > 0.001f)
                {
                    targetRotation = Quaternion.LookRotation(direction);
                }
                // ù ��°�� Ȯ�ε� Floor �浹�� ���
                break;
            }

            return targetRotation;
        }

        public Vector3 GetMousePointPosition()
        {
            // ���콺 ��ġ�� �������� Ray ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Ray�� �浹�� ��� ������Ʈ�� ������
            RaycastHit[] hits = Physics.RaycastAll(ray);
            Vector3 targetPosition = default;
            // Floor ���̾ ã��
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Floor>() != null)
                {
                    // Floor ���̾ ã�� ��� ó��
                    targetPosition = hit.point;
                    targetPosition.y = MainSystem.Instance.PlayerManager.MainPlayer.transform.position.y; // ���� ����
                    break;
                }
            }
            return targetPosition;
        }

        /// <summary>
        /// ���� ���⿡ ���� �ִ� �� üũ [ ���� �ִٸ� �� �ձ����� ���� (�� ����) ]
        /// </summary>
        public void CheckWallByDirection(Quaternion quaternionByTowardDirection, float Distance = 10f)
        {
            BasePlayer MainPlayer = MainSystem.Instance.PlayerManager.MainPlayer;
            IsHitWall = false;

            // �庮�� �ִ� �� üũ & �浹 ���� Ȯ��
            Ray ray = new Ray(MainPlayer.transform.position, (quaternionByTowardDirection * Vector3.forward).normalized); // ���콺 �������� Ray�� ��
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, Distance); // (10���� ����)
            Array.Sort(raycastHits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance)); // �Ÿ� ������ ���� (hit�� Collider���� �Ÿ� ������ ����)

            // hit �� ���� Collider �߿� �� ó���� ���� Wall �Ǻ��ϱ�
            foreach (RaycastHit hit in raycastHits)
            {
                Wall wallComponent = hit.collider.GetComponent<Wall>();
                if (wallComponent != null)
                {
                    IsHitWall = true;
                    HitWall = hit;

                    // ��ǥ ��ġ�� ���� ��ġ ���� ���� ���� ���
                    Vector3 direction = HitWall.point - MainPlayer.transform.position;

                    // ��ǥ ���������� �Ÿ� ���
                    float targetPositionDistance = 0.3f; // �÷��̾� ��������ŭ �� �Ÿ����� ����

                    // ��ǥ �������� stopDistance ��ŭ �� �� ��ġ ���
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