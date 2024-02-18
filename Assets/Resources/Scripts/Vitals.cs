using DungeonEscape;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DungeonEscape
{
    public class Vitals : MonoBehaviour
    {
        [Header("Vitals")]
        public float health;
        public float armor;

        [SerializeField] private bool isPlayer = false;
        [SerializeField] private bool hasArmor = false;

        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider armorBar;

        //Other misc stats later

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
    }
}
