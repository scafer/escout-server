﻿namespace escout.Models
{
    public class Identity
    {
        public long Id
        {
            get;
            set;
        }

        // Conversion from Identity to Int64
        public static implicit operator long(Identity d)
        {
            return (d.Id);
        }

        //  Conversion from Int64 to Identity
        public static implicit operator Identity(long d)
        {
            return (new Identity() { Id = d });
        }

        // Conversion from Identity to Int32
        public static implicit operator int(Identity d)
        {
            return ((int)d.Id);
        }

        //  Conversion from Int32 to Identity
        public static implicit operator Identity(int d)
        {
            return (new Identity() { Id = d });
        }
    }
}
