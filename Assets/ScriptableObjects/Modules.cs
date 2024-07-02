using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModulesList", menuName = "Scriptable Objects/Modules List")]
public class Modules : ScriptableObject
{
    public List<GameObject> basic;
    public List<GameObject> impediments;
    public List<GameObject> hardModules;
}