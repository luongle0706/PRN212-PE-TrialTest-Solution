using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SupplierCompanyService
    {
        private SupplierCompanyRepository _repository = new();

        public List<SupplierCompany> GetAllSupplierCompanies()
        {
            return _repository.GetAllSupplierCompanies();
        }
    }
}
