﻿namespace qlbb2.Helper
{
    public static class ServiceHelper
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static T GetService<T>() => ServiceProvider.GetService<T>();

    }
}
