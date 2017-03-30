using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace InterfaceConexao.DAL
{
    [Serializable]
    public class ConflictedException : Exception
    {
        public ConflictedException()
        {
        }

        public ConflictedException(string message)
            : base(message)
        {
        }

        public ConflictedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConflictedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public static bool CheckConflictedException(Exception ex, out ConflictedException exC)
        {
            exC = null;

            if (ex is SqlException)
            {
                var sqlex = ex as SqlException;

                if (sqlex.ErrorCode == -2146232060)
                {
                    exC = new ConflictedException("O registro possui vínculo! Não é possível fazer a exclusão!", ex);
                    return true;
                }
            }

            return false;
        }
    }
}