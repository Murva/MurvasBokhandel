using Repository.EntityModel;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CategoryService
    {
        public static List<category> getCategories() {
            List<category> categories = CategoryRepository.dbGetCategories();
            return categories;
        }
    }
}
