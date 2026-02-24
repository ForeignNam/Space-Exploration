using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float movespeed;
    private float BackGroundimagewidth;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        BackGroundimagewidth = sprite.texture.width / sprite.pixelsPerUnit;

    }


    void Update()
    {
        float MoveX = (movespeed) * Time.deltaTime;

        transform.position += new Vector3(MoveX, 0, 0);
        if (Mathf.Abs(transform.position.x) - BackGroundimagewidth > 0)
        {
            transform.position = new Vector3(0, transform.position.y);
        }
    }
}
