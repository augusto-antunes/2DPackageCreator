using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCars : MonoBehaviour, ISaveObject
{
    [SerializeField] int SIZE;
    [SerializeField] string BoxName;

    [Header("Runtime")]
    [SerializeField] float score;

    public string[] GetCurrentValues()
    {
        return new string[]
        {
            SIZE.ToString(),
            BoxName,
        };
    }

    public string[] GetKeys()
    {
        return new string[]
        {
            "SIZE",
            "BoxName",
        };
    }

    public void LoadFromDefault()
    {
        SIZE = 20589329;
        BoxName = "Danilo Domingues";
    }
    public void Start()
    {
        if(BoxName == "Salvo")
        {
            Debug.Log("Codigo");
        }
    }
    public void LoadFromValues(string[] values)
    {
        int.TryParse(values[0], out SIZE);
        BoxName = values[1];
    }
}
