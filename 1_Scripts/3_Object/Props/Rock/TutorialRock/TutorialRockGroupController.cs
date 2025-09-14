/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 14.
 *  Information :   튜토리얼 바위들 컨트롤러
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
        /// <summary> 다음 바위 떨어지는 시간 </summary>
        private const float TIME_NEXT_ROCK_DROP = 5.0f;

        /// <summary> 튜토리얼 바위들 게임 오브젝트 리스트 </summary>
        [SerializeField] private List<GameObject> ListGameObjectRocks = default;
    }

    /// <summary>
    /// Rock Control
    /// </summary>
    public partial class TutorialRockGroupController : MonoBehaviour // Rock Control
    {
        /// <summary>
        /// 바위 떨어뜨리기
        /// </summary>
        public void StartDropRocks()
        {
            StartCoroutine(CoroutineDropRocks()); // 시간 간격에 맞게 바위들 떨어뜨리기
        }

        /// <summary>
        /// 시간 간격에 맞게 바위들 떨어뜨리기
        /// </summary>
        private IEnumerator CoroutineDropRocks()
        {
            // 리스트에 있는 바위들 차례대로 활성화해서 떨어뜨리기
            for (int numRock = 0; numRock < ListGameObjectRocks.Count; numRock++)
            {
                ListGameObjectRocks[numRock].SetActive(true); // 바위 활성화해서 드랍 [드랍은 Rigidbody]

                yield return new WaitForSeconds(TIME_NEXT_ROCK_DROP); // 다음 바위 활성화까지 기다리기
            }
        }
    }
}