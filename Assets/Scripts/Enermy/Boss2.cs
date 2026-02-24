using UnityEngine;

public class Boss2 : Enemy
{
    private bool Charging =true;
    private Animator anim;
    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
      
    }
    public override void OnEnable()
    {
        base.OnEnable();
        EnterIdleState();
       
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
        base.Update();
        float positionPlayer = PlayerController.instance.transform.position.x;

        if (transform.position.y >4 || transform.position.y < -4)
        {
            SpeedY *= -1;
        }
        if(transform.position.x > 7.5)
        {
            EnterIdleState();
        }
        else if(transform.position.x < -5f || transform.position.x < positionPlayer)
        {
            EnterChargeState();
        }

    }
    private void EnterIdleState()
    {
        if(Charging)
        {
            SpeedX = 0.2f;
            SpeedY = Random.Range(-1.2f, 1.2f);
            Charging = false;
            anim.SetBool("Charging", false);
        }
     

    }
    private void EnterChargeState()
    {
        Debug.Log("Charge");
        if (!Charging)
        {
            SpeedX = Random.Range(3.5f, 4f);
            SpeedY = 0;
            Charging = true;
            anim.SetBool("Charging", true);
        }
    }
}
