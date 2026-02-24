using UnityEngine;

public class PhaserWeapon : Weapon
{
    public static PhaserWeapon instance;

   
    [SerializeField] private ObjectPooler bulletPool;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
            instance = this;
    }
    public void Shoot()
    {
        
        AudioManager.instance.PlayModifiedSound(AudioManager.instance.shoot);
        for(int i=0; i< weaponStats[weaponLevel].amount; i++)
        {
            float yPos = transform.position.y ;
            GameObject bullet = bulletPool.GetPooledObject();
            if(weaponStats[weaponLevel].amount > 1)
            {
                float spacing = weaponStats[weaponLevel].range / (weaponStats[weaponLevel].amount - 1);
                 yPos = transform.position.y - (weaponStats[weaponLevel].range / 2) + i * spacing;
            }
           

            bullet.transform.position = new Vector2(transform.position.x, yPos);
            bullet.transform.localScale = new Vector2(weaponStats[weaponLevel].size, weaponStats[weaponLevel].size);  
            bullet.SetActive(true);
        }
       
    }
    public void LevelUp()
    {
        if(weaponLevel < weaponStats.Count -1)
        {
            weaponLevel++;
        }
    }
   
}
