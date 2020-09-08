using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hokuro.Functions
{
    // Class to create actions that trigger after certain amount of time
    public class TimerFunction
    {
        // Dummy class to get MonoBehaviour functions
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action onUpdate;

            private void Update()
            {
                if (onUpdate != null) onUpdate();
            }
        }

        private readonly GameObject gameObject_;
        private readonly Action action_;
        private float timer_;
        private readonly string functionName_;

        private static List<TimerFunction> timers;
        private static GameObject initGameObject;

        /// <summary>
        /// Creates a new TimerFunction
        /// </summary>
        private TimerFunction(GameObject gameObject, Action action, float timer, string functionName)
        {
            gameObject_ = gameObject;
            action_ = action;
            timer_ = timer;
            functionName_ = functionName;
        }

        /// <summary>
        /// Initializes the GameObject that holds the timer list
        /// </summary>
        private static void Initialize()
        {
            if (initGameObject == null)
            {
                initGameObject = new GameObject("TimerFunctionObject");
                timers = new List<TimerFunction>();
            }
        }

        /// <summary>
        /// Creates a TimerFunction that triggers after certain time
        /// </summary>
        public static TimerFunction Create(Action action, float timer, string functionName = "", bool stopWithSameName = false)
        {
            Initialize();

            if (stopWithSameName) StopAllWithSameName(functionName);

            GameObject gameObject = new GameObject("TimerFunction_" + functionName, typeof(MonoBehaviourHook));
            TimerFunction timerFunction = new TimerFunction(gameObject, action, timer, functionName);
            gameObject.GetComponent<MonoBehaviourHook>().onUpdate = timerFunction.Update;

            timers.Add(timerFunction);

            return timerFunction;
        }

        /// <summary>
        /// Removes a timer from the timer list
        /// </summary>
        public static void RemoveTimer(TimerFunction timerFunction)
        {
            Initialize();

            timers.Remove(timerFunction);
        }

        /// <summary>
        /// Stops a timer with name == functionName
        /// </summary>
        public static void StopWithSameName(string functionName)
        {
            Initialize();

            for (int i = timers.Count - 1; i != 0; i--)
            {
                if (timers[i].functionName_.Equals(functionName))
                {
                    timers[i].Destroy();
                    return;
                }
            }
        }

        /// <summary>
        /// Stops all timers with name == functionName
        /// </summary>
        public static void StopAllWithSameName(string functionName)
        {
            Initialize();

            for (int i = timers.Count - 1; i != 0; i--)
            {
                if (timers[i].functionName_.Equals(functionName))
                {
                    timers[i].Destroy();
                }
            }
        }

        /// <summary>
        /// Updates timer
        /// </summary>
        private void Update()
        {
            if ((timer_ -= Time.deltaTime) <= 0f)
            {
                action_();
                Destroy();
            }
        }

        /// <summary>
        /// Destroys timer
        /// </summary>
        private void Destroy()
        {
            RemoveTimer(this);
            if (gameObject_ != null) UnityEngine.Object.Destroy(gameObject_);
        }
    }
}
