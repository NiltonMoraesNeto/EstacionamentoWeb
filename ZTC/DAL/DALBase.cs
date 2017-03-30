using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ZTC.DAL
{
      /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Entidade</typeparam>
    public abstract class DALBase<T> where T : class, new()
      {

        /// <summary>
        /// Carrega o objeto da classe com os dados do data reader
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="o"></param>
        protected abstract void LoadObjectInternal(IDataReader dr, T o);

        /// <summary>
        /// Carrega o objeto da classe com os dados do data reader. Verifica se o data reader contem dados.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="o"></param>
        protected void LoadObject(IDataReader dr, T o)
        {
            if (dr.Read())
                LoadObjectInternal(dr, o);
        }

        /// <summary>
        /// Converte o data reader em um objeto da classe
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private T ConvertToObjectInternal(IDataReader dr)
        {
            var o = new T();
            LoadObjectInternal(dr, o);
            return o;
        }

        /// <summary>
        /// Converte um data reader em um objeto da classe. Verifica se o data reader contem dados.
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected T ConvertToObject(IDataReader dr)
        {
            return dr.Read() ? ConvertToObjectInternal(dr) : default(T);
        }

        /// <summary>
        /// Atualiza um objeto da classe com os dados vindos direto da base
        /// </summary>
        /// <param name="o"></param>
        public abstract void CompleteObject(T o);

        /// <summary>
        /// Carrega uma lista de objetos da classe com os dados do Data Reader
        /// </summary>
        /// <param name="dr">Data reader</param>
        /// <param name="list">Lista de objetos a ser carregada</param>
        protected virtual void LoadList(IDataReader dr, List<T> list)
        {
            while (dr.Read())
            {
                list.Add(ConvertToObjectInternal(dr));
            }
        }

        /// <summary>
        /// Converte o objeto DataReader em uma lista de objetos da classe
        /// </summary>
        /// <param name="dr">Data reader</param>
        /// <returns></returns>
        protected List<T> ConvertToList(IDataReader dr)
        {
            var list = new List<T>();
            LoadList(dr, list);
            return list;
        }
    }
}
