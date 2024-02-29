using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonEscape
{
    public class Player_Controller : MonoBehaviour
    {
        #region - Variables - 
        // Fields for player-related variables and components
        [SerializeField] private List<Area> areasPlayerList = new List<Area>();
        private Area curArea;
        [SerializeField] private int curAreaIndex;
        private Image backgroundImage;

        // UI elements and references
        [SerializeField] private Slider staminaBar;
        public Enemy enemy;
        [SerializeField] private GameObject comabtUI; // Typo: should be "combatUI"
        [SerializeField] private GameObject enemyGameObject;

        // Game manager reference
        [SerializeField] private Game_Manager gameManager;

        // Player information and stats
        [Header("Player Variables")]
        [SerializeField] private string playerName;
        [SerializeField] private string playerDescription;
        [SerializeField] private int playerAge;
        [SerializeField] private bool canMove;
        [SerializeField] private float stamina;
        [SerializeField] Vitals vitals;

        // Attack-related variables
        [Header("Attack Variables")]
        [SerializeField] private float baseDamage;
        [SerializeField] private float stamReduction;

        // Player stats
        [Header("Stats")]
        public List<PlayerStats> playerStats;
        [SerializeField] private float healthLevel;
        [SerializeField] private float staminaLevel;
        [SerializeField] private float dexterityLevel;
        [SerializeField] private float perceptionLevel;
        [SerializeField] private float strengthLevel;

        // Inventory (not currently used)
        [Header("Inventory")]
        [SerializeField] private List<ConsumableItem> inventory;
        #endregion

        #region - Methods -
        // Initialization method
        private void Awake()
        {
            canMove = false;
            vitals = GetComponent<Vitals>();
            staminaBar.maxValue = stamina;
            staminaBar.minValue = 0;
            staminaBar.value = stamina;
        }

        // Update method
        private void Update()
        {
            // Check for player input to progress to the next area
            if (canMove)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (curAreaIndex >= areasPlayerList.Count - 1)
                    {
                        Debug.Log("Finished the game!");
                    }
                    else
                    {
                        LoadArea(curAreaIndex + 1);
                    }
                }
            }

            // Check for player stamina depletion and enemy's status
            if (stamina <= 0 && enemy.isAlive())
            {
                PlayerLose();
            }
        }

        // Check if the player is alive
        public bool isAlive()
        {
            return vitals.health > 0;
        }

        // Allocate skill points to player stats
        public void AssignSkillPoints(List<PlayerStats> stats)
        {
            // Initialize player stats and assign skill points
            playerStats = new List<PlayerStats>();
            playerStats = stats;

            // Map stats to corresponding levels
            for (int i = 0; i < Math.Min(playerStats.Count, stats.Count); i++)
            {
                switch (i)
                {
                    case 0:
                        healthLevel = playerStats[i].statLevel;
                        break;
                    case 1:
                        staminaLevel = playerStats[i].statLevel;
                        break;
                    case 2:
                        dexterityLevel = playerStats[i].statLevel;
                        break;
                    case 3:
                        perceptionLevel = playerStats[i].statLevel;
                        break;
                    case 4:
                        strengthLevel = playerStats[i].statLevel;
                        break;
                }
            }
            canMove = true;
        }

        /*
         * PlayerWin is a method that executes the following steps when the player successfully completes the combat:
         *  - Deactivates the combat UI, making it invisible to the player
         *  - Deactivates the enemy game object, removing it from the game world
         *  - Heals the player's vitals to full health
         *  - Restores player's stamina to its maximum value (100)
         *  - Enables the player to move, allowing further interaction in the game
         */
        public void PlayerWin()
        {
            comabtUI.SetActive(false);
            enemyGameObject.SetActive(false);
            vitals.Heal();
            stamina = 100f;
            staminaBar.value = 100f;
            canMove = true;
        }

        /*
         * PlayerLose is a method that executes the following steps when the player fails to complete the game:
         *  - Deactivates the combat UI, making it invisible to the player
         *  - Deactivates the enemy game object, removing it from the game world
         *  - Sets player's stamina to zero
         *  - Updates the stamina bar to reflect the empty stamina
         *  - Sets player's health to zero
         *  - Outputs a message to the console indicating the player's defeat
         *  - Destroys the player game object, removing it from the game world
         */
        public void PlayerLose()
        {
            comabtUI.SetActive(false);
            enemyGameObject.SetActive(false);
            stamina = 0f;
            staminaBar.value = 0f;
            vitals.health = 0f;
            Debug.Log("Oh no!, you died or ran out of stamina :C");
            Destroy(gameObject);
        }

        // Player attacks the enemy
        /*
         * AttackEntity is a method that executes the following steps when the player decides to attack the enemy:
         *  - Checks if it is currently the player's turn in the game
         *  - If it is the player's turn, proceeds with the attack
         *  - Calls the HandlePlayerStaminaCalculation method to calculate and reduce player's stamina based on their stats
         *  - Calls the HandlePlayerDamageCalculation method to calculate and inflict damage to the enemy based on player's stats
         *  - Sets the game manager's flag indicating the end of the player's turn to false
         */
        public void AttackEntity()
        {
            if (gameManager.PlayerTurn())
            {
                HandlePlayerStaminaCalculation();
                HandlePlayerDamageCalculation();
                gameManager.isPlayersTurn = false;
            }
        }

        /*
         * HandlePlayerStaminaCalculation is a private method that executes the following steps to calculate player stamina reduction:
         *  - Initializes a multiplier 'stamMulti' to 1, representing no reduction by default
         *  - Utilizes a switch statement to customize the reduction multiplier based on the player's stamina level
         *  - For each stamina level, sets 'stamMulti' to a specific value as per the defined cases
         *  - Calculates the actual stamina reduction by multiplying the base reduction ('stamReduction') by 'stamMulti'
         *  - Updates the player's stamina by subtracting the calculated reduction
         *  - Updates the stamina bar value to reflect the new stamina value
         */
        private void HandlePlayerStaminaCalculation()
        {
            float stamMulti = 1;
            switch (staminaLevel)
            {
                case 1:
                    stamMulti = 0.95f;
                    break;
                case 2:
                    stamMulti = 0.85f;
                    break;
                case 3:
                    stamMulti = 0.75f;
                    break;
                case 4:
                    stamMulti = 0.65f;
                    break;
                case 5:
                    stamMulti = 0.55f;
                    break;
                case 6:
                    stamMulti = 0.45f;
                    break;
                case 7:
                    stamMulti = 0.35f;
                    break;
                case 8:
                    stamMulti = 0.25f;
                    break;
                case 9:
                    stamMulti = 0.15f;
                    break;
                case 10:
                    stamMulti = 0.05f;
                    break;
                case 11:
                    stamMulti = 0.025f;
                    break;
                default:
                    break;
            }
            stamina -= stamReduction * stamMulti;
            staminaBar.value = stamina;
        }

        /*
         * HandlePlayerDamageCalculation is a private method that executes the following steps to calculate player damage to the enemy:
         *  - Initializes a multiplier 'dmgMulti' to 1, representing no damage increase by default
         *  - Utilizes a switch statement to customize the damage multiplier based on the player's strength level
         *  - For each strength level, sets 'dmgMulti' to a specific value as per the defined cases
         *  - Calculates the actual damage to be dealt by multiplying the base damage ('baseDamage') by 'dmgMulti'
         *  - Calls the TakeDamage method of the enemy's vitals, passing the calculated damage as an argument
         *    to inflict damage to the enemy
         */
        private void HandlePlayerDamageCalculation()
        {
            float dmgMulti = 1;
            switch (strengthLevel)
            {
                case 1:
                    dmgMulti = 1.025f;
                    break;
                case 2:
                    dmgMulti = 1.05f;
                    break;
                case 3:
                    dmgMulti = 1.15f;
                    break;
                case 4:
                    dmgMulti = 1.25f;
                    break;
                case 5:
                    dmgMulti = 1.35f;
                    break;
                case 6:
                    dmgMulti = 1.45f;
                    break;
                case 7:
                    dmgMulti = 1.55f;
                    break;
                case 8:
                    dmgMulti = 1.65f;
                    break;
                case 9:
                    dmgMulti = 1.75f;
                    break;
                case 10:
                    dmgMulti = 1.85f;
                    break;
                case 11:
                    dmgMulti = 1.95f;
                    break;
                default:
                    break;
            }
            enemy.vitals.TakeDamage(baseDamage * dmgMulti);
        }

        /*
         * HandleIncomingAttack is a method that executes the following steps when the player receives an attack from the enemy:
         *  - Initializes a multiplier 'dmgMulti' to 1, representing no reduction by default
         *  - Utilizes a switch statement to customize the damage reduction based on the player's health level
         *  - For each health level, sets 'dmgMulti' to a specific value as per the defined cases
         *  - Calculates the actual damage to be received by multiplying the base damage ('baseDamage') by 'dmgMulti'
         *  - Calls the TakeDamage method of the player's vitals, passing the calculated damage as an argument
         *    to inflict damage to the player
         */
        public void HandleIncomingAttack(float damage)
        {
            float dmgMulti = 1f;
            switch (healthLevel)
            {
                case 1:
                    dmgMulti = 0.95f;
                    break;
                case 2:
                    dmgMulti = 0.85f;
                    break;
                case 3:
                    dmgMulti = 0.75f;
                    break;
                case 4:
                    dmgMulti = 0.65f;
                    break;
                case 5:
                    dmgMulti = 0.55f;
                    break;
                case 6:
                    dmgMulti = 0.45f;
                    break;
                case 7:
                    dmgMulti = 0.35f;
                    break;
                case 8:
                    dmgMulti = 0.25f;
                    break;
                case 9:
                    dmgMulti = 0.15f;
                    break;
                case 10:
                    dmgMulti = 0.05f;
                    break;
                case 11:
                    dmgMulti = 0.025f;
                    break;
                default:
                    break;
            }
            float damageToGive = damage * dmgMulti;
            vitals.TakeDamage(damageToGive);
        }

        /*
         * GetAreaList is a method that executes the following steps when called:
         *  - Takes a List<Area> as an argument, representing the list of areas obtained from the Game_Manager
         *  - Assigns the received list of areas to the class variable 'areasPlayerList'
         */
        public void GetAreaList(List<Area> areas)
        {
            areasPlayerList = areas;
        }

        /*
         * LoadArea is a method that executes the following steps when called with a specific index:
         *  - Logs a message indicating the loading of a new area with the area name
         *  - Sets the class variable 'curArea' to the area at the specified index in 'areasPlayerList'
         *  - Updates the class variable 'curAreaIndex' with the specified index
         *  - Uses null-coalescing assignment to ensure 'backgroundImage' is assigned from the child Canvas's Image component
         *  - Sets the sprite of 'backgroundImage' to the sprite of the loaded area
         *  - Sets the 'canMove' flag to true, allowing the player to move
         *  - Checks if the loaded area has an enemy
         *    - If an enemy is present:
         *      - Logs a message indicating the area has an enemy
         *      - Activates the combat UI and the enemy game object
         *      - Reads and sets the current enemy data using the 'ReadCurrentEnemy' method of the 'enemy' component
         *      - Sets the sprite of the enemy game object's Image component to the current enemy's image
         *      - Sets 'canMove' to false, preventing the player from moving during combat
         *    - If no enemy is present:
         *      - Deactivates the enemy game object and the combat UI
         *      - Logs a message indicating no enemy is in the area
         */
        public void LoadArea(int index)
        {
            Debug.Log("Loaded new area " + areasPlayerList[index].areaName);
            curArea = areasPlayerList[index];
            curAreaIndex = index;
            backgroundImage ??= GetComponentInChildren<Canvas>().GetComponentInChildren<Image>(); // Use a null-coalescing assignment to keep the if check simple
            backgroundImage.sprite = curArea.areaSprite;

            canMove = true;

            // Check if the area has an enemy
            if (curArea.enemy != null)
            {
                Debug.Log("Area has an enemy");
                comabtUI.SetActive(true);
                enemyGameObject.SetActive(true);
                enemy.ReadCurrentEnemy(curArea.enemy);
                enemyGameObject.GetComponentInChildren<Image>().sprite = enemy.curEnemy.image;
                canMove = false;
            }
            else
            {
                enemyGameObject.SetActive(false);
                comabtUI.SetActive(false);
                Debug.Log("No Enemy in area " + curArea.areaName);
            };
        }
        #endregion
    }
}