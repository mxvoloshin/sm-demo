using System;

namespace BlazorApp.Api.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset TableEntityTimeStamp => DateTimeOffset.UtcNow;
        public DateTime CurrentUtcDateTime => DateTime.UtcNow;
    }
}
