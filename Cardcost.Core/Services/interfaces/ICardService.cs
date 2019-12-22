using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Domain;

namespace Cardcost.Core.Services.interfaces
{
    public interface ICardService
    {
        Task<int> GetCardInfo(string cardNum);
    }
}
