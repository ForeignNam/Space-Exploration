using UnityEngine;

public class FloatinSpace : MonoBehaviour
{

    private void Update()
    {
        float MoveX = GameManager.instance.adjustedWorldSpeed;
        transform.position += new Vector3(-MoveX, 0, 0);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }

}
