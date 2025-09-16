using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] public Animator animator = null;
    [SerializeField] public Animator transition = null;
    public GameObject transitionImage;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1.5f);
        transitionImage.SetActive(false);
        yield return new WaitForSeconds(4f);
        animator.Play("GameOverAnimation");
    }

    public void LoadTitleScreen()
    {
        transitionImage.SetActive(true);
        transition.Play("FadeIn");
        StartCoroutine(WaitTitleScreen());
    }
    private IEnumerator WaitTitleScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("DDDTitleScene");
    }
}
