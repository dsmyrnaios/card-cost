using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cardcost.Core.ValidationRules.Interfaces
{
    public interface IValidateCardNumber
    {
        Task<bool> Validate(string cardNum);
    }
}
