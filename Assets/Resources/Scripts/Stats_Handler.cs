using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace DungeonEscape
{

    public class PlayerStats
    {
        public string statName;
        public int statLevel;
    }

    public class Stats_Handler : MonoBehaviour
    {
        public GameObject statMenu;
        [SerializeField] private int statPool;
        int sumOfSkills;
        [SerializeField] private List<TMP_InputField> inputFields;
        [SerializeField] private List<PlayerStats> skills;
        private Player_Controller playerController;

        public bool isMenuOpen()
        {
            return statMenu.activeSelf;
        }

        private void Start()
        {
            statMenu.SetActive(true);
            playerController = GetComponent<Player_Controller>();
            skills = new List<PlayerStats>();
            statPool = 15;
        }

        public void CloseUI()
        {
            sumOfSkills = 0;
            for (int i = 0; i < inputFields.Count; i++)
            {
                int skillAmt = string.IsNullOrEmpty(inputFields[i].text) ? 3 : int.Parse(inputFields[i].text);

                Debug.Log(inputFields[i].name + ": " + skillAmt);

                PlayerStats stat = new PlayerStats();
                stat.statName = inputFields[i].name;
                stat.statLevel = skillAmt;


                skills.Add(stat);
                
                sumOfSkills += skillAmt;
            }

            Debug.Log(sumOfSkills.ToString());

            if (sumOfSkills != statPool)
            {
                Debug.Log("Sorry, you need to use 15!");
            }
            else
            {
                // Do something with the updated playerStats if needed.
                playerController.AssignSkillPoints(skills);
                statMenu.SetActive(false);
            }
        }
    }
}
