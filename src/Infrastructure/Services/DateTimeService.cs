using alpvisionapp.Application.Common.Interfaces;
using System;

namespace alpvisionapp.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
