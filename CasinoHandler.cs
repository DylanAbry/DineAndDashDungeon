using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CasinoHandler : MonoBehaviour
{
    [SerializeField] public Animator animator = null;
    public GameObject transitionImage;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        transitionImage.SetActive(false);
    }

    public void LoadTitleScreen()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        transitionImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        animator.Play("CasinoFadeIn");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("DDDTitleScene");
    }

}
