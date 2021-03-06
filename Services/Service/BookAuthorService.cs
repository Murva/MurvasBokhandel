﻿using Repository.EntityModel;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class BookAuthorService
    {
        public static void StoreBookAuthor(bookAuthor ba)
        {
            BookAuthorRepository.dbStoreBookAuthor(ba);
        }

        public static bool BookAuthorExists(int Aid, string ISBN)
        {
            return (BookAuthorRepository.dbGetBookAuthor(Aid, ISBN) != null ? true : false);
        }

        public static void RemoveBookAuthorByISBN(string ISBN)
        {
            BookAuthorRepository.dbRemoveBookAuthorByISBN(ISBN);
        }

        public static void RemoveBookAuthor(int Aid, string ISBN)
        {
            BookAuthorRepository.dbRemoveBookAuthor(Aid, ISBN);
        }
    }
}
