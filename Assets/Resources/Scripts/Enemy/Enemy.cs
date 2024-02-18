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
            healthSlider.maxValue = curEnemy.health;
            armorSlider.maxValue = curEnemy.armor;
            healthSlider.minValue = 0;
            armorSlider.minValue = 0;

            healthSlider.value = curEnemy.health;
            armorSlider.value = curEnemy.armor;

            vitals.health = curEnemy.health;
            vitals.armor = curEnemy.armor;
        }

        //How the enemy will attack
    }
}
