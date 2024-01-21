using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookObject : MonoBehaviour
{
    public TextMeshProUGUI BookNameText;
    public TextMeshProUGUI WriterNameText;
    [SerializeField] private TextMeshProUGUI IsbnText;
    public TextMeshProUGUI BookCountText;
    [SerializeField] private Button Button;

    public int Isbn;


    public void SetString(string bookName, string writerName, int isbn, int bookCount)
    {
        BookNameText.text = bookName;
        WriterNameText.text = writerName;
        Isbn = isbn;
        IsbnText.text = Isbn.ToString();
        BookCountText.text = bookCount.ToString();
    }

    public void RemoveBook()
    {
        foreach (var item in Library.Instance.BookList)
        {
            if (item.ISBN == Isbn)
            {
                Library.Instance.BookList.Remove(item);
                BinaryF.Instance.SaveBookList();
                Destroy(gameObject);
                break;
            }
        }
    }
    public void TakeBook()
    {
        if (Library.Instance.UserBooks == null)
        {
            Library.Instance.Lists();
        }
        foreach (var item in Library.Instance.UserBooks)
        {
            if (item.ISBN == Isbn)
            {
                return;
            }
        }


        foreach (var item in Library.Instance.BookList)
        {
            if (Isbn == item.ISBN)
            {
                if (item.BookCount > 0)
                {
                    item.BookCount -= 1;
                    BookCountText.text = item.BookCount.ToString();
                    BinaryF.Instance.SaveBookList();
                    BinaryF.Instance.LoadBookList();
                    BinaryF.Instance.LoadUserList();

                    foreach (var item2 in Library.Instance.UserList)
                    {
                        if (item2.UserName == PlayerPrefs.GetString("UserName"))
                        {
                            if (Library.Instance.UserBooks == null)
                            {
                                Library.Instance.Lists();
                            }
                            UserBook userBook = new UserBook(item.BookName, item.WriterName, item.ISBN, DateTime.Now);
                            Library.Instance.UserBooks.Add(userBook);
                            item2.BookList = Library.Instance.UserBooks;
                            BinaryF.Instance.SaveUserList();
                            break;
                        }
                    }
                    break;
                }
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