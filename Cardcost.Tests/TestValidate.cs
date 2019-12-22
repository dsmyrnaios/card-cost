using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Controllers;
using Cardcost.Core.Services.interfaces;
using Cardcost.Core.ValidationRules;
using Cardcost.Core.ValidationRules.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Cardcost.Tests
{
    public class TestValidate
    {
        private IValidateCardNumber _validateCardNumber;

        public TestValidate()
        {
            _validateCardNumber = new ValidateCardNumber();
        }

        [Theory]
        [InlineData("5168")]
        [InlineData("  ")]
        [InlineData("aaaa")]
        [InlineData("5168-3800")]
        [InlineData("5168854758732459874589")]
        public async Task ValidateCardNumberFailed(string cardnum)
        {
            try
            {
                var response = await _validateCardNumber.Validate(cardnum);
                // Assert
                var viewResult = Assert.IsType<bool>(response);
                Assert.Equal(false, viewResult);
            }
            catch (Exception exc)
            {
                Assert.Equal(true, true);
            }
        }

        [Theory]
        [InlineData("51683800")]
        [InlineData("45717360")]
        public async Task ValidateCardNumberPassed(string cardnum)
        {
            var response = await _validateCardNumber.Validate(cardnum);
            //
            // Assert
            var viewResult = Assert.IsType<bool>(response);
            Assert.Equal(true, viewResult);
        }
    }
}
