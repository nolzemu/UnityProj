using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticCoins
{
    public static int Coins;
    static StaticCoins()
    {
        Coins = 0;
    }
    public static void IncreaseCoins()
    {
        Coins++;

    }
}