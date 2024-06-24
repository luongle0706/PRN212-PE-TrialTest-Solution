using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StaffMemberRepository
    {
        private AirConditionerShop2024DbContext _context;

        public StaffMember? GetUserLogin(string emailAddress, string password)
        {
            _context = new();
            return _context.StaffMembers.FirstOrDefault(
                account => account.EmailAddress.Equals(emailAddress) && account.Password.Equals(password));
        }
    }
}
