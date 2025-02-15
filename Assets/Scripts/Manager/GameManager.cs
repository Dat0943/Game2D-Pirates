using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Ship")]
    [SerializeField] private Rigidbody2D shipRb;
    float shipFallSpeed;

    [Header("Player: ")]
    PlayerController player;

    [Header("Enemy")]
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private float spawnMaxTime = 7f;
    [SerializeField] private float spawnMinTime = 5f;

    [Header("Map")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SwipeController swipeController;
    [SerializeField] private Sprite[] sprite;
    int currentMap;

    [Header("Coin")]
    int coinInGame;
    int coinCounting;
    public int CoinCounting { get => coinCounting; set => coinCounting = value; }
    public int CoinInGame { get => coinInGame; set => coinInGame = value; }
    public PlayerController Player { get => player; set => player = value; }

    [Header("Gift")]
    DateTime nextGiftTime;

    public bool isStart;
    public bool isDie;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    void Start()
    {
        GuiManager.Ins.ShowGamePanel(false);
        coinCounting = Prefs.CoinData;
        GuiManager.Ins.UpdateCoinCounting(coinCounting);

        currentMap = Prefs.MapData;
        SelectMap();
    }

    void Update()
    {
        if (isStart)
        {
            shipFallSpeed = Mathf.MoveTowards(TagConsts.SHIP_MIN_SPEED, TagConsts.SHIP_MAX_SPEED, 0.1f * Time.deltaTime);
            shipRb.velocity = new Vector2(0, -shipFallSpeed);
        }
        
        if (!isStart && isDie)
        {
            shipRb.velocity = Vector2.zero; 
            shipRb.isKinematic = true; 
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        GuiManager.Ins.ShowGamePanel(true);
        ActivePlayer();
        SelectMap();
        ItemManager.Ins.UpdateItemText();
        shipRb.gravityScale = 1;
        isStart = true;
        StartCoroutine(SpawnEnemyCoroutine());
    }

    void ActivePlayer()
    {
        if (player)
            Destroy(player.gameObject);

        PlayerController newPlayerPb = PlayerManager.Ins.players[Prefs.CurPlayerId].playerPb; // Lấy ra đưuọc thằng player hiện tại đang Active

        if (newPlayerPb)
            player = Instantiate(newPlayerPb, newPlayerPb.transform.position, Quaternion.identity);
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        //yield return new WaitForSeconds(5f);

        while (true)
        {
            SpawnEnemy(); 
            yield return new WaitForSeconds(UnityEngine.Random.Range(spawnMinTime, spawnMaxTime)); 
        }
    }

    void SpawnEnemy()
    {
        if (enemies.Length == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, enemies.Length); 
        Enemy enemy = Instantiate(enemies[randomIndex], enemies[randomIndex].transform.position, Quaternion.identity); 
    }

    #region Select Map
    void SelectMap()
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            if(currentMap == i + 1)
            {
                background.sprite = sprite[i];
            }
        }
    }

    // Sự kiện Button
    public void SaveMap()
    {
        currentMap = swipeController.currentPage;
        Prefs.MapData = currentMap;
        PlayerPrefs.Save();
        SelectMap();
    }
    #endregion

    public void EndGame()
    {
        Prefs.CoinData = coinCounting;
        GuiManager.Ins.ShowGameoverDialog();
        isDie = true;
    }

    #region Coin
    public void UpdateCoin()
    {
        if(player.IsX2CoinMode == false)
        {
            coinInGame++;
            GuiManager.Ins.UpdateCoinInGame(coinInGame);
            AddCoin(1);
        }
        else if(player.IsX2CoinMode == true)
        {
            coinInGame += 2;
            GuiManager.Ins.UpdateCoinInGame(coinInGame);
            AddCoin(2);
        }

        
    }

    public void AddCoin(int coin)
    {
        coinCounting += coin;
        Prefs.CoinData = coinCounting;
    }
    #endregion

    public int GetDistanceClimbed()
    {
        return (int)(0 - shipRb.gameObject.transform.position.y) / 10;
    }

    public void PauseGame()
    {
        Time.timeScale = 0; 
    }
}
