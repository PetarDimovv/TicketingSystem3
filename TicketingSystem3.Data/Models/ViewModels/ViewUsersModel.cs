using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem3.Data.Models.ViewModels
{
    public class ViewUsersModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsApprove { get; set; }
        public List<string> Roles { get; set; }
    }
}
