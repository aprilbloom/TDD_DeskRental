using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
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
        private readonly DeskBookingRequest _request;
        private readonly List<Desk> _avaialbleDesks;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly Mock<IDeskRepository> _deskRepositoryMock;
        private readonly DeskBookingRequestProcessor _processor;

        public DeskBookingRequestProcessorTest()
        {
            _request = new DeskBookingRequest
            {
                FirstName = "Leah",
                LastName = "Leonor",
                Email = "leah@leahleonor.com",
                Date = new DateTime(2020, 1, 28)
            };

            _avaialbleDesks = new List<Desk> { new Desk { Id = 7 } };

            _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            _deskRepositoryMock = new Mock<IDeskRepository>();
            _deskRepositoryMock.Setup(x => x.GetAvailableDesks(_request.Date))
                .Returns(_avaialbleDesks);

            _processor = new DeskBookingRequestProcessor(
                _deskBookingRepositoryMock.Object,_deskRepositoryMock.Object);
        }

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

            //Act
            DesktBookingResult result = _processor.BookDesk(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {   
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveDeskBooking()
        {
            //Arrange
            DeskBooking savedDeskBooking = null;
            _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking =>
                {
                    savedDeskBooking = deskBooking;
                });

            //Act
            _processor.BookDesk(_request);

            //verify is the save method was called once with any desk booking object 
            _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);

            //Asset
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(_request.FirstName, savedDeskBooking.FirstName);
            Assert.Equal(_request.LastName, savedDeskBooking.LastName);
            Assert.Equal(_request.Email, savedDeskBooking.Email);
            Assert.Equal(_request.Date, savedDeskBooking.Date);
            Assert.Equal(_avaialbleDesks.First().Id, savedDeskBooking.DeskId);
        }

        [Fact]
        public void ShouldNotSaveDeskBookingIfNoDeskAvailable()
        {
            _avaialbleDesks.Clear();

            _processor.BookDesk(_request);

            //verify is the save method was called once with any desk booking object 
            _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Never);

        }
    }
}
