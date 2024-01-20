using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankService : GenericSingleton<TankService> 
{
    private TankType playerTank;
    [SerializeField] int EnemyTankSpawnCount;
    string enemyCount_PP = "enemyCount";
    [SerializeField] LevelManager levelManager;
    public List<EnemyTankController> EnemyTanksControllers { get; private set; } = new List<EnemyTankController>();
    [SerializeField] private TankTypes tanks;
    public Coroutine destroyAll;
    public int destroyedEnemyTanks { private get; set; } = 0;

    public Follower playerTankFollower;
    public PlayerTankController playerTankController;
    public Action onLevelCleared;

    public event Action<int> distanceMilestoneCover;
    public event Action<int> onKilled;
    public event Action onGameOver;
    protected override void Start()
    {
        StartGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        StartGame();
    }

    //public TankView getPlayerTankView() => playerTank.tankview;
    void StartGame()
    {
        if (playerTankController != null) return;
        playerTankController = CreatePlayerTank();

        EnemyTankSpawnCount = (int)PlayerPrefs.GetFloat(enemyCount_PP);
        CreateEnemyTanks();
    }

    public void ServiceLaunchBullet(BulletType _bulletTyoe, TransformSet _launch)
    {
        BulletService.Instance.FireBullet(_bulletTyoe, _launch);
    }

    Vector3 getBulletSpeed()
    {
        return Vector3.zero;
    }

    public void destroyAllTanks()
    {
        destroyAll = StartCoroutine(destroyAllEnemies());
        levelManager.destroyWorld();
    }

    private IEnumerator destroyAllEnemies()
    {
        foreach (EnemyTankController tank in EnemyTanksControllers)
        {
            if (tank != null)
            {
                tank?.DestroyTank();
                yield return tank.tankView.destroyThis;
            }
            else
            {
                yield return null;
            }
            
        }
        EnemyTanksControllers.Clear();
    }

    private TankType chooseRandomTank()
    {
        int n = Random.Range(0, tanks.Types.Count);
        return tanks.Types[n];    
    }
    private PlayerTankController CreatePlayerTank()
    {
        playerTank = chooseRandomTank();
        TankModel tankModel = new TankModel(playerTank.speed, playerTank.health, playerTank.bulletType);
        PlayerTankController tankController = new PlayerTankController(tankModel, playerTank.tankview);
        return tankController;
    }
    private void CreateEnemyTanks()
    {
        //EnemyTanks.Clear();
        //EnemyTanksControllers.Clear();
        for (int i = 0; i < EnemyTankSpawnCount; i++)
        {
            TankType randomTankType = chooseRandomTank();
            TankModel tankModel = new TankModel(randomTankType.speed, randomTankType.health, randomTankType.bulletType);
            EnemyTankController tankController = new EnemyTankController(tankModel, randomTankType.tankview);
            EnemyTanksControllers.Add(tankController);
        }
    }

    public void checkDistanceAcheivement(int distance)
    {
        distanceMilestoneCover?.Invoke(distance);
    }

    public void killIncrementer()
    {
        destroyedEnemyTanks+=1;
        onKilled?.Invoke(destroyedEnemyTanks);
    }

    public void gameOverProcess()
    {
        onGameOver?.Invoke();
    }
}
