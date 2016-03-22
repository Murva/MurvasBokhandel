﻿using System.Collections.Generic;
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

        public static BookWithAuthorsAndAuthors GetBookWithAuthorsAndAuthors(string isbn)
        {
            return MapBookWithAuthorsAndAuthors(BookRepository.dbGetBook(isbn));
        }

        public static BookWithAuthorsAndAuthors MapBookWithAuthorsAndAuthors(book b)
        {
            BookWithAuthorsAndAuthors baa = new BookWithAuthorsAndAuthors();
            baa.Book = b;
            baa.Authors = AuthorService.GetAuthors("FirstName");
            baa.BookAuthors = AuthorService.GetAuthersByBook(b.ISBN);

            return baa;
        }

        public static List<book> GetBooks()
        {
            return BookRepository.dbGetBooks();
        }

        public static BookWithAuthorS GetBookWithAuthors(string isbn)
        {
            return MapBookWithAuthorS(BookRepository.dbGetBook(isbn));
        }

        private static BookWithAuthorS MapBookWithAuthorS(book b)
        {
            BookWithAuthorS bookandauthers = new BookWithAuthorS();
            bookandauthers.Book = b;
            bookandauthers.Authors = AuthorService.GetAuthersByBook(b.ISBN);
            return bookandauthers;
        }

        public static bool BookExists(string ISBN)
        {
            return (BookRepository.dbGetBook(ISBN) != null ? true : false);
        }

        public static BookWithClassifications NewBookWithClassifications()
        {
            return new BookWithClassifications()
                {
                    Book = new Repository.EntityModel.book(),
                    Classifications = ClassificationService.GetClassifications()
                };
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

        public static void UpdateBook(book b)
        {
            BookRepository.dbUpdateBook(b);
        }

        public static List<book> GetBooksBySearch(string input)
        {
            return BookRepository.dbGetBooksBySearch(input);
        }       

        public static bool StoreBook(book b, int copies, string library)
        {
            BookRepository.dbStoreBook(b);
            for (int i = 0; i < copies; i++)
                CopyService.CreateCopy(b.ISBN, library);

            return true;
        }

        public static bool RemoveBook(book b)
        {
            if (HasBorrows(b))
                return false;

            CopyService.RemoveCopyByISBN(b.ISBN);
            BookAuthorService.RemoveBookAuthorByISBN(b.ISBN);
            BookRepository.dbRemoveBook(b.ISBN);

            return true;
        }

        public static bool HasBorrows(book b)
        {
            foreach (copy c in CopyRepository.dbGetCopiesByISBN(b.ISBN))
                if (CopyService.IsBorrowed(c))
                    return false;
            return true;
        }
    }
}
