using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MurvasBokhandel.Models
{
    public class Mockup
    {
        public class BOOK {
            public int ISBN, SignId,
                       PublicationYear;
            public string Title, publicationinfo, pages;
        }
        public class BOOK_AUTHOR {
            public int ISBN;
            public int Aid;
        }
        public class AUTHOR {
            public int Aid, BirthYear;
            public string FirstName, LastName;
        }
        public class COPY {
            public int Barcode, StatusId, ISBN;
            public string Location, library;
        }
        public class STATUS {
            public int statusid;
            public string status;
        }
        public class CLASSIFICATION {
            public int SignId;
            public string Signum, Description;
        }
        public class BORROW {
            public int Barcode, PersonId;
            public DateTime BorrowDate, ToBeReturnedDate,
                            ReturnDate;
        }
        public class BORROWER {
            public int PersonId, CategoryId;
            public string FirstName, LastName,
                          Address, Telno;
        }
        public class CATEGORY {
            public int CategoryId, Penaltyperday;
            public string Category, Period;
        }

        public class BORROWEDBOOK {
            public BOOK book = new BOOK();
            public AUTHOR author = new AUTHOR();
            public BORROW borrow = new BORROW();
        }

        // Skapa egna klasser här //
        public BORROWEDBOOK[] books = new BORROWEDBOOK[3];

        public Mockup()
        {
            for (int i = 0; i < 3; i++ )
                books[i] = new BORROWEDBOOK();

            books[0].author.Aid = 0;
            books[0].author.FirstName = "Johan";
            books[0].author.LastName = "Rasmussen";
            books[0].author.BirthYear = 1994;
            books[0].book.Title = "Den Murviga Ödlan";
            books[0].book.PublicationYear = 2011;
            books[0].book.ISBN = 190213;
            books[0].borrow.BorrowDate = new DateTime(2013, 11, 10);
            books[0].borrow.ToBeReturnedDate = new DateTime(1899, 12, 30);

            books[1].author.Aid = 1;
            books[1].author.FirstName = "Kalas";
            books[1].author.LastName = "Knutte";
            books[1].author.BirthYear = 1280;
            books[1].book.Title = "How To Get Bitches";
            books[1].book.PublicationYear = 1679;
            books[1].book.ISBN = 234190;
            books[1].borrow.BorrowDate = new DateTime(1878, 11, 10);
            books[1].borrow.ToBeReturnedDate = new DateTime(2016, 03, 12);

            books[2].author.Aid = 2;
            books[2].author.FirstName = "Mona";
            books[2].author.LastName = "Saltbil";
            books[2].author.BirthYear = 2004;
            books[2].book.Title = "Politik i ett nötskal";
            books[2].book.PublicationYear = 1999;
            books[2].book.ISBN = 335174;
            books[2].borrow.BorrowDate = new DateTime(2007, 11, 10);
            books[2].borrow.ToBeReturnedDate = new DateTime(2016, 02, 24);
        }
    }
}