using UnityEngine;

public class BeetleMorph : Enemy
{
    [SerializeField] private Sprite[] morphSprites;
    private float Timer;
    private float frequency ;
    private float amplitude;
    private float centerY;
    public override void OnEnable()
    {
        base.OnEnable();
        Timer = transform.position.y;
        frequency = Random.Range(0.3f, 1f);

        amplitude = Random.Range(0.8f, 1.5f); ;
        centerY = transform.position.y;
    }
    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = morphSprites[Random.Range(0, morphSprites.Length)];
        desenemyPooler = GameObject.Find("BeetlePopPool").GetComponent<ObjectPooler>();
        hitsound = AudioManager.instance.beetlehit;
        dessound = AudioManager.instance.beetledestroy;
        SpeedX = Random.Range(-0.8f, 1.5f);
    }

    public override void Update()
    {
        base.Update();
        Timer -= Time.deltaTime;
        float sineWave = Mathf.Sin(Timer * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, centerY+  sineWave);
    }
}
