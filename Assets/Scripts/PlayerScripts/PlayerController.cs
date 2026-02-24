using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rb;
    private Vector2 PlayerPosition;
    private Animator animator;
    private FlashWhite flashWhite;
    [SerializeField] private float MoveSpeed;

    public bool boosting;
    [Header("Energy Player")]
    [SerializeField] private float Energy;
    [SerializeField] private float MaxEnergy;
    [SerializeField] private float Energyregen;

    [Header("Health Player")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxhealth;
    private ObjectPooler destroyplayer;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem engineEffect;
    [Header("Experience System")]

    [SerializeField] private int experience;
    [SerializeField] private int currentlevel;
    [SerializeField] private int maxlevel;
    [SerializeField] private List<int> playerlevel;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
            instance = this;
    }
    void Start()
    {

        flashWhite = GetComponent<FlashWhite>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        destroyplayer = GameObject.Find("Boom1Pool").GetComponent<ObjectPooler>();
        for(int i = playerlevel.Count ; i < maxlevel; i++)
        {
            playerlevel.Add(Mathf.CeilToInt(playerlevel[playerlevel.Count -1 ] * 1.1f  + 15));
        }
        Energy = MaxEnergy;
        UIController.instance.UpdateEnergy(Energy, MaxEnergy);
        _health = _maxhealth;
        UIController.instance.UpdateHealth(_health, _maxhealth);
        experience = 0;
        UIController.instance.UpdateExperience(experience, playerlevel[currentlevel]);
    }


    private void Update()
    {
        if (Time.timeScale > 0)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);

            PlayerPosition = new Vector2(directionX, directionY).normalized;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnterBoosting();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoosting();
            }
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire1"))
            {
                PhaserWeapon.instance.Shoot();
            }
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(PlayerPosition.x * MoveSpeed, PlayerPosition.y * MoveSpeed);
        if (boosting)
        {
            if (Energy >= 0.5f)
            {
                Energy -= 0.5f;
            }
            else
            {
                ExitBoosting();
            }
        }
        else
        {
            if (Energy < MaxEnergy)
                Energy += Energyregen;


        }
        UIController.instance.UpdateEnergy(Energy, MaxEnergy);
    }
    private void EnterBoosting()
    {
        if (Energy > 10)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.fire);

            animator.SetBool("boosting", true);
            GameManager.instance.SetWorldSpeed(7f);
            boosting = true;
            engineEffect.Play();
        }

    }
    public void ExitBoosting()
    {
        animator.SetBool("boosting", false);
        GameManager.instance.SetWorldSpeed(1f);
        boosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
            {
                asteroid.TakeDamage(1, false);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(1);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        UIController.instance.UpdateHealth(_health, _maxhealth);
        AudioManager.instance.PlaySound(AudioManager.instance.hit);
        flashWhite.Flash();
        if (_health <= 0)
        {
            ExitBoosting();
            GameManager.instance.SetWorldSpeed(0);
            gameObject.SetActive(false);

            GameObject destroyEffect = destroyplayer.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);
            GameManager.instance.GameOver();
            AudioManager.instance.PlaySound(AudioManager.instance.ice);
        }
    }
    public void GainExperience(int exp)
    {
        experience += exp;
        UIController.instance.UpdateExperience(experience, playerlevel[currentlevel]);
        if(experience >= playerlevel[currentlevel])
        {
            LevelUp();
        }

    }

    public void LevelUp()
    {
       experience -= playerlevel[currentlevel];
        if(currentlevel < maxlevel -1)
        {
            currentlevel++;
        }
        UIController.instance.UpdateExperience(experience, playerlevel[currentlevel]);
        PhaserWeapon.instance.LevelUp();
        _maxhealth++;
        _health = _maxhealth;
        UIController.instance.UpdateHealth(_health, _maxhealth);

    }
}