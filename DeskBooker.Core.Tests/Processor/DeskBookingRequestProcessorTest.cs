using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTest
    {
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //Act
            var request = new DeskBookingRequest
            {
                FirstName = "Leah",
                LastName = "Leonor",
                Email = "leah@leahleonor.com",
                Date = new DateTime(2020, 1, 28)
            };

            var processor = new DeskBookingRequestProcessor();

            //Act
            DesktBookingResult result = processor.BookDesk(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }
    }
}
