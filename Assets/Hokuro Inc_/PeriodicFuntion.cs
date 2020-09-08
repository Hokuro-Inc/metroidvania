using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hokuro.Functions
{
    // Class to create periodic functions
    public class PeriodicFuntion
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
        private readonly float baseTimer_;
        private readonly string functionName_;

        private static List<PeriodicFuntion> timers;
        private static GameObject initGameObject;

        /// <summary>
        /// Creates a new PeriodicFunction
        /// </summary>
        private PeriodicFuntion(GameObject gameObject, Action action, float timer, string functionName)
        {
            gameObject_ = gameObject;
            action_ = action;
            timer_ = timer;
            baseTimer_ = timer;
            functionName_ = functionName;
        }

        /// <summary>
        /// Initializes the GameObject that holds the timer list
        /// </summary>
        private static void Initialize()
        {
            if (initGameObject == null)
            {
                initGameObject = new GameObject("PeriodicFunctionObject");
                timers = new List<PeriodicFuntion>();
            }
        }

        /// <summary>
        /// Creates a PeriodicFunction that persists through scenes
        /// </summary>
        public static PeriodicFuntion CreateGlobal(Action action, float timer)
        {
            PeriodicFuntion periodicFuntion = Create(action, timer);
            MonoBehaviour.DontDestroyOnLoad(periodicFuntion.gameObject_);
            return periodicFuntion;
        }

        /// <summary>
        /// Creates a PeriodicFunction that triggers every certain time
        /// </summary>
        public static PeriodicFuntion Create(Action action, float timer, string functionName = "", bool stopWithSameName = false, bool activateInmediately = false)
        {
            Initialize();

            if (stopWithSameName) StopAllWithSameName(functionName);

            GameObject gameObject = new GameObject("PeriodicFunction_" + functionName, typeof(MonoBehaviourHook));
            PeriodicFuntion periodicFunction = new PeriodicFuntion(gameObject, action, timer, functionName);
            gameObject.GetComponent<MonoBehaviourHook>().onUpdate = periodicFunction.Update;

            timers.Add(periodicFunction);

            if (activateInmediately) action();

            return periodicFunction;
        }

        /// <summary>
        /// Removes a timer from the timer list
        /// </summary>
        public static void RemoveTimer(PeriodicFuntion periodicFuntion)
        {
            Initialize();

            timers.Remove(periodicFuntion);
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
        /// Checks if a timer is active
        /// </summary>
        public static bool IsTimerActive(string functionName)
        {
            Initialize();

            for (int i = timers.Count - 1; i != 0; i--)
            {
                if (timers[i].functionName_.Equals(functionName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Changes the timer trigger frecuency
        /// </summary>
        public void SetTimer(float timer)
        {
            timer_ = timer;
        }

        /// <summary>
        /// Updates timer
        /// </summary>
        private void Update()
        {
            if ((timer_ -= Time.deltaTime) <= 0f)
            {
                action_();
                timer_ = baseTimer_;
            }
        }

        /// <summary>
        /// Destroys timer
        /// </summary>
        public void Destroy()
        {
            RemoveTimer(this);
            if (gameObject_ != null) UnityEngine.Object.Destroy(gameObject_);
        }
    }
}
