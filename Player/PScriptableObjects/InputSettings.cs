using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/InputSettings", fileName = "InputSetting")]
public class InputSettings : ScriptableObject
{
    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string jump;
    [SerializeField] private string attack;

    public string Horizontal { get { return horizontal; } }
    public string Vertical{ get { return vertical; } }
    public string Jump { get { return jump; } }
    public string Attack { get { return attack; } }
}
