using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StaffMemberService
    {
        private StaffMemberRepository repository = new();

        public StaffMember? CheckLoginUser(string emailAddress, string password)
        {
            return repository.GetUserLogin(emailAddress, password);
        }
    }
}
