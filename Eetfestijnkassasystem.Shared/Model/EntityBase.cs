﻿using System;

namespace Eetfestijnkassasystem.Shared.Model
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            if (DateTimeCreated == default)
                DateTimeCreated = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}
