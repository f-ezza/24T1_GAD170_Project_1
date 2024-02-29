using System.Collections.Generic;
using UnityEngine;

namespace DungeonEscape
{
    public class Area : MonoBehaviour
    {
        #region - Variables -
        public string areaName;
        public string areaDescription;
        public Sprite areaSprite;
        public Enemy_Scriptable enemy;

        [SerializeField] private List<Enemy> enemies;
        #endregion

        #region - Methods - 
        /*
         * SetAreaData is a method that executes the following steps when called with an AreaData object as an argument:
         *  - Sets the class variables 'areaName', 'areaDescription', 'areaSprite', and 'enemy' to the corresponding values from the provided AreaData
         *  - Sets the name of the GameObject associated with this script to the 'areaName'
         */
        public void SetAreaData(AreaData data)
        {
            areaName = data.areaName;
            areaDescription = data.areaDescription;
            areaSprite = data.areaSprite;
            gameObject.name = areaName;
            enemy = data.enemy;
        }
        #endregion
    }
}
