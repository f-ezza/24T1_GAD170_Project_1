using UnityEngine;

namespace DungeonEscape
{
    public enum EnemyBehaviour { Aggressive, Mild, Defensive, Peaceful}

    [CreateAssetMenu(fileName = "New Enemy", menuName = "DungeonEscape/Enemy", order = 1)]
    public class Enemy_Scriptable : ScriptableObject
    {
        public string enemyName;
        public string enemyDescription;
        public float health;
        public float armor;

        public float damage;
        public float missChance;


        public EnemyBehaviour behaviour;
        public Sprite image;
    }
}