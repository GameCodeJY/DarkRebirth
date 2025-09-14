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
            // 랜덤으로 Socket들 뽑기
            int[] randomNumbers = GetRandomNumbers(0, listBoxItemTransform.Count-1, Random.Range(8, 13));

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                // Wood_Box 프리팹 생성
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
        /// 주어진 범위에서 중복되지 않는 랜덤 숫자를 뽑아냅니다.
        /// </summary>
        /// <param name="min">범위의 최소값</param>
        /// <param name="max">범위의 최대값</param>
        /// <param name="count">뽑을 숫자의 개수</param>
        /// <returns>랜덤 숫자 배열</returns>
        int[] GetRandomNumbers(int min, int max, int count)
        {
            if (count > (max - min + 1))
            {
                throw new System.ArgumentException("뽑을 숫자의 개수가 범위의 숫자보다 많을 수 없습니다.");
            }

            // 전체 숫자를 배열로 생성
            int range = max - min + 1;
            int[] numbers = new int[range];
            for (int i = 0; i < range; i++)
            {
                numbers[i] = min + i;
            }

            // Fisher-Yates Shuffle 알고리즘을 사용하여 랜덤화
            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(i, range); // i부터 range-1까지 랜덤 선택
                                                          // 스왑
                int temp = numbers[i];
                numbers[i] = numbers[randomIndex];
                numbers[randomIndex] = temp;
            }

            // 처음 count개의 숫자를 결과로 반환
            int[] result = new int[count];
            System.Array.Copy(numbers, result, count);

            return result;
        }
    }
}