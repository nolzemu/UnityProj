using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public string levelToLoad; // Имя сцены для загрузки
    public Vector3 position;
    public VectorValue playerStorage;
    public int requiredCoins = 9;

    private int collectedCoins = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadetoLevel()
    {
        anim.SetTrigger("Fade");
    }

    public void OnFadeComplete()
    {
        playerStorage.initialValue = position;
        StaticCoins.Coins = 0;
        // Загрузка сцены, указанной в переменной levelToLoad
        SceneManager.LoadScene(levelToLoad);
    }

    public void SetCollectedCoins(int coins)
    {
        collectedCoins = coins;
    }

    public bool CanProceedToNextLevel()
    {
        return collectedCoins >= requiredCoins;
    }
}
