using UnityEngine;
using System.Collections;
public class DestroyWhenAnimationFinish : MonoBehaviour
{
    private Animator animator;
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DestroyAfterAnimation());

    }
    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
