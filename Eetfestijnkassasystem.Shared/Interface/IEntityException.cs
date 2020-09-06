using System;
using System.Collections.Generic;
using System.Text;

namespace Eetfestijnkassasystem.Shared.Interface
{
    public interface IEntityException
    {
        string Type { get; }
        string StackTrace { get; }
        string Message { get; }
    }
}
