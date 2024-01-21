using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryF : MonoBehaviour
{
    public static BinaryF Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {       
        if (!File.Exists(Application.persistentDataPath + "/Users.txt"))
        {
            SaveUserList();
        }
        if (!File.Exists(Application.persistentDataPath + "/Books.txt"))
        {
            SaveBookList();
        }
    }    
    public void SaveBookList()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/Books.txt", FileMode.Create);
        bf.Serialize(file, Library.Instance.BookList);
        file.Close();
    }
    public void LoadBookList()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Books.txt", FileMode.Open);
        Library.Instance.BookList = (List<Book>)bf.Deserialize(file);
        file.Close();
    }


    public void SaveUserList()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/Users.txt", FileMode.Create);
        bf.Serialize(file, Library.Instance.UserList);
        file.Close();
    }
    public void LoadUserList()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Users.txt", FileMode.Open);
        Library.Instance.UserList = (List<User>)bf.Deserialize(file);
        file.Close();
    }
}
