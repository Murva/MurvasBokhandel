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

        // Skapa egna klasser här //
        
        public Mockup() { 
                        
        }
    }
}