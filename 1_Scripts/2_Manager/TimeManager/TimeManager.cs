/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 09.
 *  Information :   Time Manager
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    /// <summary>
    /// Game Time
    /// </summary>
    public partial class TimeManager : BaseManager // Game Time
    {
        /// <summary>
        /// ���� �ð� �帧 ���� [���� / �÷���]
        /// </summary>
        public void SetGameTimeScale(bool isRun)
        {
            if (isRun.Equals(true))
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
        }

        /// <summary>
        /// ���ο� ����
        /// </summary>
        public void ExcuteSlowTime(float duration, float timeScale)
        {
            StartCoroutine(DoSlowByDuration(duration, timeScale));
        }

        private IEnumerator DoSlowByDuration(float duration, float timeScale)
        {
            Time.timeScale = timeScale;

            float time = 0;
            while(time < duration)
            {
                time += Time.unscaledDeltaTime;
                yield return null;
            }

            SetGameTimeScale(true);
        }

        public float ConvertMinuteSecondToFloat(float inputTime)
        {
            int minutes = (int)inputTime;
            float decimalPart = inputTime - minutes;

            float seconds = decimalPart * 100;       // �Ҽ��θ� �ʷ� �ؼ�
            float normalized = minutes + (seconds / 60f);
            return normalized;
        }
    }
}