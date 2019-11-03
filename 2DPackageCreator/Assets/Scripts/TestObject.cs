using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestObject : MonoBehaviour, ISaveObject
{
    public Transform[] slot;
    [SerializeField] int ID;
    [SerializeField] string playerName;
    [SerializeField] string currentScene;

    [Header("Runtime")]
    [SerializeField] float score;

    public string[] GetCurrentValues()
    {
        return new string[]
        {
            ID.ToString(),
            playerName,
            currentScene,
        };
    }

    public string[] GetKeys()
    {
        return new string[]
        {
            "ID",
            "PlayerName",
            "CurrentScene",
        };
    }

    public void LoadFromDefault()
    {
        ID = 1;
        playerName = "Danilo Domingues";
        Scene m_scene;
        m_scene = SceneManager.GetActiveScene();
        currentScene = m_scene.name;
    }

    public void LoadFromValues(string[] values)
    {
        int.TryParse(values[0], out ID);
        playerName = values[1];
        this.gameObject.transform.position = slot[ID].position;
        currentScene = values[2];
    }

    void Start()
    {

        
        
    }

    void Update()
    {
    }
  
}
