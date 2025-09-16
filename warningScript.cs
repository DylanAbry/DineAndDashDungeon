using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warningScript : MonoBehaviour
{
    [SerializeField] public Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WarningSpan());
    }

    private IEnumerator WarningSpan()
    {
        yield return new WaitForSeconds(5f);
        animator.Play("WarningFadeIn");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("DDDTitleScene");
    }
}
