using System;
using System.Collections.Generic;

namespace DeskBooker.Core.Domain
{
    public class DesktBookingResult : DeskBookingBase
    {
        public DeskBookingResultCode Code;

        public int? DeskBookingId { get; set; }
    }
}