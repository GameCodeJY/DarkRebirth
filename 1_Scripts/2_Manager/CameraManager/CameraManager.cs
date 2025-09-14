/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 24.
 *  Information :   Camera Manager
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class CameraManager : BaseManager // Data Field
    {
        #region Member Value
        /// <summary> Main Camera </summary>
        public Camera MainCamera = default;
        /// <summary> UI Camera </summary>
        public Camera UICamera = default;
        /// <summary> UI Camera Controller </summary>
        private UICameraController UICameraController = default;
        /// <summary> Main Camera Controller </summary>
        public MainCameraController MainCameraController { get; private set; } = default;
        /// <summary> Camera Group Controller </summary>
        public CameraGroupController CameraGroupController { get; private set; } = default;
        /// <summary> 쿼터뷰인가 </summary>
        public bool IsQuaterView = true;
        #endregion
    }

    /// <summary>
    /// Sign Up
    /// </summary>
    public partial class CameraManager : BaseManager // Sign Up
    {
        /// <summary>
        /// UI Camera 등록
        /// </summary>
        public void SignUpUICameraController(UICameraController uICameraController)
        {
            UICameraController = uICameraController;
            UICamera = UICameraController.UICamera;
        }

        /// <summary>
        /// Home Main Camera Controller 등록
        /// </summary>
        public void SignUpMainCameraController(MainCameraController mainCameraController)
        {
            MainCameraController = mainCameraController;
        }

        /// <summary>
        /// Camera Group Controller 등록
        /// </summary>
        public void SignUpCameraGroupController(CameraGroupController cameraGroupController)
        {
            CameraGroupController = cameraGroupController;
        }
    }

    /// <summary>
    /// UI
    /// </summary>
    public partial class CameraManager : BaseManager // UI
    {
        /// <summary>
        /// Get UI Mouse Position
        /// </summary>
        public Vector2 GetUIMousePosition()
        {
            return UICamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public partial class CameraManager : BaseManager
    {
        public void StartCameraShaking(Vector3 direction, float shakingDuration = 0.2f, float shakingStartMagnitude = 0.2f)
        {
            MainCameraController.StartCameraShaking(direction.normalized, shakingDuration, shakingStartMagnitude);
        }
    }


    /// <summary>
    /// Set
    /// </summary>
    public partial class CameraManager : BaseManager // Set
    {
        public void SetCameraTarget(Transform transform)
        {
            MainCameraController.SetCameraTarget(transform);
        }
    }
}