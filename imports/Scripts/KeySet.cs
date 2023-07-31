using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represennts a set of keys that one player will use. Each player will have their own key set.
/// </summary>
[CreateAssetMenu(fileName = "Key Set", menuName = "Custom Objects/Create Key Set", order = 1)]
public class KeySet : ScriptableObject
{
    public KeySet()
    {

    }

    public KeySet(KeyCode leftKey, KeyCode rightKey, KeyCode upKey, KeyCode downKey, KeyCode actionKey)
    {
        LeftKey = leftKey;
        RightKey = rightKey;
        UpKey = upKey;
        DownKey = downKey;
        ActionKey = actionKey;
    }

    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode ActionKey;
}
