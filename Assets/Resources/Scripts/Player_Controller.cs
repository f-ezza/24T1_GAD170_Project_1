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
        [SerializeField] private Enemy enemy;
        [SerializeField] private GameObject comabtUI;
        [SerializeField] private GameObject enemyGameObject;

        [Header("Player Variables")]
        [SerializeField] private string playerName;
        [SerializeField] private string playerDescription;
        [SerializeField] private int playerAge;
        [SerializeField] private bool canMove;
        [SerializeField] private float stamina;

        [Header("Stats")]
        public List<PlayerStats> playerStats;
        [SerializeField] private float healthLevel;
        [SerializeField] private float staminaLevel;
        [SerializeField] private float dexterityLevel;
        [SerializeField] private float perceptionLevel;
        [SerializeField] private float strengthLevel;   

        [Header("Inventory")]
        [SerializeField] private List<ConsumableItem> inventory;

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
        }



        public void AssignSkillPoints()
        {
            for (int i = 0; i < playerStats.Count; i++)
            {
                if (i == 0) { healthLevel = playerStats[i].statLevel; }
                if (i == 1) { staminaLevel = playerStats[i].statLevel; }
                if (i == 2) { dexterityLevel = playerStats[i].statLevel; }
                if (i == 3) { perceptionLevel = playerStats[i].statLevel; }
                if (i == 4) { strengthLevel = playerStats[i].statLevel; }

            }
        }

        public void AttackEntity()
        {
            //Do Damage blah, blah, blah

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
