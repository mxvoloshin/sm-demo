using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Api.Services
{
    public interface IDateTimeService
    {
        DateTimeOffset TableEntityTimeStamp { get; }
        DateTime CurrentUtcDateTime { get; }
    }
}
