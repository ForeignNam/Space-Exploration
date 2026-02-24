using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler desenemyPooler;
    [SerializeField] protected int lives;
    [SerializeField] protected int maxlives;
    [SerializeField] protected int damage;
    [SerializeField] protected int experienceToGive;

    protected AudioSource hitsound;
    protected AudioSource dessound;

    protected float SpeedX;
    protected float SpeedY;
    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void OnEnable()
    {
        lives = maxlives;
    }
    public virtual void Start()
    {
       
        flashWhite = GetComponent<FlashWhite>();

    }
  
    public virtual void Update()
    {
        transform.position += new Vector3(SpeedX * Time.deltaTime, SpeedY * Time.deltaTime, 0f);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if(player) player.TakeDamage(damage);
        }
    }
    public virtual void TakeDamage(int damage)
    {
        lives -= damage;
        AudioManager.instance.PlayModifiedSound(hitsound);
        if (lives > 0)
        {
           flashWhite.Flash();
        }
        else
        {
            flashWhite.Reset();
            AudioManager.instance.PlayModifiedSound(dessound);
            GameObject desenemy = desenemyPooler.GetPooledObject();
            desenemy.transform.position = transform.position;
            desenemy.transform.rotation = transform.rotation;
            desenemy.SetActive(true);
            PlayerController.instance.GainExperience(experienceToGive);
            gameObject.SetActive(false);

        }
    }
}
