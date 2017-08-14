using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager> {
    protected GameDataManager() { }

    [SerializeField]
    public ItemDataBaseList itemDatabase;

}
