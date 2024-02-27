using DungeonEscape;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace DungeonEscape
{
    public class Enemy : MonoBehaviour
    {
        [Header("Current Enemy")]
        public Enemy_Scriptable curEnemy;
        public Vitals vitals;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider armorSlider;

        [Header("External References")]
        [SerializeField] private Player_Controller playerController;
        [SerializeField] private Game_Manager gameManager;

        private void Update()
        {
            if (!gameManager.PlayerTurn())
            {
                AttackPlayer();
            }
        }


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
        private void AttackPlayer()
        {
            if (isAlive())
            {
                int miss = (Random.Range(0f, 1f) <= curEnemy.missChance) ? 0 : 1;
                Debug.Log(miss);
                if (miss == 1)
                {
                    playerController.HandleIncomingAttack(curEnemy.damage);
                    if(!playerController.isAlive())
                    {
                        playerController.PlayerLose();
                    }
                    gameManager.isPlayersTurn = true;
                }
                else
                {
                    gameManager.isPlayersTurn = true;
                    Debug.Log("Missed!");
                    return;
                }
            }
            else
            {
                playerController.PlayerWin();
            }
        }

        public bool isAlive()
        {
            if (vitals.health <= 0) {
                return false;
            }
            return true;
        }
    }
}
