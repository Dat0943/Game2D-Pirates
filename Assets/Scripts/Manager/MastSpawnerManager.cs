using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mastPrefab;
    [SerializeField] private Transform container, lastMast;

    void Update()
    {
        if (GameManager.Ins.isStart)
        {
            if (Vector2.Distance(Vector2.zero, new Vector2(0, lastMast.position.y)) < 6)
            {
                int mastSpawnIndex = Random.Range(0, mastPrefab.Length);
                GameObject spawnMast = Instantiate(mastPrefab[mastSpawnIndex], new Vector2(0f, lastMast.GetChild(0).position.y), Quaternion.identity);
                spawnMast.transform.parent = container;
                lastMast = spawnMast.transform;

                if(spawnMast == mastPrefab[0] || spawnMast == mastPrefab[2] || spawnMast == mastPrefab[3])
                {
                    // Hiện ngẫu nhiên các coinInGame
                    int coinCount = Random.Range(0, lastMast.GetChild(2).childCount);
                    int lastPack = Random.Range(0, lastMast.GetChild(2).GetChild(coinCount).childCount);

                    if (lastPack > 0)
                    {
                        for (int i = 0; i < lastPack; i++)
                        {
                            int whichCoin = Random.Range(0, lastMast.GetChild(2).childCount);
                            int lastPacks = Random.Range(0, lastMast.GetChild(2).GetChild(whichCoin).childCount);
                            GameObject lastPackPack = lastMast.GetChild(2).GetChild(whichCoin).GetChild(lastPacks).gameObject;
                            lastPackPack.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
