/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 03.
 *  Information :   Field Portal Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class FieldPortalTrigger : MonoBehaviour // Data Field
    {
        /// <summary> Main Player </summary>
        private BasePlayer _mainPlayer = default;
        /// <summary> 해당되는 Field Controller </summary>
        [SerializeField] private FieldController _newFieldController = default;
        /// <summary> 포탈 상호작용 이벤트 </summary>
        [SerializeField] private UnityEvent _eventFieldPortalInteraction = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class FieldPortalTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = mainPlayer;
                MainSystem.Instance.SceneManager.UIManager.StageUIController.ActiveFKeyMessageUI("상호작용");
            }
        }

        private void Update()
        {
            if (_mainPlayer == null)
                return;

            if (Input.GetKey(KeyCode.F) == false)
                return;

            _mainPlayer = null;

            // 상호작용 메세지 UI OFF
            MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();

            // 다음 필드로 이동
            MainSystem.Instance.ProcessManager.StartCoroutine(GoNextField());

            // 포탈과 상호작용시 이벤트 실행
            _eventFieldPortalInteraction?.Invoke();

            gameObject.SetActive(false);
        }

        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = null;
                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();
            }
        }

        private IEnumerator GoNextField()
        {
            // 몬스터 풀 초기화
            MainSystem.Instance.PoolManager.ResetMonsterPool();

            yield return new WaitForSeconds(1.5f);

            // 다음 필드로 변경
            MainSystem.Instance.ProcessManager.ChangeToNextField();
        }
    }
}