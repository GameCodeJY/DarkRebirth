/*
 *  Coder       :   JY
 *  Last Update :   2025. 09. 02.
 *  Information :   인스턴스 매니저 - 게임 내 모든 인스턴스 관리
 */

namespace MainSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// 데이터 필드
    /// </summary>
    public class InstanceManager : BaseManager // Data Field
    {
        #region Data Field

        #region Home Scene
        /// <summary> Home Controller </summary>
        public HomeController HomeController { get; private set; } = default;
        /// <summary> Base Monster </summary>
        public BaseMonster BaseMonster = default;
        #endregion

        #region Stage Scene
        public ColosseumController ColosseumController { get; private set; } = default;
        public HomerunAxSocketGroupController HomerunAxSocketGroupController = default;
        #endregion

        private GameObject DamageText = default;
        #endregion

        #region Public API
        public void SpawnDamageText(Vector3 centerPosition, float damage)
        {
            // 1. 카메라 방향으로 랜덤한 위치 계산
            Vector2 randomInCircle = UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0f, 0.3f);

            // 2. 카메라 기준 방향 벡터 (forward 방향으로 x, y 축)
            Vector3 cameraRight = Camera.main.transform.right;
            Vector3 cameraUp = Camera.main.transform.up;

            // 3. 오프셋 계산 (카메라 기준 위치 + 랜덤한 오프셋)
            Vector3 offset = (cameraRight * randomInCircle.x) + (cameraUp * randomInCircle.y);
            offset += Vector3.up * 1.5f; // 위로 살짝 올려서 더 보기 좋게

            Vector3 spawnPosition = centerPosition + offset;

            // 4. 데미지 텍스트 생성
            DamageText damageText = Instantiate(DamageText, spawnPosition, Quaternion.identity).GetComponent<DamageText>();
            damageText.SetDamageText(damage);
        }
        #endregion

        #region Initialize
        protected override void Allocate()
        {
            base.Allocate();

            DamageText = Resources.Load<GameObject>("DamageText/DamageText");
        }

        /// <summary>
        /// Sign Up HomeController
        /// </summary>
        public void SignUpHomeController(HomeController homeController)
        {
            HomeController = homeController;
        }

        /// <summary>
        /// Sign Up Colosseum Controller
        /// </summary>
        public void SignUpColosseumController(ColosseumController colosseumController)
        {
            ColosseumController = colosseumController;
        }

        /// <summary>
        /// Sign Up Homerun Ax Socket Group Controller
        /// </summary>
        public void SignUpHomerunAxSocketGroupController(HomerunAxSocketGroupController homerunAxSocketGroupController)
        {
            HomerunAxSocketGroupController = homerunAxSocketGroupController;
        }
        #endregion
    }
}