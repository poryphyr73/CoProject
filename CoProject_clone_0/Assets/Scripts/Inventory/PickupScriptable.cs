using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScriptable : ScriptableObject
{
    [SerializeField] private int _stackSize { get; }
    [SerializeField] private int _width { get; }
    [SerializeField] private int _height { get; }
    [SerializeField] string _name { get; }
}
