using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyText : MonoBehaviour
{
    public Text text;

    void Update()
    {
        text.text = StaticCoins.Coins.ToString();        

    }

}
