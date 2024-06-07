using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public string levelToLoad; // ��� ����� ��� ��������
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
        // �������� �����, ��������� � ���������� levelToLoad
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
