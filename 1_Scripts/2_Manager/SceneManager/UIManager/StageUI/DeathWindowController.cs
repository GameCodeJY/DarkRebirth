/*
 *  Coder       :   JY
 *  Last Update :   2025. 09. 02.
 *  Information :   Death Window Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class DeathWindowController : MonoBehaviour // Data Field
    {
        #region Const Value
        /// <summary> Player�� �״� �ð� </summary>
        private float TIME_PLAYER_DEATH = 5.5f;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class DeathWindowController : MonoBehaviour // Main
    {
        private void Start()
        {
            ResetGameData();
        }
    }

    /// <summary>
    /// Death
    /// </summary>
    public partial class DeathWindowController : MonoBehaviour // Death
    {
        private void ResetGameData()
        {
            // ���� Ǯ ����
            MainSystem.Instance.PoolManager.ResetMonsterPool();

            // �ʵ� �޴��� ������ ����
            MainSystem.Instance.FieldManager.ResetFieldManagerData();

            StartCoroutine(CheckDeathTime());
        }

        /// <summary>
        /// Check Death Time
        /// </summary>
        private IEnumerator CheckDeathTime()
        {
            yield return new WaitForSeconds(TIME_PLAYER_DEATH);

            // �÷��̾� ������ ����
            MainSystem.Instance.PlayerManager.MainPlayer.ResetPlayer();

            MainSystem.Instance.SceneManager.SceneLoadAsync("3_Home");
        }
    }
}