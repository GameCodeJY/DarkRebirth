/*
 *  Coder       :   JY
 *  Last Update :   2025. 01. 16.
 *  Information :   Box Item Group Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class BoxItemGroupController : MonoBehaviour // Data Field
    {
        #region Serialize Field
        /// <summary> Box Item Transform List </summary>
        [SerializeField] private List<Transform> listBoxItemTransform = default;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class BoxItemGroupController : MonoBehaviour // Main
    {
        private void Start()
        {
            // �������� Socket�� �̱�
            int[] randomNumbers = GetRandomNumbers(0, listBoxItemTransform.Count-1, Random.Range(8, 13));

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                // Wood_Box ������ ����
                Instantiate(Resources.Load("Item/Wood_Box"), listBoxItemTransform[randomNumbers[i]]);
            }
        }
    }

    /// <summary>
    /// Random
    /// </summary>
    public partial class BoxItemGroupController : MonoBehaviour // Random
    {
        /// <summary>
        /// �־��� �������� �ߺ����� �ʴ� ���� ���ڸ� �̾Ƴ��ϴ�.
        /// </summary>
        /// <param name="min">������ �ּҰ�</param>
        /// <param name="max">������ �ִ밪</param>
        /// <param name="count">���� ������ ����</param>
        /// <returns>���� ���� �迭</returns>
        int[] GetRandomNumbers(int min, int max, int count)
        {
            if (count > (max - min + 1))
            {
                throw new System.ArgumentException("���� ������ ������ ������ ���ں��� ���� �� �����ϴ�.");
            }

            // ��ü ���ڸ� �迭�� ����
            int range = max - min + 1;
            int[] numbers = new int[range];
            for (int i = 0; i < range; i++)
            {
                numbers[i] = min + i;
            }

            // Fisher-Yates Shuffle �˰����� ����Ͽ� ����ȭ
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(i, range); // i���� range-1���� ���� ����
                                                          // ����
                int temp = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temp;
            }

            // ó�� count���� ���ڸ� ����� ��ȯ
            int[] result = new int[count];
            System.Array.Copy(numbers, result, count);

            return result;
        }
    }
}