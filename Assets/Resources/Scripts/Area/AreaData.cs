using UnityEngine;

namespace DungeonEscape
{
    [CreateAssetMenu(fileName = "New Area Data", menuName = "DungeonEscape/AreaData", order = 1)]
    public class AreaData : ScriptableObject
    {
        public string areaName;
        public string areaDescription;
        public Sprite areaSprite;
        public Enemy_Scriptable enemy;
        public int areaIndex;
    }
}