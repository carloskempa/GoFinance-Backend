using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoFinance.Domain.Entities;
using GoFinance.Domain.Interfaces.Services;

namespace GoFinance.Domain.Services
{
    public class EmailService : IEmailService
    {
        public Task Enviar(Smtp smtp)
        {
            throw new NotImplementedException();
        }
    }
}
