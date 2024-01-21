using System;

[Serializable]
public class Book
{
    public string BookName;
    public string WriterName;
    public int ISBN;
    public int BookCount;   

    public Book(string bookname,string writername, int isbn, int bookcount) 
    { 
        BookName = bookname;
        WriterName = writername;
        ISBN = isbn;
        BookCount = bookcount;
    }
}
