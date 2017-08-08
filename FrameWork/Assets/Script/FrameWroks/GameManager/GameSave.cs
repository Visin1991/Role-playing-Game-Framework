using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSave : ScriptableObject {

    public bool isEmpty = true;

    public string sceneName;

    public LEData playerData;
}
