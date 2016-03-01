﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Repository.Repository;
using Repository.EntityModel;

namespace Services.Service
{
    public class BookService
    {
        public static List<book> GetBooksByAuthor(int aid)
        {
            return BookRepository.dbGetBookListByAuthor(aid);
        }

        public static book GetBook(string isbn) {
            return BookRepository.dbGetBook(isbn);
        }

        public static List<book> GetBooks()
        {
            return BookRepository.dbGetBooks();
        }

        public static BookAndAuthors GetBookAndAuthors(string isbn)
        {
            return MapBookAndAuthors(BookRepository.dbGetBook(isbn));
        }

        private static BookAndAuthors MapBookAndAuthors(book b)
        {
            BookAndAuthors bookandauthers = new BookAndAuthors();
            bookandauthers.Book = b;
            bookandauthers.Authors = AuthorService.GetAuthersByBook("LastName");
            return bookandauthers;
        }

        //public static AuthorWithBooks GetAuthorWithBooks(int aid)
        //{
        //    return MapAuthorWithBooks(AuthorRepository.dbGetAuthor(aid));
        //}

        //public static AuthorWithBooks MapAuthorWithBooks(author a)
        //{
        //    AuthorWithBooks authorwithbooks = new AuthorWithBooks();
        //    authorwithbooks.Author = a;
        //    authorwithbooks.Books = BookService.GetBooksByAuthor(a.Aid);

        //    return authorwithbooks;
        //}

    }
}
