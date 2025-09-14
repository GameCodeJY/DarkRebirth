/*
 *  Coder       :   JY
 *  Last Update :   2025. 01. 11.
 *  Information :   Bricks Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class BricksController : MonoBehaviour // Data Field
    {
        #region Serialize Field
        /// <summary> Bricks Rigidbodies List </summary>
        [SerializeField] private List<Rigidbody> listRigidbodiesBricks = default;
        /// <summary> Bricks Group GameObject </summary>
        [SerializeField] private GameObject gameObjectBricksGroup = default;
        /// <summary> ���� Bricks Group GameObject </summary>
        [SerializeField] private GameObject nextGameObjectBricksGroup = default;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class BricksController : MonoBehaviour // Main
    {
        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                if (nextGameObjectBricksGroup != null)
                {
                    nextGameObjectBricksGroup.SetActive(true);
                }
                StartCoroutine(DropBricks());
            }
        }
    }

    /// <summary>
    /// Drop
    /// </summary>
    public partial class BricksController : MonoBehaviour // Drop
    {
        /// <summary>
        /// ������ ����߸���
        /// </summary>
        private IEnumerator DropBricks()
        {
            gameObjectBricksGroup.SetActive(false);

            // ���� �迭 ����
            int[] numbers = Enumerable.Range(0, listRigidbodiesBricks.Count).ToArray();

            ShuffleArray(numbers);

            for (int i = 0; i < numbers.Length; i++)
            {
                listRigidbodiesBricks[numbers[i]].isKinematic = false;
                ApplyRandomTorqueToBrick(listRigidbodiesBricks[numbers[i]]);
                yield return new WaitForSeconds(Random.Range(0.0f, 0.05f)); // 0.3f
            }

            yield return new WaitForSeconds(1.0f);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Array Shuffle (Fisher-Yates �˰���)
        /// </summary>
        private void ShuffleArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int randomIndex = Random.Range(0, array.Length);
                // �� ���� ��ȯ
                int temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        /// <summary>
        /// �����̿� ���� ��ũ�� ����
        /// </summary>
        private void ApplyRandomTorqueToBrick(Rigidbody rigidbodyBrick)
        {
            float torqueStrength = Random.Range(3.0f, 4.0f); // ��ũ ���� 1.0f, 2.0f // 5.f 6.f

            // ������ �������� ��ũ ����
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            // ���� ��ũ ���� ����
            Vector3 randomTorque = new Vector3(randomX, randomY, randomZ) * torqueStrength;

            // Rigidbody�� ������ ��ũ ����
            rigidbodyBrick.AddForce(Vector3.down * Random.Range(1.0f, 2.0f), ForceMode.Impulse); // 5.0f, 6.0f
            rigidbodyBrick.AddTorque(randomTorque, ForceMode.Impulse);
        }
    }
}