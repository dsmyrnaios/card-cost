using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Core.ValidationRules.Interfaces;
using String = System.String;

namespace Cardcost.Core.ValidationRules
{
    public class ValidateCardNumber: IValidateCardNumber
    {
        public string CardNumber { get; set; }

        public async Task<bool> Validate(string cardNum)
        {
            if(String.IsNullOrWhiteSpace(cardNum))
                throw new Exception("Card number must have a value");

            if (cardNum.Length < 6)
                throw new Exception("Card number must have at least 6 digits");

            if (cardNum.Length > 16)
                throw new Exception("Card number must have at most 16 digits");

            if(cardNum.ToCharArray().Any(c => !char.IsDigit(c)))
                throw new Exception("Card number must contain only digits 0-9");

            return true;
        }
    }
}
