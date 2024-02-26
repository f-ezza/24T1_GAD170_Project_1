using DungeonEscape;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DungeonEscape
{
    public class Enemy : MonoBehaviour
    {
        [Header("Current Enemy")]
        [SerializeField] private Enemy_Scriptable curEnemy;
        [SerializeField] private Vitals vitals;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider armorSlider;


        public void ReadCurrentEnemy(Enemy_Scriptable enemy) 
        { 
            curEnemy = enemy;
            float level = Random.Range(1, 5);
            float enemyHealth = curEnemy.health;
            switch (level)
            {
                case 2:
                    enemyHealth = enemyHealth + 25;
                    break;
                case 3:
                    enemyHealth = enemyHealth + 50;
                    break;
                case 4:
                    enemyHealth = enemyHealth + 75;
                    break;
                case 5:
                    enemyHealth = enemyHealth + 100;
                    break;
                default:
                    enemyHealth = curEnemy.health; 
                    break;
            }
            healthSlider.maxValue = enemyHealth;
            armorSlider.maxValue = curEnemy.armor;
            healthSlider.minValue = 0;
            armorSlider.minValue = 0;

            healthSlider.value = enemyHealth;
            armorSlider.value = curEnemy.armor;

            vitals.health = enemyHealth;
            vitals.armor = curEnemy.armor;
        }

        //How the enemy will attack
    }
}
