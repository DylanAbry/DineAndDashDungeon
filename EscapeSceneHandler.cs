using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeSceneHandler : MonoBehaviour
{
    [SerializeField] public Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToPlayFadeInAndLoadScene());
    }

    private IEnumerator WaitToPlayFadeInAndLoadScene()
    {
        // Wait for 8 seconds before starting the fade-in animation
        yield return new WaitForSeconds(6.5f);

        // Play the fade-in animation
        animator.Play("FadeIn");

        // Wait for the length of the fade-in animation before transitioning
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // After the animation finishes, load the next scene
        SceneManager.LoadSceneAsync("HomeEndingScene");
    }
}
