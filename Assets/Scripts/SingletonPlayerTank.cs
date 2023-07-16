
using UnityEngine;

public class SingletonPlayerTank : GenericSingleton<SingletonPlayerTank>
{
    protected override void Awake()
    {
        base.Awake();
        InitializePlayerTank();

    }

    private void InitializePlayerTank()
    {
        Debug.Log("Player tank is Initialised");
    }

}
