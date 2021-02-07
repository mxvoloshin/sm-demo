using System;

namespace BlazorApp.Api.Services
{
    public interface IDateTimeService
    {
        DateTimeOffset TableEntityTimeStamp { get; }
        DateTime CurrentUtcDateTime { get; }
    }
}
