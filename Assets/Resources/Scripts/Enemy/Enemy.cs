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
        #region - Variables -
        [Header("Current Enemy")]
        public Enemy_Scriptable curEnemy;
        public Vitals vitals;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider armorSlider;

        [Header("External References")]
        [SerializeField] private Player_Controller playerController;
        [SerializeField] private Game_Manager gameManager;
        #endregion

        #region - Methods -
        private void Update()
        {
            if (!gameManager.PlayerTurn())
            {
                AttackPlayer();
            }
        }

        /*
         * ReadCurrentEnemy is a method that executes the following steps when called with an Enemy_Scriptable as an argument:
         *  - Sets the class variable 'curEnemy' to the provided enemy scriptable object
         *  - Generates a random level between 1 and 4 (inclusive) for the enemy
         *  - Retrieves the base health value from the 'curEnemy' scriptable object
         *  - Modifies the enemy's health based on the random level
         *  - Sets the maximum values of the health and armor sliders based on the modified enemy health and base armor
         *  - Sets the minimum values of the health and armor sliders to 0
         *  - Sets the initial values of the health and armor sliders to the modified enemy health and base armor, respectively
         *  - Updates the 'vitals' component's health and armor variables with the modified enemy health and base armor, respectively
         */
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

        /*
         * AttackPlayer is a private method that executes the following steps:
         *  - Checks if the enemy is still alive using the 'isAlive' method
         *    - If the enemy is alive:
         *      - Generates a random value between 0 and 1 to determine if the attack will miss based on the enemy's miss chance
         *      - Logs the generated value for debugging purposes
         *      - Checks if the attack missed (miss value is 1):
         *        - If the attack missed:
         *          - Calls the 'HandleIncomingAttack' method of the 'playerController' with the enemy's damage
         *          - Checks if the player is still alive after taking the damage
         *            - If the player is still alive, sets 'gameManager.isPlayersTurn' to true
         *            - If the player is not alive, calls the 'PlayerLose' method of the 'playerController' and ends the turn
         *          - Sets 'gameManager.isPlayersTurn' to true
         *        - If the attack did not miss:
         *          - Sets 'gameManager.isPlayersTurn' to true
         *          - Logs a message indicating the attack missed
         *          - Returns from the method
         *    - If the enemy is not alive:
         *      - Calls the 'PlayerWin' method of the 'playerController' to indicate the player has won
         */
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

        //Check if enemy is alive
        public bool isAlive()
        {
            if (vitals.health <= 0) {
                return false;
            }
            return true;
        }
        #endregion
    }
}
