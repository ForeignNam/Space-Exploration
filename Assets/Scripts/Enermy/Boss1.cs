using UnityEngine;

public class Boss1 : Enemy
{
    
    private Animator anim;
   
    private enum BossState { Patrol, Charge }
    private BossState currentState;

    private float SwitchTimer;
    private float SwitchInterval;

    
    
 
    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        EnterChargeState();
        AudioManager.instance.PlaySound(AudioManager.instance.bossspawn);
    }
    public override void Start()
    {
        base.Start();
        desenemyPooler = GameObject.Find("Boom3Pool").GetComponent<ObjectPooler>();
        hitsound = AudioManager.instance.hitArmor;
        dessound = AudioManager.instance.boom2;

    }


    public override void Update()
    {
        //base.Update();

        if (PlayerController.instance == null) return;

        float positionPlayer = PlayerController.instance.transform.position.x;
        if (SwitchTimer > 0)
        {
            SwitchTimer -= Time.deltaTime;
        }
        else
        {
          
            DecideNextState(positionPlayer);

        }
       
        if (transform.position.x < positionPlayer && currentState == BossState.Patrol)
        {
            EnterChargeState();
        }
        if (transform.position.y > 3f) SpeedY = -Mathf.Abs(SpeedY);
        else if (transform.position.y < -3f) SpeedY = Mathf.Abs(SpeedY);
        bool boost= PlayerController.instance.boosting;
        float MoveX;
        if(boost && currentState == BossState.Patrol)
        {
            MoveX = GameManager.instance.WorldSpped * Time.deltaTime * -0.5f;   
        }
        else
        {
            MoveX = SpeedX * Time.deltaTime;
        }
       float MoveY= SpeedY * Time.deltaTime;
        transform.position += new Vector3(MoveX, MoveY, 0);

        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
    private void DecideNextState(float playerX)
    {
        if(currentState == BossState.Charge)
        {
            if(transform.position.x > playerX)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }

        }
        
    }
     void EnterPatrolState()
    {
        currentState = BossState.Patrol;
        SpeedX = 0;
        SpeedY = Random.Range(-2f, 2f);
        SwitchInterval =Random.Range(2f,3f);
        SwitchTimer = SwitchInterval;
        
        anim.SetBool("Charging",  false);
    }
     void EnterChargeState()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.bossCharge);
        }
        currentState = BossState.Charge;
        SpeedX = -5f;
        SpeedY = 0;
        SwitchInterval = Random.Range(2f, 2.5f); ;
        SwitchTimer = SwitchInterval;
       
        anim.SetBool("Charging", true);
        
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
            {
                asteroid.TakeDamage(damage,false);
            }
        }
        
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        AudioManager.instance.PlayModifiedSound(AudioManager.instance.hit);
        lives -= _damage;
        if(lives <= 0)
        {
            GameObject destroyEffectBoss = desenemyPooler.GetPooledObject();
            destroyEffectBoss.transform.position = transform.position;
            destroyEffectBoss.transform.rotation = transform.rotation;
            destroyEffectBoss.SetActive(true);
            gameObject.SetActive(false);
            PlayerController.instance.GainExperience(experienceToGive);
        }
    }
    
}
