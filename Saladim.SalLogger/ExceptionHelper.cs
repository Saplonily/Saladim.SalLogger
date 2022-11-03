#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Saladim.SalLogger;

public static class ExceptionHelper
{
    [DebuggerStepThrough]
    public static List<Exception> GetChainedExceptions(Exception exception)
    {
        List<Exception> list = new();
        Exception? cur = exception;
        while (cur != null)
        {
            list.Add(cur);
            cur = cur.InnerException;
        }
        return list;
    }
}