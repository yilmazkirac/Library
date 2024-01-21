using System;
using TMPro;
using UnityEngine;
public class BookDateObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BookNameText;
    [SerializeField] private TextMeshProUGUI WriterNameText;
    public TextMeshProUGUI IsbnText;
    public TextMeshProUGUI UserNameText;
    [SerializeField] private TextMeshProUGUI DateTakenText;
    [SerializeField] private TextMeshProUGUI LastReturnDateText;

  
    public DateTime LastReturnDate;
    public void SetString(string bookName, string writerName, int isbn,string userName,DateTime dateTaken, DateTime lastReturnDate)
    {
        BookNameText.text = bookName;
        WriterNameText.text = writerName;
        IsbnText.text = isbn.ToString();
        UserNameText.text = userName;
        DateTakenText.text= dateTaken.ToString();
        LastReturnDate = lastReturnDate;
        LastReturnDateText.text= lastReturnDate.Date.ToString();
    }
}