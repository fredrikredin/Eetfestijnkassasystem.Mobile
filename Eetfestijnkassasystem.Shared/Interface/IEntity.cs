using System;
using System.Collections.Generic;
using System.Text;

namespace Eetfestijnkassasystem.Shared.Interface
{
    public interface IEntity
    {
        int Id { get; set; }
        //string Name { get; set; }
        DateTime DateTimeCreated { get; set; }
    }
}
