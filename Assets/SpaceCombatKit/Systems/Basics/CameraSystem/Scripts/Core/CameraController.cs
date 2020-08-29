﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSX.CameraSystem
{
    /// <summary>
    /// Base class for a script that controls the camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {

        [Header("General")]

        // A reference to the camera this controller is controlling
        protected CameraEntity cameraEntity;
        public virtual void SetCamera(CameraEntity camera)
        {
            cameraEntity = camera;
            cameraEntity.onCameraViewTargetChanged.AddListener(OnCameraViewTargetChanged);
        }

        [Header("Starting Values")]

        [Tooltip("The camera view that is selected upon switching to a new camera target.")]
        [SerializeField]
        protected CameraView startingView;

        [Tooltip("Whether to default to the first available view, if the startingView value is not set.")]
        [SerializeField]
        protected bool defaultToFirstAvailableView = true;

        // Whether this camera controller is currently activated
        protected bool controllerEnabled = false;
        public bool ControllerEnabled { get { return controllerEnabled; } }

        // Whether this camera controller is ready to be activated
        protected bool initialized = false;
        public bool Initialized { get { return initialized; } }

        
        /// <summary>
        /// Called to start/stop this camera controller.
        /// </summary>
        public virtual void SetControllerEnabled(bool enable)
        {
            if (enable)
            {
                // If this camera controller has been initialized to the current camera target, activate it.
                if (initialized) controllerEnabled = true;
            }
            else
            {
                controllerEnabled = false;
            }
        }

        /// <summary>
        /// Called when the camera target that the camera is currently following changes.
        /// </summary>
        /// <param name="target">The new camera target.</param>
        /// <param name="startController">Whether to start the controller immediately.</param>
        public virtual void OnCameraTargetChanged(CameraTarget target, bool startController)
        {

            SetControllerEnabled(false);

            initialized = false;

            if (target == null) return;

            if (Initialize(target))
            {
                initialized = true;

                if (startingView != null)
                {
                    cameraEntity.SetView(startingView);
                }
                else
                {
                    if (defaultToFirstAvailableView && target.CameraViewTargets.Count > 0)
                    {
                        cameraEntity.SetView(target.CameraViewTargets[0].CameraView);
                    }
                    else
                    {
                        cameraEntity.SetView(null);
                    }
                }

                if (startController) SetControllerEnabled(true);
            }
        }

        /// <summary>
        /// Initialize this camera controller with a camera target.
        /// </summary>
        /// <param name="target">The camera target.</param>
        /// <returns>Whether initialization was successful.</returns>
        protected virtual bool Initialize(CameraTarget target)
        {
            return true;
        }

        protected virtual void OnCameraViewTargetChanged(CameraViewTarget newTarget)
        {
            if (newTarget != null)
            {
                cameraEntity.transform.position = newTarget.transform.position;
            }
        }

        // Put controller code for physics-driven camera targets here
        protected virtual void CameraControllerFixedUpdate() { }
        protected virtual void FixedUpdate()
        {
            // If not activated or no camera view target selected, exit.
            if (!controllerEnabled || cameraEntity.CameraTarget == null) return;

            CameraControllerFixedUpdate();
        }

        // Put controller code for non-physics-driven camera targets here
        protected virtual void CameraControllerUpdate() { }
        protected virtual void Update()
        {
            // If not activated or no camera view target selected, exit.
            if (!controllerEnabled || cameraEntity.CameraTarget == null) return;

            CameraControllerUpdate();
        }

        // Put controller code here that needs to occur after all Update methods in the game are completed for the frame.
        protected virtual void CameraControllerLateUpdate() { }
        protected virtual void LateUpdate()
        {
            // If not activated or no camera view target selected, exit.
            if (!controllerEnabled || cameraEntity.CameraTarget == null) return;

            CameraControllerLateUpdate();
        }
    }
}