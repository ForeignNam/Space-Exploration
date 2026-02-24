using UnityEngine;
using System.Collections.Generic;
public class LocustMorph : Enemy
{

    [SerializeField] private List<Frames> frames;
    private int variant;
    private bool isCharging ;
    public override void OnEnable()
    {
        base.OnEnable();
        variant = Random.Range(0, frames.Count);
        EnterIdle();
    }
    public override void Start()
    {
        base.Start();
        desenemyPooler = GameObject.Find("LocustPopPool").GetComponent<ObjectPooler>();
        hitsound = AudioManager.instance.locusthit;
        dessound = AudioManager.instance.locustdestroy;
        


    }
    public override void Update()
    {
        base.Update();
        if( transform.position.y > 4.5 || transform.position.y < -4.5)
        {
            SpeedY *= -1;
        }
    }
    private void EnterIdle()
    {
        isCharging = false;
        spriteRenderer.sprite = frames[variant].sprite[0];
        SpeedX = Random.Range(0.1f, 0.6f);
        SpeedY = Random.Range(-0.9f, 0.9f); ;
    }
    private void EnterCharge()
    {
        if (!isCharging)
        {
            isCharging = true;
            spriteRenderer.sprite = frames[variant].sprite[1];
            AudioManager.instance.PlaySound(AudioManager.instance.locustcharge);
            SpeedX = Random.Range(-4f, -6f);
            SpeedY = 0;
        }
        
    }
    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprite;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(lives < maxlives * 0.5f)
        EnterCharge();
    }
}
