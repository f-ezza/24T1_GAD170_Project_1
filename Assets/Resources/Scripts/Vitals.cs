using DungeonEscape;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DungeonEscape
{
    public class Vitals : MonoBehaviour
    {
        #region - Variables - 
        [Header("Vitals")]
        public float health;
        public float armor;

        [SerializeField] private bool isPlayer = false;
        [SerializeField] private bool hasArmor = false;

        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider armorBar;
        #endregion

        #region - Methods -
        private void Start()
        {
            if (gameObject.GetComponent<Player_Controller>() != null)
            {
                isPlayer = true;
                healthBar.maxValue = health;
                healthBar.minValue = 0;
                healthBar.value = health;
                armorBar.maxValue = armor;
                armorBar.minValue = 0;
                armorBar.value = armor;
            }
            if(armor >= 1) { hasArmor = true; }
        }

        /*
         * TakeDamage is a method that executes the following steps when called with a float value representing the amount of damage:
         *  - Checks if the character has armor
         *    - If true:
         *      - Reduces the health by 25% of the damage and the armor by the full damage
         *      - Checks if the armor has depleted and sets 'hasArmor' to false
         *    - If false:
         *      - Reduces the health by the full damage
         *  - Updates the current values of the health and armor bars ('healthBar' and 'armorBar')
         *  - Checks if the health has reached or fallen below 0
         *    - If true:
         *      - Sets the health to 0
         *      - If the character is the player, destroys the GameObject associated with this script
         */
        public void TakeDamage(float damage)
        {
            if (hasArmor)
            {
                health -= damage * 0.25f;
                armor -= damage;

                if (armor <= 0)
                {
                    hasArmor = false;
                }
            }
            else
            {
                health -= damage;
            }

            healthBar.value = health; 
            armorBar.value = armor;
            if (health <= 0) { health = 0; if (isPlayer) { Destroy(gameObject); }}

        }

        /*
         * Heal is a method that executes the following steps when called:
         *  - Sets the maximum value of the health bar ('healthBar') to 100
         *  - Sets the minimum value of the health bar to 0
         *  - Sets the current value of the health bar to 100
         *  - Sets the maximum value of the armor bar ('armorBar') to 100
         *  - Sets the minimum value of the armor bar to 0
         *  - Sets the current value of the armor bar to 100
         */
        public void Heal()
        {
            healthBar.maxValue = 100f;
            healthBar.minValue = 0;
            healthBar.value = 100f;
            armorBar.maxValue = 100f;
            armorBar.minValue = 0;
            armorBar.value = 100f;
        }
        #endregion
    }
}
