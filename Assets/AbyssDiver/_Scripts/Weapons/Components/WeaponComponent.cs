using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyntez.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        protected bool isAttackActive;

        protected virtual void Awake() {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void HandleEnter() {
            isAttackActive = true;
        }

        protected virtual void HandleExit() {
            isAttackActive = false;
        }

        protected virtual void OnEnable() {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable() {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }
}
