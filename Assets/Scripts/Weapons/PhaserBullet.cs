using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    private PhaserWeapon WeaponLevels;
    void Start()
    {
        WeaponLevels = PhaserWeapon.instance;
    }
    void Update()
    {
        transform.position += new Vector3(WeaponLevels.weaponStats[WeaponLevels.weaponLevel].speed * Time.deltaTime, 0f);
        if (transform.position.x > 9)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
            {
                asteroid.TakeDamage(WeaponLevels.weaponStats[WeaponLevels.weaponLevel].damage,true);
            }
            gameObject.SetActive(false);

        }
       
        else if (collision.gameObject.CompareTag("Critter"))
        {
           
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(WeaponLevels.weaponStats[WeaponLevels.weaponLevel].damage);
            }
            gameObject.SetActive(false);
        }
    }
}
