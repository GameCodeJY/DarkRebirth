/*
 *  Coder       :   JY
 *  Last Update :   2025. 02. 05.
 *  Information :   Colosseum Controller
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
    public partial class ColosseumController : MonoBehaviour // Data Field
    {
        /// <summary> 원래 바닥 GameObject </summary>
        public GameObject GameObjectOriginFloor = default;
        /// <summary> 분할된 바닥 GameObject </summary>
        public GameObject GameObjectPartialFloor = default;
        /// <summary> Floor Collider GameObject </summary>
        public GameObject GameObjectFloorCollider = default;
        /// <summary> Bricks Rigidbodies List </summary>
        [SerializeField] private List<Rigidbody> listRigidbodiesBricks = default;
        /// <summary> Minotaur Map Center Trigger Group GameObject </summary>
        public GameObject GameObjectMinotaurMapCenterTriggerGroup = default;
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class ColosseumController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            MainSystem.Instance.InstanceManager.SignUpColosseumController(this);
        }

        private void Initialize()
        {
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class ColosseumController : MonoBehaviour // Main
    {
        private void Awake()
        {
            Allocate();
        }

        private void Start()
        {
            Initialize();
        }
    }

    /// <summary>
    /// Drop
    /// </summary>
    public partial class ColosseumController : MonoBehaviour // Drop
    {
        /// <summary>
        /// Drop Bricks
        /// </summary>
        public void DropBricks()
        {
            GameObjectOriginFloor.SetActive(false);
            GameObjectPartialFloor.SetActive(true);
            GameObjectFloorCollider.SetActive(false);

            // 숫자 배열 생성
            int[] numbers = Enumerable.Range(0, listRigidbodiesBricks.Count).ToArray();

            ShuffleArray(numbers);

            for (int i = 0; i < numbers.Length; i++)
            {
                listRigidbodiesBricks[numbers[i]].isKinematic = false;
                ApplyRandomTorqueToBrick(listRigidbodiesBricks[numbers[i]]);
            }
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