using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDisable : MonoBehaviour
{
    public GameObject[] Coins;

    private void Start()
    {
        for (int i = 0; i < Coins.Length; i++)
        {
           int num = Random.Range(0, Coins.Length);

            if (num == 2 || num == 3)
            {
                Coins[i].SetActive(true);
            }
        }
    }
}
