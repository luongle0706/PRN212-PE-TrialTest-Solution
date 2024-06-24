using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SupplierCompanyRepository
    {
        private AirConditionerShop2024DbContext _context;

        public List<SupplierCompany> GetAllSupplierCompanies()
        {
            _context = new();
            return _context.SupplierCompanies.ToList();
        }
    }
}
