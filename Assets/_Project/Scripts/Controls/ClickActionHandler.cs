using System;
using UnityEngine;

public class ClickActionHandler : MonoBehaviour
{
    public static event Action<TileClickActionType> ChangedActionType;
    public static TileClickActionType ActionType { get; private set; }
    
    public static void SetActionType(TileClickActionType actionType)
    {
        ActionType = actionType;
        ChangedActionType?.Invoke(actionType);
    }
}