using UnityEngine;

namespace DungeonEscape
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "DungeonEscape/Consumable", order = 1)]
    public class ConsumableItem : ScriptableObject
    {
        public string itemName;
        public string itemDescription;
        public Sprite itemIcon;
        public int uses;
        public bool isKey;

    }
}