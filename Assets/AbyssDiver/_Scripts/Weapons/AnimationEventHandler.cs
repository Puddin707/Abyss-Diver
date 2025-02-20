using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyntez.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    }
}
