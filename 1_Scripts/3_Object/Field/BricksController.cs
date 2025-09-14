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
        /// <summary> 다음 Bricks Group GameObject </summary>
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
        /// 돌맹이 떨어뜨리기
        /// </summary>
        private IEnumerator DropBricks()
        {
            gameObjectBricksGroup.SetActive(false);

            // 숫자 배열 생성
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
        /// Array Shuffle (Fisher-Yates 알고리즘)
        /// </summary>
        private void ShuffleArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int randomIndex = Random.Range(0, array.Length);
                // 두 숫자 교환
                int temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        /// <summary>
        /// 돌맹이에 랜덤 토크힘 적용
        /// </summary>
        private void ApplyRandomTorqueToBrick(Rigidbody rigidbodyBrick)
        {
            float torqueStrength = Random.Range(3.0f, 4.0f); // 토크 강도 1.0f, 2.0f // 5.f 6.f

            // 랜덤한 방향으로 토크 생성
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            // 랜덤 토크 방향 백터
            Vector3 randomTorque = new Vector3(randomX, randomY, randomZ) * torqueStrength;

            // Rigidbody에 랜덤한 토크 적용
            rigidbodyBrick.AddForce(Vector3.down * Random.Range(1.0f, 2.0f), ForceMode.Impulse); // 5.0f, 6.0f
            rigidbodyBrick.AddTorque(randomTorque, ForceMode.Impulse);
        }
    }
}