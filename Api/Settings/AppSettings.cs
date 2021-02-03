using BlazorApp.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Api.Settings
{
    public class AppSettings : ICloudStorageSettings
    {
        private string _connectionString;
        
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString", EnvironmentVariableTarget.Process);
                }

                return _connectionString;
            }
        }
    }
}
