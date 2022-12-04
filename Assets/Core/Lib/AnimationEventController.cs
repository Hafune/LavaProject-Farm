using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEventController : MonoBehaviour
    {
        public static readonly string CallbackFunctionName = nameof(AnimationEventControllerCallback);

        private Dictionary<string, EventActions> _actions = new();

        public void AnimationEventControllerCallback(AnimationEvent animationEvent) =>
            _actions[animationEvent.stringParameter].callback?.Invoke();

        public void AddEventListener(string eventStringParam, Action action)
        {
            if (!_actions.ContainsKey(eventStringParam))
                _actions[eventStringParam] = new EventActions();

            _actions[eventStringParam].callback += action;
        }

        public void RemoveEventListener(string eventStringParam, Action action) =>
            _actions[eventStringParam].callback -= action;

        private class EventActions
        {
            public Action callback;
        }
    }
}