using System;

namespace escout.Services
{
    public class BaseService : IDisposable
    {
        ~BaseService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Cleanup
        }
    }
}
