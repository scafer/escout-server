using System;

namespace escout.Services;

public class BaseService : IDisposable
{
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~BaseService()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        //Cleanup
    }
}