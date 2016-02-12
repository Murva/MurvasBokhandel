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
            public int statusid { get; set; }
            public string status { get; set; }
        }
        public class CLASSIFICATION {
            public int SignId { get; set; }
            public string Signum { get; set; }
            public string Description { get; set; }
        }
        public class BORROW {
            public long Barcode { get; set; }
            public long PersonId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime ToBeReturnedDate { get; set; }
            public DateTime ReturnDate { get; set; }
        }
        public class BORROWER {
            public long PersonId { get; set; }
            public int CategoryId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; } 
            public string Telno { get; set; }
        }
        public class CATEGORY {
            public int CategoryId, Penaltyperday;
            public string Category, Period;
        }

        //* ------------------------------------------------ LISTOR ---------------------------------------------------  *//

        public static List<Mockup.AUTHOR> Authors = new List<Mockup.AUTHOR>()
        {
            new Mockup.AUTHOR() {
                Aid = 1,
                FirstName = "J.K",
                LastName = "Rowling",
                BirthYear = 1961
            },
            new Mockup.AUTHOR() {
                Aid = 2,
                FirstName = "Liza",
                LastName = "Marklund",
                BirthYear = 1967
            },
            new Mockup.AUTHOR() {
                Aid = 3,
                FirstName = "Astrid",
                LastName = "Lindgren",
                BirthYear = 1917
            }
        };

       public static List<Mockup.BOOK_AUTHOR> Book_Authers = new List<Mockup.BOOK_AUTHOR>() {
            new Mockup.BOOK_AUTHOR() { Aid = 1, ISBN = 9789129697704 },
            new Mockup.BOOK_AUTHOR() { Aid = 1, ISBN = 9789129675559 },
            new Mockup.BOOK_AUTHOR() { Aid = 1, ISBN = 9789129675566 },
            new Mockup.BOOK_AUTHOR() { Aid = 2, ISBN = 9789164204530 },
            new Mockup.BOOK_AUTHOR() { Aid = 2, ISBN = 9789175790336 },
            new Mockup.BOOK_AUTHOR() { Aid = 3, ISBN = 9789129697308 },
            new Mockup.BOOK_AUTHOR() { Aid = 4, ISBN = 9789129698442 },
        };

       public static List<Mockup.BOOK> Books = new List<Mockup.BOOK>()
        {
            new Mockup.BOOK() { ISBN=9789129697704, Title="Harry Potter och De vises sten", SignId = 0, Pages = "512", PublicationYear = 1997, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129675559, Title="Harry Potter och hemligheternas kammare", SignId = 0, Pages = "751", PublicationYear = 1999, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129675566, Title="Harry Potter och fången från Azkaban", SignId = 0, Pages = "895", PublicationYear = 2001, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789164204530, Title="Järnblod", SignId = 0, Pages = "523", PublicationYear = 2011, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789175790336, Title="Lyckliga gatan", SignId = 0, Pages = "566", PublicationYear = 2012, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129697308, Title="Allrakäraste syster", SignId = 0, Pages = "876", PublicationYear = 1975, Publicationinfo = "No info"},
            new Mockup.BOOK() { ISBN=9789129698442, Title="Känner du Pippi Långstrump?", SignId = 0, Pages = "120", PublicationYear = 1961, Publicationinfo = "No info"}
        };

       //* ------------------------------------------------ LISTOR END ---------------------------------------------------  *//

       //* ------------------------------------------------ RESULTATLISTOR ---------------------------------------------------  *//

       public static List<AuthorWithBooks> AuthorsWithBooksResults = new List<AuthorWithBooks>() {
            new AuthorWithBooks() {
                Author = Mockup.Authors[0],
                Books = new List<Mockup.BOOK>() {
                    Mockup.Books[0],
                    Mockup.Books[1],
                    Mockup.Books[2]
                }
            },
            new AuthorWithBooks() {
                Author = Mockup.Authors[1],
                Books = new List<Mockup.BOOK>() {
                    Mockup.Books[3],
                    Mockup.Books[4]
                }
            },
            new AuthorWithBooks() {
                Author = Mockup.Authors[2],
                Books = new List<Mockup.BOOK>() {
                    Mockup.Books[5],
                    Mockup.Books[6]
                } 
            }
        };

       //* ------------------------------------------------ RESULTATLISTOR END ---------------------------------------------------  *//

        public Mockup() { 
                        
        }
    }
}