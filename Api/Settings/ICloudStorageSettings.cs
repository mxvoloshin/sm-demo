using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Api.Interfaces
{
    public interface ICloudStorageSettings
    {
        string ConnectionString { get; }
    }
}
