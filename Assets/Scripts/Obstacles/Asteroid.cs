using UnityEngine;
using System.Collections;
public class Asteroid : MonoBehaviour
{
   
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites;
    private FlashWhite flashWhite;

    [SerializeField] private ObjectPooler destroyEffectPool;
    private int MaxLives ;
    private int lives ;
    private int damage ;
    private int experienceValue = 1;
    float PushX;
    float PushY;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void OnEnable()
    {
        lives = MaxLives;
        transform.rotation = Quaternion.identity;
         PushX = Random.Range(-1f, 0);
         PushY = Random.Range(-1f, 1);
        rb.linearVelocity = new Vector2(PushX, PushY);
    }
    private void Start()
    {
        destroyEffectPool = GameObject.Find("Boom2Pool").GetComponent<ObjectPooler>();
        flashWhite = GetComponent<FlashWhite>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        PushX = Random.Range(-1f, 0);
        PushY = Random.Range(-1f, 1);

        float randonScale= Random.Range(0.6f, 1.1f);
        transform.localScale = new Vector3(randonScale, randonScale);
        MaxLives = 5;
        lives = MaxLives;
        damage = 1;
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
           PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.TakeDamage(damage);
            }
            
        } 
    }
    public void TakeDamage(int _damage, bool giveExperience)
    {
        AudioManager.instance.PlayModifiedSound(AudioManager.instance.hitRock);
       
        lives-= _damage;
        if(lives >0)
        flashWhite.Flash();
        else 
        {
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.transform.localScale = transform.localScale;
            destroyEffect.SetActive(true);

            
            AudioManager.instance.PlayModifiedSound(AudioManager.instance.boom2);
            flashWhite.Reset();
            gameObject.SetActive(false);
            if(giveExperience) PlayerController.instance.GainExperience(experienceValue);
        }
    }
    
}
