using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hokuro.Functions
{
    // Class to create functions that are executed until they return true
    public class UpdateFunction
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
        private readonly Func<bool> func_;
        private readonly string functionName_;
        private bool isActive_;

        private static List<UpdateFunction> timers;
        private static GameObject initGameObject;

        /// <summary>
        /// Creates a new UpdateFunction
        /// </summary>
        private UpdateFunction(GameObject gameObject, Func<bool> func, string functionName, bool isActive)
        {
            gameObject_ = gameObject;
            func_ = func;
            functionName_ = functionName;
            isActive_ = isActive;
        }

        /// <summary>
        /// Initializes the GameObject that holds the timer list
        /// </summary>
        private static void Initialize()
        {
            if (initGameObject == null)
            {
                initGameObject = new GameObject("UpdateFunctionObject");
                timers = new List<UpdateFunction>();
            }
        }

        /// <summary>
        /// Creates a UpdateFunction using an action
        /// </summary>
        public static UpdateFunction Create(Action action)
        {
            return Create(() => { action(); return false; }) ;
        }

        /// <summary>
        /// Creates a UpdateFunction using a func<bool>
        /// </summary>
        public static UpdateFunction Create(Func<bool> func, string functionName = "", bool isActive = true, bool stopAllWithSameName = false)
        {
            Initialize();

            if (stopAllWithSameName) StopAllWithSameName(functionName);

            GameObject gameObject = new GameObject("UpdateFunction_" + functionName, typeof(MonoBehaviourHook));
            UpdateFunction updateFunction = new UpdateFunction(gameObject, func, functionName, isActive);
            gameObject.GetComponent<MonoBehaviourHook>().onUpdate = updateFunction.Update;

            timers.Add(updateFunction);

            return updateFunction;
        }

        /// <summary>
        /// Removes a timer from the timer list
        /// </summary>
        public static void RemoveTimer(UpdateFunction updateFunction)
        {
            Initialize();

            timers.Remove(updateFunction);
        }

        /// <summary>
        /// Destroys a timer from the timer list
        /// </summary>
        public static void DestroyTimer(UpdateFunction updateFunction)
        {
            Initialize();

            if (updateFunction != null) updateFunction.Destroy();
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
        /// Pauses timer
        /// </summary>
        public void Pause()
        {
            isActive_ = false;
        }

        /// <summary>
        /// Resumes timer
        /// </summary>
        public void Resume()
        {
            isActive_ = true;
        }

        /// <summary>
        /// Updates timer
        /// </summary>
        private void Update()
        {
            if (!isActive_) return;
            if (func_())
            {
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