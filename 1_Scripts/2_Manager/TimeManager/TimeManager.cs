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
        /// 게임 시간 흐름 셋팅 [정지 / 플레이]
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
        /// 슬로우 연출
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

            float seconds = decimalPart * 100;       // 소수부를 초로 해석
            float normalized = minutes + (seconds / 60f);
            return normalized;
        }
    }
}