using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static SaveData saveData;
    public GameObject menu;
    public GameObject menu2;

    string SavePath => Path.Combine(Application.persistentDataPath, "save.data"); 
    private void Awake() 
    { 
        if (saveData == null) 
        { 
            Load(); 
        }
        else
        {
            Save(); 
        } 
    }

    private void Load()
    {
        FileStream file = null; 
        try 
        { 
            file = File.Open(SavePath, FileMode.Open); 
            var bf = new BinaryFormatter(); 
            saveData = bf.Deserialize(file) as SaveData; 
        } 
        catch (Exception e) 
        { 
            Debug.Log(e.Message); saveData = new SaveData(); 
        }
        finally 
        { 
            if (file != null) file.Close(); 
        }
    }
    private void Save()
    {
        FileStream file = null; 
        try 
        { 
            if (!Directory.Exists(Application.persistentDataPath)) Directory.CreateDirectory(Application.persistentDataPath); 
            file = File.Create(SavePath); 
            var bf = new BinaryFormatter(); 
            bf.Serialize(file, saveData); 
        } 
        catch (Exception e) 
        { 
            Debug.Log(e); 
        } 
        finally 
        { 
            if (file != null) file.Close(); 
        }
    }


    // Start is called before the first frame update
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnUpgradeClick()
    {
        Debug.Log("TODO NEXT WEEK");
        menu.SetActive(true);

    }

    public void OnCharacterSelectionClick()
    {
        menu2.SetActive(true);
    }

    public void OnQuitButtonClick()
    {
        
    }

    public void OnRetryClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("Title");
    }


    public void OnCharacter1Click()
    {
        saveData.IsWhiteUnlocked = true;
        saveData.IsBlackUnlocked = false;
        menu2.SetActive(false);
    }

    public void OnCharacter2Click()
    {
        saveData.IsWhiteUnlocked = false;
        saveData.IsBlackUnlocked = true;
        menu2.SetActive(false);

    }

    public void OnUpgrade1Click()
    {
        if (saveData.orbCount >= 1)
        {
            saveData.orbCount -= 1;
            saveData.Upgrade1 = true;
        }
        menu.SetActive(false);

    }

    public void OnUpgrade2Click()
    {
        if (saveData.orbCount >= 100)
        {
            saveData.orbCount -= 100;
            saveData.Upgrade2 = true;
        }
        menu.SetActive(false);
    }

}
