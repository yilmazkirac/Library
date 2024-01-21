using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    public static Library Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }       
    }
    public List<Book> BookList;
    public List<User> UserList;
    public List<UserBook> UserBooks;
    public void Lists()
    {
        UserBooks = new List<UserBook> ();
    }
}
