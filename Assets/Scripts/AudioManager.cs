using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;
    public AudioSource ice;
    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource boom2;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource Squished;
    public AudioSource burn;
    public AudioSource hitArmor;
    public AudioSource bossCharge;
    public AudioSource bossspawn;
    public AudioSource beetlehit;
    public AudioSource beetledestroy;
    public AudioSource locusthit;
    public AudioSource locustdestroy;
    public AudioSource locustcharge;
   
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);

        }
        else
            instance = this;
    }
    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();


    }
    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.8f, 1.2f);   
        sound.Stop();
        sound.Play();


    }

}
