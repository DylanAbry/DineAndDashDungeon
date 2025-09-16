using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseHandler : MonoBehaviour
{

    public GameObject transitionImage;
    [SerializeField] public Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(WaitToDisableTransition());
    }

    public void LoadTitleScreen()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator WaitToDisableTransition()
    {
        yield return new WaitForSeconds(1.1f);
        transitionImage.SetActive(false);
    }

    private IEnumerator Wait()
    {
        transitionImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        animator.Play("HouseFadeIn");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("DDDTitleScene");
    }
}
