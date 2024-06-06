using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public int levelToLoad;
    public Vector3 position;
    public VectorValue playerStorage;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [System.Obsolete]
    public void FadetoLevel()
    {
        anim.SetTrigger("Fade");
    }
    public void OnFadeComplete()
    {
        playerStorage.initialValue = position;
        SceneManager.LoadScene("2lvl");
    }
}
