using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Core.ValidationRules.Interfaces;
using Cardcost.Domain;
using String = System.String;

namespace Cardcost.Core.ValidationRules
{
    public class ValidateCardNumber: IValidateCardNumber
    {
        public string CardNumber { get; set; }

        public async Task<bool> Validate(string cardNum)
        {
            if (cardNum.ToCharArray().Any(c => !char.IsDigit(c)))
                throw new ApiException(new Exception("Card number must contain only digits 0-9"), 400);

            if (String.IsNullOrWhiteSpace(cardNum))
                throw new ApiException(new Exception("Card number must have a value"), 400);
            
            if (cardNum.Length < 6)
                throw new ApiException(new Exception("Card number must have at least 6 digits"), 400);

            if (cardNum.Length > 16)
                throw new ApiException(new Exception("Card number must have at most 16 digits"), 400);
            
            return true;
        }
    }
}
