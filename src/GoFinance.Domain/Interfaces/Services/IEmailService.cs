using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoFinance.Domain.Entities;

namespace GoFinance.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task Enviar(Smtp smtp, string email,string title, string body);
    }
}
