using System;

namespace DeskBooker.Core.Processor
{
    internal class DeskBookingRequestProcessor
    {
        public DeskBookingRequestProcessor()
        {
        }

        internal DesktBookingResult BookDesk(DeskBookingRequest request)
        {
            return new DesktBookingResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date,
            };
        }
    }
}