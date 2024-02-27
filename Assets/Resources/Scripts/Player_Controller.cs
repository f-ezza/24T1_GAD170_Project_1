using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonEscape
{
    public class Player_Controller : MonoBehaviour
    {
        [SerializeField] private List<Area> areasPlayerList = new List<Area>();
        private Area curArea;
        [SerializeField] private int curAreaIndex;
        private Image backgroundImage;

        [SerializeField] private Slider staminaBar;
        public Enemy enemy;
        [SerializeField] private GameObject comabtUI;
        [SerializeField] private GameObject enemyGameObject;

        [SerializeField] private Game_Manager gameManager;

        [Header("Player Variables")]
        [SerializeField] private string playerName;
        [SerializeField] private string playerDescription;
        [SerializeField] private int playerAge;
        [SerializeField] private bool canMove;
        [SerializeField] private float stamina;
        [SerializeField] Vitals vitals;

        [Header("Attack Variables")]
        [SerializeField] private float baseDamage;
        [SerializeField] private float stamReduction;

        [Header("Stats")]
        public List<PlayerStats> playerStats;
        [SerializeField] private float healthLevel;
        [SerializeField] private float staminaLevel;
        [SerializeField] private float dexterityLevel;
        [SerializeField] private float perceptionLevel;
        [SerializeField] private float strengthLevel;   

        [Header("Inventory")]
        [SerializeField] private List<ConsumableItem> inventory;

        private void Awake()
        {
            canMove = false;
            vitals = GetComponent<Vitals>();
            staminaBar.maxValue = stamina;
            staminaBar.minValue = 0;
            staminaBar.value = stamina;
        }

        private void Update()
        {
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
            if(stamina <= 0)
            {
                PlayerLose();
            }
        }

        public bool isAlive()
        {
            if (vitals.health <= 0)
            {
                return false;
            }
            return true;
        }

        public void AssignSkillPoints(List<PlayerStats> stats)
        {
            playerStats = new List<PlayerStats>();
            playerStats = stats;
            for (int i = 0; i < playerStats.Count; i++)
            {
                if (i == 0) { healthLevel = playerStats[i].statLevel; }
                if (i == 1) { staminaLevel = playerStats[i].statLevel; }
                if (i == 2) { dexterityLevel = playerStats[i].statLevel; }
                if (i == 3) { perceptionLevel = playerStats[i].statLevel; }
                if (i == 4) { strengthLevel = playerStats[i].statLevel; }

            }
            canMove = true;
        }

        public void PlayerWin()
        {
            comabtUI.SetActive(false);
            enemyGameObject.SetActive(false);
            vitals.Heal();
            canMove = true;
        }

        public void PlayerLose()
        {
            comabtUI.SetActive(false);
            enemyGameObject.SetActive(false);
            stamina = 0f;
            staminaBar.value = 0f;
            vitals.health = 0f;
            vitals.armor = 0f;
            Debug.Log("Oh no!, you died or ran out of stamina :C");
            Destroy(gameObject);
        }

        public void AttackEntity()
        {
            if (gameManager.PlayerTurn() && stamina >= 5f) 
            {
                HandlePlayerStaminaCalculation();
                HandlePlayerDamageCalculation();
                gameManager.isPlayersTurn = false;
                
            }

        }

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
        private void HandlePlayerDamageCalculation()
        {
            float dmgMulti = 1;
            switch(strengthLevel)
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

        public void HandleIncomingAttack(float damage)
        {
            float dmgMulti = 1f;
            switch(healthLevel)
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
            float damageToGive = baseDamage * dmgMulti;
            vitals.TakeDamage(damageToGive);
        }

        public void GetAreaList(List<Area> areas)
        {
            areasPlayerList = areas;
        }

        public void LoadArea(int index)
        {
            Debug.Log("Loaded new area " + areasPlayerList[index].areaName);
            curArea = areasPlayerList[index];
            curAreaIndex = index;
            backgroundImage ??= GetComponentInChildren<Canvas>().GetComponentInChildren<Image>(); //Use a null-coalescing assignment to keep the if check simple
            backgroundImage.sprite = curArea.areaSprite;

            canMove = true;
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
    }
}
