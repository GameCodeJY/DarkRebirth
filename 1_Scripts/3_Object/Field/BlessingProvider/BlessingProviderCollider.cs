/*
 *  Coder       :   JY
 *  Last Update :   2025. 09. 02.
 *  Information :   Blessing Provider Collider
 */

namespace MainSystem
{
    using UnityEngine;
    using UnityEngine.Events;

    public class BlessingProviderCollider : MonoBehaviour
    {
        #region Data Field
        private BasePlayer _mainPlayer = default;
        /// <summary> 상호작용 이벤트 </summary>
        [SerializeField] private UnityEvent _eventInteraction = default;
        #endregion

        #region Main
        private void OnTriggerEnter(Collider collision)
        {
            BasePlayer mainPlayer = collision.gameObject.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = mainPlayer;

                MainSystem.Instance.SceneManager.UIManager.StageUIController.ActiveFKeyMessageUI("서약 맺기");
            }
        }

        private void Update()
        {
            if (_mainPlayer != null && Input.GetKey(KeyCode.F))
            {
                _mainPlayer = null;

                // 게임 일시정지
                Time.timeScale = 0f;

                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();
                MainSystem.Instance.SceneManager.UIManager.StageUIController.SkillScrollUIController.gameObject.SetActive(true);

                _eventInteraction?.Invoke();
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            BasePlayer mainPlayer = collision.gameObject.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = null;

                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();
            }
        }
        #endregion
    }
}