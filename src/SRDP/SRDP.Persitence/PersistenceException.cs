using System;

namespace SRDP.Persitence
{
    public class PersistenceException : Exception
    {
        internal PersistenceException(string bussinessMessage)
            : base(bussinessMessage)
        {

        }

    }
}
