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
        [SerializeField] private float skills;
        private Player_Controller playerController;

        public bool isMenuOpen()
        {
            return statMenu.activeSelf;
        }

        private void Start()
        {
            statMenu.SetActive(true);
            playerController = GetComponent<Player_Controller>();
            statPool = 15;
        }

        public void CloseUI()
        {
            for (int i = 0; i < inputFields.Count; i++)
            {
                int skillAmt = string.IsNullOrEmpty(inputFields[i].text) ? 3 : int.Parse(inputFields[i].text);

                Debug.Log(inputFields[i].name + ": " + skillAmt);
                
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
                statMenu.SetActive(false);
            }
        }
    }
}
