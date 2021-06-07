using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoFinance.Domain.Interfaces.Services
{
    public interface ICriptografiaService
    {
        byte[] Encrypt(string text);
    }
}
