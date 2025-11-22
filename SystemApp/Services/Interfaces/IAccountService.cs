using BaseBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemApp.DTOs;

namespace SystemApp.Services.Interfaces
{
    public interface IAccountService
    {
        string Login(LoginDto loginDto);

    }
}
