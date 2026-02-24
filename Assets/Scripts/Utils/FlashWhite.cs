using UnityEngine;
using System.Collections;
public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material normalMaterial;
    private Material WhiteMaterial;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalMaterial = spriteRenderer.material;
        WhiteMaterial = Resources.Load<Material>("Materials/White");
    }

   public void Flash()
   {
        spriteRenderer.material = WhiteMaterial;
        StartCoroutine(ResetMaterial());
   }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = normalMaterial;
    }
    public void Reset()
    {
        spriteRenderer.material = normalMaterial;
    }
}

