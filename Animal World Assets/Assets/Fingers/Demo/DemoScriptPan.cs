﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    public class DemoScriptPan : MonoBehaviour
    {
        public FingersScript FingersScript;
        public UnityEngine.UI.Text log;
        public UnityEngine.UI.ScrollRect logView;

        private void Log(string text, params object[] args)
        {
            string logText = string.Format(text, args);
            log.text += logText + System.Environment.NewLine;
            Debug.Log(logText);
            logView.normalizedPosition = Vector2.zero;
        }

        private void Start()
        {
            PanGestureRecognizer pan = new PanGestureRecognizer();
            pan.StateUpdated += Pan_Updated;
            FingersScript.AddGesture(pan);

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.StateUpdated += Tap_StateUpdated;
            tap.ClearTrackedTouchesOnEndOrFail = true;
            tap.AllowSimultaneousExecution(pan);
            FingersScript.AddGesture(tap);
        }

        private void Tap_StateUpdated(GestureRecognizer gesture)
        {
            Log("Tap gesture, state: {0}, position: {1},{2}", gesture.State, gesture.FocusX, gesture.FocusY);
        }

        private void Pan_Updated(GestureRecognizer gesture)
        {
            //Log("Pan gesture, state: {0}, position: {1},{2}", gesture.State, gesture.FocusX, gesture.FocusY);
        }
    }
}