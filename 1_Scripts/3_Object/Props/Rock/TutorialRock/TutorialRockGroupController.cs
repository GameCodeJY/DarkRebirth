/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 14.
 *  Information :   Ʃ�丮�� ������ ��Ʈ�ѷ�
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class TutorialRockGroupController : MonoBehaviour // Data Field
    {
        /// <summary> ���� ���� �������� �ð� </summary>
        private const float TIME_NEXT_ROCK_DROP = 5.0f;

        /// <summary> Ʃ�丮�� ������ ���� ������Ʈ ����Ʈ </summary>
        [SerializeField] private List<GameObject> ListGameObjectRocks = default;
    }

    /// <summary>
    /// Rock Control
    /// </summary>
    public partial class TutorialRockGroupController : MonoBehaviour // Rock Control
    {
        /// <summary>
        /// ���� ����߸���
        /// </summary>
        public void StartDropRocks()
        {
            StartCoroutine(CoroutineDropRocks()); // �ð� ���ݿ� �°� ������ ����߸���
        }

        /// <summary>
        /// �ð� ���ݿ� �°� ������ ����߸���
        /// </summary>
        private IEnumerator CoroutineDropRocks()
        {
            // ����Ʈ�� �ִ� ������ ���ʴ�� Ȱ��ȭ�ؼ� ����߸���
            for (int numRock = 0; numRock < ListGameObjectRocks.Count; numRock++)
            {
                ListGameObjectRocks[numRock].SetActive(true); // ���� Ȱ��ȭ�ؼ� ��� [����� Rigidbody]

                yield return new WaitForSeconds(TIME_NEXT_ROCK_DROP); // ���� ���� Ȱ��ȭ���� ��ٸ���
            }
        }
    }
}