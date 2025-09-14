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
        /// <summary> ���� Life Time </summary>
        private const float LIFE_TIME_ROCK = 300.0f;
        /// <summary> �÷��̾ ���� ������ ���ư��� ���ǵ� </summary>
        private const float MOVE_SPEED_ROCK_MOVE_TO_PLAYER = 10f;
        /// <summary> ���� ȸ�� �ӵ� </summary>
        private const float ROTATION_SPEED_ROCK = 360.0f;
        #endregion

        /// <summary> ������ Rigidbody </summary>
        [SerializeField] private Rigidbody RigidbodyRock = default;
        /// <summary> Rock Model Transform </summary>
        [SerializeField] private Transform TransformRockModel = default;
        /// <summary> ������ ����ִ� �ð� </summary>
        private float LifeTimeRock = 0.0f;
        /// <summary> ���� ���� �÷��� </summary>
        private bool IsStopRock = false;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // Main
    {
        private void Update()
        {
            CheckRockLife(); // ������ Life Time üũ

            MoveOrStopRock(); // �÷��̾ ��� ���� ���°� �ƴϸ� �̵�
        }
    }

    /// <summary>
    /// ���� LifeCycle
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // ���� LifeCycle
    {
        /// <summary>
        /// ������ Life üũ
        /// </summary>
        private void CheckRockLife()
        {
            LifeTimeRock += Time.deltaTime;
            if (LifeTimeRock > LIFE_TIME_ROCK) // ������ Life Time�� ���� �� ��Ȱ��ȭ
            {
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ���� ������
    /// </summary>
    public partial class TutorialRock : MonoBehaviour // ���� ������
    {
        /// <summary>
        /// ���� ������ ����
        /// </summary>
        public void SetRockMove()
        {
            IsStopRock = false;
        }

        /// <summary>
        /// ���� ���� ����
        /// </summary>
        public void SetRockStop()
        {
            IsStopRock = true;
        }

        /// <summary>
        /// ���� ������ �� ���� ��
        /// </summary>
        private void MoveOrStopRock()
        {
            // ���� �׻� ȸ��
            TransformRockModel.Rotate(Vector3.left, ROTATION_SPEED_ROCK * Time.deltaTime, Space.Self);

            // �÷��̾ ��� ���� ���°� �ƴϸ� �̵� & ȸ��
            if (IsStopRock.Equals(false))
            {
                // ������ �̵�
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