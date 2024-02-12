using System.Collections.Generic;
using UnityEngine;

namespace DungeonEscape
{
    public class Area : MonoBehaviour
    {
        public string areaName;
        public string areaDescription;
        public Sprite areaSprite;

        [SerializeField] private List<Enemy> enemies;

        public void SetAreaData(AreaData data)
        {
            areaName = data.areaName;
            areaDescription = data.areaDescription;
            areaSprite = data.areaSprite;
            gameObject.name = areaName;
        }
    }
}
