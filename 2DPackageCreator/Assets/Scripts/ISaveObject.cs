using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveObject
{
    string[] GetKeys();
    string[] GetCurrentValues();
    void LoadFromValues(string[] values);
    void LoadFromDefault();
}
