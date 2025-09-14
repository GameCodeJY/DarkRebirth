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
        /// <summary> �ش�Ǵ� Field Controller </summary>
        [SerializeField] private FieldController _newFieldController = default;
        /// <summary> ��Ż ��ȣ�ۿ� �̺�Ʈ </summary>
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
                MainSystem.Instance.SceneManager.UIManager.StageUIController.ActiveFKeyMessageUI("��ȣ�ۿ�");
            }
        }

        private void Update()
        {
            if (_mainPlayer == null)
                return;

            if (Input.GetKey(KeyCode.F) == false)
                return;

            _mainPlayer = null;

            // ��ȣ�ۿ� �޼��� UI OFF
            MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();

            // ���� �ʵ�� �̵�
            MainSystem.Instance.ProcessManager.StartCoroutine(GoNextField());

            // ��Ż�� ��ȣ�ۿ�� �̺�Ʈ ����
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
            // ���� Ǯ �ʱ�ȭ
            MainSystem.Instance.PoolManager.ResetMonsterPool();

            yield return new WaitForSeconds(1.5f);

            // ���� �ʵ�� ����
            MainSystem.Instance.ProcessManager.ChangeToNextField();
        }
    }
}