using Repository.EntityModel;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
