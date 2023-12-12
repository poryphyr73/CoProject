using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Slot
{
    BACK,
    BELT,
    HAND
}
public class ContainerScriptable : ScriptableObject
{
    [SerializeField] public int _width { get; private set; }
    [SerializeField] public int _height { get; private set; }
    [SerializeField] public string _name { get; private set; }
    [SerializeField] public Slot _slot { get; private set; }
}
