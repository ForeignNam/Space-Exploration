using UnityEngine;

public class Crutter1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] crutter1Sprite;
     private ObjectPooler CritterZappedPool;
     private ObjectPooler CritterburnedPool;

    private Vector3 targetposition;
    private float movespeed;
    private Quaternion targetrotation;

    private float timer;
    private float moveinterval;
    void Start()
    {
        CritterburnedPool = GameObject.Find("Critter1_BumPool").GetComponent<ObjectPooler>();
        CritterZappedPool = GameObject.Find("Critter1_ZappedPool").GetComponent<ObjectPooler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = crutter1Sprite[Random.Range(0, crutter1Sprite.Length)];
        movespeed = Random.Range(0.5f, 3f);
        moveinterval = Random.Range(0.5f, 2f);
        timer = moveinterval;
        Generatetargetposition();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }
        else
        {
            Generatetargetposition();
            moveinterval = Random.Range(0.3f, 2f);
            timer = moveinterval;
        }
        targetposition -= new Vector3(GameManager.instance.WorldSpped * Time.deltaTime, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetposition, movespeed * Time.deltaTime);

        Vector3 diff = targetposition - transform.position;
        if (diff != Vector3.zero)
        {
            targetrotation = Quaternion.LookRotation(Vector3.forward, diff);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetrotation, 1080 * Time.deltaTime);

        }
    
    }

    private void Generatetargetposition()
    {
        float targetX = Random.Range(-5f, 5f);
        float targetY = Random.Range(-5f, 5f);
        targetposition = new Vector2(targetX, targetY);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            GameObject zappedEffect = CritterZappedPool.GetPooledObject();
            zappedEffect.transform.position = transform.position;
            zappedEffect.transform.rotation = transform.rotation;
            zappedEffect.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.instance.PlayModifiedSound(AudioManager.instance.Squished);
            GameManager.instance.CritterCount++;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameObject BurnEffect = CritterburnedPool.GetPooledObject();
            BurnEffect.transform.position = transform.position;
            BurnEffect.transform.rotation = transform.rotation;
            BurnEffect.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.instance.PlayModifiedSound(AudioManager.instance.burn);
            GameManager.instance.CritterCount++;
        }
    }
}
