using System;

namespace quik.Runtime.Services
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(Type serviceType)
            : base($"Service of type {serviceType.Name} was not found.")
        {
        }

        public ServiceNotFoundException(Type serviceType, Exception innerException)
            : base($"Service of type {serviceType.Name} was not found.", innerException)
        {
        }
    }
}
