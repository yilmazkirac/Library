using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MYBookObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BookNameText;
    [SerializeField] private TextMeshProUGUI WriterNameText;
    [SerializeField] private TextMeshProUGUI IsbnText;
    [SerializeField] private TextMeshProUGUI DateTakenText;
    [SerializeField] private TextMeshProUGUI LastReturnDateText;
    [SerializeField] private Button Button;

    public int Isbn;

    [SerializeField] private DateTime DateTaken;
    

    public void SetString(string bookName, string writerName, int isbn,DateTime dateTaken, DateTime lastReturnDate)
    {
        BookNameText.text = bookName;
        WriterNameText.text = writerName;
        Isbn = isbn;
        IsbnText.text = Isbn.ToString();
        DateTaken = dateTaken;
        DateTakenText.text= dateTaken.ToString();
        LastReturnDateText.text= lastReturnDate.Date.ToString();
    }
   
    public void Return()
    {
        BinaryF.Instance.LoadBookList();

        foreach (var item in Library.Instance.BookList)
        {
            if (Isbn == item.ISBN)
            {
                item.BookCount += 1;
                BinaryF.Instance.SaveBookList();
                BinaryF.Instance.LoadBookList();
                BinaryF.Instance.LoadUserList();
                foreach (var item2 in Library.Instance.UserList)
                {
                    if (item2.UserName == PlayerPrefs.GetString("UserName"))
                    {
                        foreach (var item3 in Library.Instance.UserBooks)
                        {
                            if (item3.ISBN == Isbn)
                            {
                                Library.Instance.UserBooks.Remove(item3);
                                Destroy(gameObject);
                                item2.BookList = Library.Instance.UserBooks;
                                BinaryF.Instance.SaveUserList();                               
                                break;
                            }                          
                        }
                        break;
                    }                                     
                }
                break;
            }          
        }
    }

    public void SetButton(string text, Action action = null)
    {
        Button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        Button.GetComponent<Button>().onClick.RemoveAllListeners();
        Button.GetComponent<Button>().onClick.AddListener(() => { action(); });
    }
}