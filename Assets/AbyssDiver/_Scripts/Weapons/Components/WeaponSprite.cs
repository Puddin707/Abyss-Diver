using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Hyntez.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer baseSpriteRenderer;
        private SpriteRenderer weaponSpriteRenderer;

        [SerializeField] private WeaponSprites[] weaponSprites;
        private int currentWeaponSpriteIndex;
        protected override void HandleEnter() {
            base.HandleEnter();

            currentWeaponSpriteIndex = 0;
        }
        private void HandleBaseSpriteChange(SpriteRenderer sr) {
            if (!isAttackActive) {
                weaponSpriteRenderer.sprite = null;
                return;
            }

            var currentAttackSprite = weaponSprites[weapon.CurrentAttackCounter].Sprites;

            if(currentWeaponSpriteIndex >= currentAttackSprite.Length) {
                Debug.LogWarning($"{weapon.name} weapon sprites lenght mismatch");
                return;
            }

            weaponSpriteRenderer.sprite = currentAttackSprite[currentWeaponSpriteIndex];

            currentWeaponSpriteIndex++;
        }

        protected override void Awake()
        {
            base.Awake();

            baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

            //TODO: Need Weapon Data
            //baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            //weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        }

        protected override void OnEnable() {
            base.OnEnable();

            baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable() {
            base.OnDisable();

            baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

            weapon.OnEnter -= HandleEnter;
        }
    }

    [System.Serializable]
    public class WeaponSprites 
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
        
    }
}
