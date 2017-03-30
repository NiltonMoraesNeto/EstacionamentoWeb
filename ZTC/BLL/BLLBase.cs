using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZTC.BLL;
using InterfaceConexao.DAL;

namespace ZTC.BLL
{
    public abstract class BLLBase<T> where T : class
    {
        /// <summary>
        /// Carrega o objeto com as informacoes da base de dados
        /// </summary>
        /// <param name="o"></param>
        public void CompleteObject(T o)
        {
            using (var dal = DatabaseConnection.GetDataAccessLayer())
            {
                CompleteObject(o, dal);
            }
        }

        /// <summary>
        /// Carrega o objeto com as informacoes da base de dados
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dal"></param>
        public abstract void CompleteObject(T o, DataAccessLayer dal, bool completeRelatedObjects = true);

        /// <summary>
        /// Carrega os objetos relacionados com as informacoes da base de dados
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dal"> </param>
        protected abstract void CompleteRelatedObjects(T o, DataAccessLayer dal, bool completeRelatedObjects = true);
    }
}