using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private string adminName;
    private string adminpassword;
    [Header("SINGUP------------------")]
    [SerializeField] private TMP_InputField _singUpUserName;
    [SerializeField] private TMP_InputField _singUpPassword;
    [Header("INPUTS------------------")]
    [SerializeField] private TMP_InputField _userName;
    [SerializeField] private TMP_InputField _password;
    [Header("PANELS------------------")]
    [SerializeField] private GameObject _libraryAdminPanel;
    [SerializeField] private GameObject _libraryUserPanel;
    [SerializeField] private GameObject _singUpPanel;
    [SerializeField] private GameObject _loginPanel;
    [Header("ADMIN PANELS------------------")]
    [SerializeField] private GameObject _libraryBookPanel;
    [SerializeField] private GameObject _libraryAddBookPanel;
    [SerializeField] private GameObject _userGivenBookPanel;
    [Header("USER PANELS------------------")]
    [SerializeField] private GameObject _libraryBookPanel2;
    [SerializeField] private GameObject _userLibraryBookPanel;
    [Header("ADD BOOK--------------")]
    [SerializeField] private TMP_InputField _bookName;
    [SerializeField] private TMP_InputField _writerName;
    [SerializeField] private TMP_InputField _isbn;
    [SerializeField] private TMP_InputField _bookCount;

    [Header("CONTENTS--------------")]
    [SerializeField] private Transform _libraryBookPanelContent; 
    [SerializeField] private Transform _libraryBookPanelContent2;
    [SerializeField] private Transform _userBookContent;
    [SerializeField] private Transform _givenBooksContent;
    [Header("OBJECT--------------")]
    [SerializeField] private GameObject _bookPrefab;
    [SerializeField] private GameObject _myBookPrefab;
    [SerializeField] private GameObject _givenBook;
    [Header("SEARCH BOOK--------------")]
    [SerializeField] private TMP_InputField _searchTextUser;
    [SerializeField] private TMP_InputField _searchTextAdmin;
    [Header("WARNING TEXT--------------")]
    [SerializeField] private GameObject _warningSingUp;
    [SerializeField] private GameObject _warningLogin;
    [SerializeField] private GameObject _warningAddBook;

    private void Start()
    {
        _warningSingUp.SetActive(false);
        _warningLogin.SetActive(false);
        _warningAddBook.SetActive(false);
        adminName = "admin";
        adminpassword = "admin";
        ActivePanel(_loginPanel);
        ActiveLibraryPanels(_libraryBookPanel);
    }

    //Kullanici Giris Cikis Kayit Islemleri
    public void UserSave()
    {
        bool control = false;
        BinaryF.Instance.LoadUserList();
        foreach (var item in Library.Instance.UserList)
        {
            if (item.UserName == _singUpUserName.text)
                control = true;


            _warningSingUp.SetActive(true);
        }
        if (!control)
        {
            if (_singUpUserName.text!=""&& _singUpPassword.text != "")
            {
                User user = new User(_singUpUserName.text, _singUpPassword.text);
                Library.Instance.UserList.Add(user);
                BinaryF.Instance.SaveUserList();
                ActivePanel(_loginPanel);
                _singUpUserName.text = "";
                _singUpPassword.text = "";
                _warningSingUp.SetActive(false);
            }          
        }
    }
    public void Login()
    {
        bool control = false;
        if (adminName == _userName.text && adminpassword == _password.text)
        {
            AllBooksSearchForAdmin();
            ActivePanel(_libraryAdminPanel);
            ActiveLibraryPanels(_libraryBookPanel);
            _userName.text = "";
            _password.text = "";
            control = true;
        }
        if (!control)
        {
            BinaryF.Instance.LoadUserList();
            foreach (var item in Library.Instance.UserList)
            {
                if (item.UserName == _userName.text && item.Password == _password.text)
                {
                    AllBooksSearchForUser();
                    ActivePanel(_libraryUserPanel);
                    ActiveUserLibraryPanel(_libraryBookPanel2);
                    PlayerPrefs.SetString("UserName", _userName.text);
                    PlayerPrefs.SetString("Password", _password.text);
                    _userName.text = "";
                    _password.text = "";
                    Library.Instance.UserBooks = item.BookList;
                    _warningLogin.SetActive(false);
                }
            }
            _warningLogin.SetActive(true);
        }

    }
    public void Exit()
    {
        PlayerPrefs.SetString("UserName", "");
        PlayerPrefs.SetString("Password", "");
        Library.Instance.UserBooks.Clear();
    }

    //Panel Kontrolleri
    public void ActivePanel(GameObject panel)
    {
        _warningSingUp.SetActive(false);
        _warningLogin.SetActive(false);
        _singUpUserName.text = "";
        _singUpPassword.text = "";

        _libraryAdminPanel.SetActive(false);
        _loginPanel.SetActive(false);
        _libraryUserPanel.SetActive(false);
        _singUpPanel.SetActive(false);
        panel.SetActive(true);
    }
    public void ActiveLibraryPanels(GameObject panel)
    {
        _warningAddBook.SetActive(false);
        _bookName.text = "";
        _writerName.text = "";
        _isbn.text = "";
        _bookCount.text = "";

        _libraryBookPanel.SetActive(false);
        _libraryAddBookPanel.SetActive(false);
        _userGivenBookPanel.SetActive(false);
        panel.SetActive(true);
    }
    public void ActiveUserLibraryPanel(GameObject panel)
    {
        _libraryBookPanel2.SetActive(false);
        _userLibraryBookPanel.SetActive(false);
        panel.SetActive(true);
    }

    //Kitap Ekleme
    public void AddBook()
    {
        if (_bookName.text != "" && _writerName.text != "" && _isbn.text != "" && _bookCount.text != "")
        {
            bool control = false;
            foreach (var item in Library.Instance.BookList)
            {
                if (item.ISBN.ToString() == _isbn.text)
                {
                    control = true;
                    _warningAddBook.SetActive(true);
                    break;
                }
            }

            if (!control)
            {
                 int isbn;
                 int bookcount;
                 bool control1= Int32.TryParse(_isbn.text, out isbn);
                 bool control2 = Int32.TryParse(_bookCount.text, out bookcount);
                if (control1 && control2)
                {
                    GameObject newBook = Instantiate(_bookPrefab, _libraryBookPanelContent);
                    newBook.GetComponent<BookObject>().SetButton("DEL", newBook.GetComponent<BookObject>().RemoveBook);
                    newBook.GetComponent<BookObject>().SetString(_bookName.text, _writerName.text, isbn, bookcount);
                    Book book = new Book(_bookName.text, _writerName.text, isbn, bookcount);
                    Library.Instance.BookList.Add(book);
                    BinaryF.Instance.SaveBookList();
                    _warningAddBook.SetActive(false);

                    _bookName.text = "";
                    _writerName.text = "";
                    _isbn.text = "";
                    _bookCount.text = "";
                }
            }
        }
    }      

    //Admin Sorgulari
    public void BooksSearchForAdmin()
    {
        for (int i = 0; i < _libraryBookPanelContent.childCount; i++)
        {
            if (_libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().BookNameText.text == _searchTextAdmin.text || _libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().WriterNameText.text == _searchTextAdmin.text)
            {
                _libraryBookPanelContent.GetChild(i).gameObject.SetActive(true);
            }
            else if (_searchTextAdmin.text == "")
            {
                _libraryBookPanelContent.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _libraryBookPanelContent.GetChild(i).gameObject.SetActive(false);
            }
        }
        _searchTextAdmin.text = "";
    }
    public void AllBooksSearchForAdmin()
    {
        BinaryF.Instance.LoadBookList();
        foreach (var item in Library.Instance.BookList)
        {
            bool control = false;

            for (int i = 0; i < _libraryBookPanelContent.childCount; i++)
            {
                if (_libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().Isbn == item.ISBN)
                {
                    if (!_libraryBookPanelContent.GetChild(i).gameObject.activeInHierarchy)
                    {
                        _libraryBookPanelContent.GetChild(i).gameObject.SetActive(true);
                    }
                    if (_libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().Isbn == item.ISBN)
                    {
                        if (_libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().BookCountText.text != item.BookCount.ToString())
                        {
                            _libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().BookCountText.text = item.BookCount.ToString();
                            // Destroy(_bookCaseUsers.GetChild(i).gameObject);
                        }
                        control = true;
                        break;
                    }                                  
                }
            }
            if (!control)
            {
                GameObject newBook = Instantiate(_bookPrefab, _libraryBookPanelContent);
                newBook.GetComponent<BookObject>().SetButton("DEL", newBook.GetComponent<BookObject>().RemoveBook);
                newBook.GetComponent<BookObject>().SetString(item.BookName, item.WriterName, item.ISBN, item.BookCount);
            }
        }


        for (int i = 0; i < _libraryBookPanelContent.childCount; i++)
        {
            bool control = false;
            foreach (var item in Library.Instance.BookList)
            {
                if (_libraryBookPanelContent.GetChild(i).GetComponent<BookObject>().Isbn == item.ISBN)
                {
                    control = true;
                    break;
                }
            }
            if (!control)
            {
                Destroy(_libraryBookPanelContent.GetChild(i).gameObject);
            }
        }
    }

    //Kullanici Sorgulari
    public void BooksSearchForUser()
    {
        for (int i = 0; i < _libraryBookPanelContent2.childCount; i++)
        {
            if (_libraryBookPanelContent2.GetChild(i).GetComponent<BookObject>().BookNameText.text == _searchTextUser.text || _libraryBookPanelContent2.GetChild(i).GetComponent<BookObject>().WriterNameText.text == _searchTextUser.text)
            {
                _libraryBookPanelContent2.GetChild(i).gameObject.SetActive(true);
            }
            else if (_searchTextUser.text == "")
            {
                _libraryBookPanelContent2.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                _libraryBookPanelContent2.GetChild(i).gameObject.SetActive(false);
            }
        }
        _searchTextUser.text = "";
    }
    public void AllBooksSearchForUser()
    {
        BinaryF.Instance.LoadBookList();
        foreach (var item in Library.Instance.BookList)
        {
            bool control = false;

            for (int i = 0; i < _libraryBookPanelContent2.childCount; i++)
            {
                if (!_libraryBookPanelContent2.GetChild(i).gameObject.activeInHierarchy)
                {
                    _libraryBookPanelContent2.GetChild(i).gameObject.SetActive(true);
                }
                if (_libraryBookPanelContent2.GetChild(i).GetComponent<BookObject>().Isbn == item.ISBN)
                {
                    if (_libraryBookPanelContent2.GetChild(i).GetComponent<BookObject>().BookCountText.text != item.BookCount.ToString())
                    {
                        _libraryBookPanelContent2.GetChild(i).GetComponent<BookObject>().BookCountText.text=item.BookCount.ToString();
                    }                   
                       control = true;
                       break;                    
                }
            }
            if (!control)
            {
                GameObject newBook = Instantiate(_bookPrefab, _libraryBookPanelContent2);
                newBook.GetComponent<BookObject>().SetButton("TAKE", newBook.GetComponent<BookObject>().TakeBook);
                newBook.GetComponent<BookObject>().SetString(item.BookName, item.WriterName, item.ISBN, item.BookCount);
            }
        }


        for (int i = 0; i < _libraryBookPanelContent2.childCount; i++)
        {
            bool control = false;
            foreach (var item in Library.Instance.BookList)
            {
                if (_libraryBookPanelContent2.GetChild(i).GetComponent<BookObject>().Isbn == item.ISBN)
                {
                    control = true;
                    break;
                }
            }
            if (!control)
            {
                Destroy(_libraryBookPanelContent2.GetChild(i).gameObject);
            }
        }
    }
    public void AllMyBooks()
    {
        BinaryF.Instance.LoadUserList();


        foreach (var item in Library.Instance.UserList)
        {
            if (item.UserName == PlayerPrefs.GetString("UserName"))
            {
                Library.Instance.UserBooks = item.BookList;              
                break;
            }
        }

        if (Library.Instance.UserBooks == null)
        {
            Library.Instance.Lists();
        }
        foreach (var item in Library.Instance.UserBooks)
            {
                bool control = false;

                for (int i = 0; i < _userBookContent.childCount; i++)
                {
                    if (_userBookContent.GetChild(i).GetComponent<MYBookObject>().Isbn == item.ISBN)
                    {
                        control = true;
                        break;
                    }
                }
                if (!control)
                {
                    GameObject newBook = Instantiate(_myBookPrefab, _userBookContent);
                    newBook.GetComponent<MYBookObject>().SetButton("RETURN", newBook.GetComponent<MYBookObject>().Return);
                    newBook.GetComponent<MYBookObject>().SetString(item.BookName, item.WriterName, item.ISBN,item.DateTaken, item.LastReturnDate);
                }
            }
        
            

        for (int i = 0; i < _userBookContent.childCount; i++)
        {
            bool control = false;
            foreach (var item in Library.Instance.UserBooks)
            {
                if (_userBookContent.GetChild(i).GetComponent<MYBookObject>().Isbn == item.ISBN)
                {
                    control = true;
                    break;
                }
            }
            if (!control)
            {
                Destroy(_userBookContent.GetChild(i).gameObject);
            }
        }
    }    

    //Verilen Kitam Sorgulari
    public void AllBooksGiven()
    {
        BinaryF.Instance.LoadUserList();
       
            Library.Instance.Lists();
        
        if (Library.Instance.UserBooks == null)
        {
            return;
        }
        foreach (var item in Library.Instance.UserList)
        {      

            Library.Instance.UserBooks = item.BookList;
      
            foreach (var item2 in Library.Instance.UserBooks)
            {
                bool control = false;

                for (int i = 0; i < _givenBooksContent.childCount; i++)
                {
                    if (!_givenBooksContent.GetChild(i).gameObject.activeInHierarchy)
                    {
                        _givenBooksContent.GetChild(i).gameObject.SetActive(true);
                    }
                    if (_givenBooksContent.GetChild(i).GetComponent<BookDateObject>().IsbnText.text == item2.ISBN.ToString()&& _givenBooksContent.GetChild(i).GetComponent<BookDateObject>().UserNameText.text == item.UserName)
                    {
                        control = true;
                        break;
                    }
                }
                if (!control)
                {                 
                    GameObject newBook = Instantiate(_givenBook, _givenBooksContent);
                    newBook.GetComponent<BookDateObject>().SetString(item2.BookName, item2.WriterName, item2.ISBN, item.UserName, item2.DateTaken, item2.LastReturnDate);
                    if (item2.LastReturnDate < DateTime.Now)
                    {
                        newBook.GetComponent<Image>().color = Color.red;
                    }
                }            
            }      
        } 
    }
    public void BooksThatHaveExpired()
    {      

        for (int i = 0; i < _givenBooksContent.childCount; i++)
        {
            if (_givenBooksContent.GetChild(i).GetComponent<BookDateObject>().LastReturnDate < DateTime.Now)
            {
                _givenBooksContent.GetChild(i).gameObject.SetActive(true);
            }         
            else
            {
                _givenBooksContent.GetChild(i).gameObject.SetActive(false);
            }
        }           

    }

    // QUIT
    public void AppQuit()
    { 
        Application.Quit();
    }
}
