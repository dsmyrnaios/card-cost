using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Domain;

namespace Cardcost.Core.Services.interfaces
{
    public interface ICardService
    {
        Task<CardInfo> GetCardInfo(string cardNum);
    }
}
