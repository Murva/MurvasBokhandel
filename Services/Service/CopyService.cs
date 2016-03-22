using Repository.EntityModel;
using Repository.Repository;
using System.Collections.Generic;

namespace Services.Service
{
    public class CopyService
    {
        public static void RemoveCopyByISBN(string ISBN)
        {
            List<copy> Copies = CopyService.GetCopiesByISBN(ISBN);
            foreach (copy c in Copies)
            {
                RemoveCopy(c.Barcode);
            }
        }

        public static List<copy> GetCopiesByISBN(string ISBN)
        {
            return CopyRepository.dbGetCopiesByISBN(ISBN);
        }

        public static void RemoveCopy(string Barcode)
        {
            CopyRepository.dbRemoveCopy(Barcode);
        }

        public static void CreateCopy(string isbn, string library)
        {
            CopyRepository.dbCreateCopy(isbn, library);
        }

        public static bool IsBorrowed(copy c)
        {
            if (StatusRepository.dbGetStatusByISBN(c.ISBN).statusid != 2)
                return true;

            return false;
        }
    }
}
