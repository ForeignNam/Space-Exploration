using UnityEngine;
using System.Collections.Generic;
public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponLevelStats> weaponStats;


    [System.Serializable]
    public class WeaponLevelStats
    {
        public float speed;
        public int damage;
        public float amount;
        public float size;
        public float range;

    }
   

}
