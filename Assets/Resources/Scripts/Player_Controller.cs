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
        [SerializeField] private int healthLevel = 0;
        [SerializeField] private int staminaLevel = 0;
        [SerializeField] private int dexterityLevel = 0;
        [SerializeField] private int perceptionLevel = 0;
        [SerializeField] private int strengthLevel = 0;

        [Header("Inventory")]
        [SerializeField] private List<ConsumableItem> inventory;

        private void Start()
        {
            staminaBar.maxValue = 100;
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
        }


        public void CreateCharCreatePanel()
        {

        }

        public void AttackEntity()
        {
            //Do Damage blah, blah, blah

            switch(staminaLevel)
            {
                case 1:
                    stamina -= 25 * 0.20f;
                    break;
                case 2:
                    stamina -= 25 * 0.25f;
                    break;
                case 3:
                    stamina -= 25 * 0.30f;
                    break;
                case 4:
                    stamina -= 25 * 0.35f;
                    break;
                case 5:
                    stamina -= 25 * 0.40f;
                    break;
                default:
                    stamina -= 25;
                    break;
            }
            staminaBar.value = stamina;
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
