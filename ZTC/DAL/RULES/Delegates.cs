using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceConexao.DAL
{
    public delegate bool ConditionDelegate();
    public delegate bool ConditionDelegate<in T>(T o);
}