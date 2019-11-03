using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour
{

    static SaveHandler instance;
    public SaveHandler Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SaveHandler>();
                if(instance == null)
                {
                    Debug.Log("Could not find Singleton instance, be sure to put one in the current scene.");
                }
            }

            return instance;
        }
    }

    public bool IsSavePresent
    {
        get { return PlayerPrefs.GetString("IsSavePresent") == true.ToString(); }
        set 
        { 
            PlayerPrefs.SetString("IsSavePresent", value.ToString());
            PlayerPrefs.Save();
        }
    }

    [SerializeField] ISaveObject[] saveObjects;
    [SerializeField] MonoBehaviour[] saveObjectsCopy;
    bool click;
    public GameObject E;
    public bool switcher;
    bool change;
    private void Update()
    {
        if (change && Input.GetKey(KeyCode.UpArrow))
        {
            SaveValues();
            SceneManager.LoadScene(scene);
            
        }
    }
    private void Awake() 
    {
        instance = this;
        change = false;
        var monos = FindObjectsOfType<MonoBehaviour>();
        monos = monos.Where(mono => mono is ISaveObject).ToArray();

        saveObjectsCopy = monos; 
        saveObjects = monos.Cast<ISaveObject>().ToArray();

        var savePresent = IsSavePresent;
        for (int i = 0; i < saveObjects.Length; i++)
        {
            if(savePresent)
            {
                saveObjects[i].LoadFromValues(GetValues(saveObjects[i].GetKeys()));
            }
            else
            {
                saveObjects[i].LoadFromDefault();    
            }
            
        }
        IsSavePresent = true;
    }

    void Start()
    {
    }
    /*void OnDestroy()
    {
        SaveValues();
    }*/

    string[] GetValues(string[] keys)
    {
        var results = new string[keys.Length];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = PlayerPrefs.GetString(keys[i]);
        }

        return results;
    }

    public void SaveValues()
    {
        string[] keys;
        string[] values;
        for (int i = 0; i < saveObjects.Length; i++)
        {
            keys = saveObjects[i].GetKeys();
            values = saveObjects[i].GetCurrentValues();

            for (int j = 0; j < keys.Length; j++)
            {
                PlayerPrefs.SetString(keys[j], values[j]);               
            }
        }

        PlayerPrefs.Save();
    }
    public string scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && switcher)
        {
            E.SetActive(true);
            change = true;
        }
        else
        {
            SaveValues();
            SceneManager.LoadScene(scene);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && switcher)
        {
            E.SetActive(false);
            change = false;
        }
    }

}
