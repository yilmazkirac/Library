using System;
using System.Collections.Generic;

[Serializable]
public class User 
{
    public string UserName;
    public string Password;
    public List<UserBook> BookList;
    public User(string username, string password)
    {
        UserName=username;
        Password=password;
    }
}
