﻿//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DigitalRubyShared
{
    /// <summary>
    /// Allows two finger pan, scale and rotate on a game object
    /// </summary>
    [AddComponentMenu("Fingers Gestures/Pan, Rotate and Scale")]
    public class FingersPanRotateScaleScript : MonoBehaviour
    {
        [Tooltip("The camera to use to convert screen coordinates to world coordinates. Defaults to Camera.main.")]
        public Camera Camera;

        [Tooltip("Whether to bring the object to the front when a gesture executes on it")]
        public bool BringToFront = true;

        [Tooltip("Minimum touch count to start panning. Rotating and scaling always requires two fingers. This should be 1 or 2.")]
        [Range(1, 2)]
        public int PanMinimumTouchCount = 1;

        [Tooltip("Whether the gestures in this script can execute simultaneously with all other gestures.")]
        public bool AllowExecutionWithAllGestures;

        [Tooltip("The mode to execute in, can be require over game object, execute anywhere but only affect this game object, or raycast to find the game object to affect.")]
        public GestureRecognizerComponentScriptBase.GestureObjectMode Mode = GestureRecognizerComponentScriptBase.GestureObjectMode.RequireIntersectWithGameObject;

        public PanGestureRecognizer PanGesture { get; private set; }
        public ScaleGestureRecognizer ScaleGesture { get; private set; }
        public RotateGestureRecognizer RotateGesture { get; private set; }

        private Rigidbody2D rigidBody2D;
        private Rigidbody rigidBody;
        private SpriteRenderer spriteRenderer;
        private CanvasRenderer canvasRenderer;
        private Transform _transform;
        private int startSortOrder;
        private Vector3 panStart;

        private static readonly List<RaycastResult> captureRaycastResults = new List<RaycastResult>();

        public static GameObject StartOrResetGesture(GestureRecognizer r, bool bringToFront, Camera camera, GameObject obj, SpriteRenderer spriteRenderer, GestureRecognizerComponentScriptBase.GestureObjectMode mode)
        {
            GameObject result = null;
            if (r.State == GestureRecognizerState.Began)
            {
                if ((result = GestureIntersectsObject(r, camera, obj, mode)) != null)
                {
                    if (bringToFront && spriteRenderer != null)
                    {
                        spriteRenderer.sortingOrder = 1000;
                    }
                }
                else
                {
                    r.Reset();
                }
            }
            return result;
        }

        private static int RaycastResultCompare(RaycastResult r1, RaycastResult r2)
        {
            SpriteRenderer rend1 = r1.gameObject.GetComponent<SpriteRenderer>();
            if (rend1 != null)
            {
                SpriteRenderer rend2 = r2.gameObject.GetComponent<SpriteRenderer>();
                if (rend2 != null)
                {
                    int comp = rend2.sortingLayerID.CompareTo(rend1.sortingLayerID);
                    if (comp == 0)
                    {
                        comp = rend2.sortingOrder.CompareTo(rend1.sortingOrder);
                    }
                    return comp;
                }
            }
            return 0;
        }

        private static GameObject GestureIntersectsObject(GestureRecognizer r, Camera camera, GameObject obj, GestureRecognizerComponentScriptBase.GestureObjectMode mode)
        {
            captureRaycastResults.Clear();
            PointerEventData p = new PointerEventData(EventSystem.current);
            p.Reset();
            p.position = new Vector2(r.FocusX, r.FocusY);
            p.clickCount = 1;
            EventSystem.current.RaycastAll(p, captureRaycastResults);
            captureRaycastResults.Sort(RaycastResultCompare);

            foreach (RaycastResult result in captureRaycastResults)
            {
                if (result.gameObject == obj)
                {
                    return result.gameObject;
                }
                else if (result.gameObject.GetComponent<Collider>() != null ||
                    result.gameObject.GetComponent<Collider2D>() != null ||
                    result.gameObject.GetComponent<FingersPanRotateScaleScript>() != null)
                {
                    if (mode == GestureRecognizerComponentScriptBase.GestureObjectMode.AllowOnAnyGameObjectViaRaycast)
                    {
                        return result.gameObject;
                    }

                    // blocked by a collider or another gesture, bail
                    break;
                }
            }
            return null;
        }

        private void PanGestureUpdated(GestureRecognizer r)
        {
            GameObject obj = StartOrResetGesture(r, BringToFront, Camera, gameObject, spriteRenderer, Mode);
            if (r.State == GestureRecognizerState.Began)
            {
                SetStartState(obj);
                if (rigidBody != null)
                {
                    panStart = rigidBody.position;
                }
                else if (rigidBody2D != null)
                {
                    panStart = rigidBody2D.position;
                }
                else
                {
                    panStart = gameObject.transform.position;
                }
            }
            else if (r.State == GestureRecognizerState.Executing)
            {
                Vector3 screenMovement = new Vector2(PanGesture.DistanceX, PanGesture.DistanceY);
                Plane plane = new Plane(-Camera.transform.forward, _transform.position);
                float distance = plane.GetDistanceToPoint(Camera.transform.position);
                screenMovement.z = distance;
                Vector3 screenToWorld = Camera.ScreenToWorldPoint(screenMovement);
                Vector3 worldMovement = screenToWorld - Camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, screenMovement.z));
                //Debug.LogFormat("Screen movement: {0}, World movement: {1}", screenMovement, worldMovement);
                if (rigidBody != null)
                {
                    rigidBody.MovePosition(panStart + worldMovement);
                }
                else if (rigidBody2D != null)
                {
                    worldMovement.z = 0.0f;
                    rigidBody2D.MovePosition(panStart + worldMovement);
                }
                else if (canvasRenderer != null)
                {
                    screenMovement.z = 0.0f;
                    _transform.position = panStart + screenMovement;
                }
                else
                {
                    _transform.position = panStart + worldMovement;
                }
            }
            else if (r.State == GestureRecognizerState.Ended)
            {
                if (spriteRenderer != null && BringToFront)
                {
                    spriteRenderer.sortingOrder = startSortOrder;
                }
            }
        }

        private void ScaleGestureUpdated(GestureRecognizer r)
        {
            StartOrResetGesture(r, BringToFront, Camera, gameObject, spriteRenderer, Mode);
            if (r.State == GestureRecognizerState.Executing)
            {
                _transform.localScale *= ScaleGesture.ScaleMultiplier;
            }
        }

        private void RotateGestureUpdated(GestureRecognizer r)
        {
            StartOrResetGesture(r, BringToFront, Camera, gameObject, spriteRenderer, Mode);
            if (r.State == GestureRecognizerState.Executing)
            {
                if (rigidBody != null)
                {
                    float angle = RotateGesture.RotationDegreesDelta;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Camera.transform.forward);
                    rigidBody.MoveRotation(rigidBody.rotation * rotation);
                }
                else if (rigidBody2D != null)
                {
                    rigidBody2D.MoveRotation(rigidBody2D.rotation + RotateGesture.RotationDegreesDelta);
                }
                else if (canvasRenderer != null)
                {
                    _transform.Rotate(Vector3.forward, RotateGesture.RotationDegreesDelta, Space.Self);
                }
                else
                {
                    _transform.Rotate(Camera.transform.forward, RotateGesture.RotationDegreesDelta, Space.Self);
                }
            }
        }

        private void SetStartState(GameObject obj)
        {
            rigidBody2D = obj.GetComponent<Rigidbody2D>();
            rigidBody = obj.GetComponent<Rigidbody>();
            spriteRenderer = obj.GetComponent<SpriteRenderer>();
            canvasRenderer = obj.GetComponent<CanvasRenderer>();
            if (spriteRenderer != null)
            {
                startSortOrder = spriteRenderer.sortingOrder;
            }
            _transform = (rigidBody == null ? (rigidBody2D == null ? obj.transform : rigidBody2D.transform) : rigidBody.transform);
        }

        private void Start()
        {
            this.Camera = (this.Camera == null ? Camera.main : this.Camera);
            PanGesture = new PanGestureRecognizer();
            PanGesture.MinimumNumberOfTouchesToTrack = PanMinimumTouchCount;
            PanGesture.MaximumNumberOfTouchesToTrack = 2;
            PanGesture.StateUpdated += PanGestureUpdated;
            ScaleGesture = new ScaleGestureRecognizer();
            ScaleGesture.StateUpdated += ScaleGestureUpdated;
            RotateGesture = new RotateGestureRecognizer();
            RotateGesture.StateUpdated += RotateGestureUpdated;
            SetStartState(gameObject);
            if (AllowExecutionWithAllGestures)
            {
                PanGesture.AllowSimultaneousExecutionWithAllGestures();
                PanGesture.AllowSimultaneousExecutionWithAllGestures();
                ScaleGesture.AllowSimultaneousExecutionWithAllGestures();
            }
            else
            {
                PanGesture.AllowSimultaneousExecution(ScaleGesture);
                PanGesture.AllowSimultaneousExecution(RotateGesture);
                ScaleGesture.AllowSimultaneousExecution(RotateGesture);
            }
            if (Mode == GestureRecognizerComponentScriptBase.GestureObjectMode.RequireIntersectWithGameObject)
            {
                RotateGesture.PlatformSpecificView = gameObject;
                PanGesture.PlatformSpecificView = gameObject;
                ScaleGesture.PlatformSpecificView = gameObject;
            }
            FingersScript.Instance.AddGesture(PanGesture);
            FingersScript.Instance.AddGesture(ScaleGesture);
            FingersScript.Instance.AddGesture(RotateGesture);
        }
    }
}
