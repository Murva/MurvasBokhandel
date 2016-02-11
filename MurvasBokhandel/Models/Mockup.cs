using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MurvasBokhandel.Models
{
    public class Mockup
    {
        public class BOOK {
            public long ISBN { get; set; }
            public int SignId { get; set; }
            public int PublicationYear { get; set; }
            public string Title { get; set; }
            public string Publicationinfo { get; set; }
            public string Pages { get; set; }
        }
        public class BOOK_AUTHOR {
            public long ISBN { get; set; }
            public int Aid { get; set; }
        }
        public class AUTHOR {
            public int Aid { get; set; }
            public int BirthYear { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        public class COPY {
            public int Barcode { get; set; }
            public int StatusId { get; set; }
            public long ISBN { get; set; }
            public string Location { get; set; }
            public string Library { get; set; }
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