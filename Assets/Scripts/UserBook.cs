using System;

[Serializable]
public class UserBook
{
    public string BookName;
    public string WriterName;
    public int ISBN;

    public DateTime DateTaken;
    public DateTime LastReturnDate;
    public UserBook(string bookname, string writername, int isbn,DateTime dateTaken)
    {
        BookName = bookname;
        WriterName = writername;
        ISBN = isbn;
        DateTaken = dateTaken;
        LastReturnDate = dateTaken.AddDays(1).Date;
    }
}
