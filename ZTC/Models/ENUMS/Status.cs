using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ZTC.Models.ENUMS
{

    public enum StatusLicenca 
    {
        Ativa = 1,
        Inativa,
        Suspensa,
        Limitada //Só faz consultas
    }
    public enum StatusAtividadeExecucao 
    {
        [Display(Name = "Não Iniciada")]
        NaoIniciada = 1,
        [Display(Name = "Em Execução")]
        EmExecucao,
        [Display(Name = "Aguardando Definição")]
        AguardandoDefinicao
    }

    public enum StatusUsuarioLicenca
    {
        Ativo = 1,
        Suspenso,
        Inativo,
        [Display(Name = "Aguardando Aprovação")]
        AguardandoAprovacao
    }

    public enum StatusAtividadesMonitoramento
    {
        NaoIniciado = 1,
        EmExecucao,
        AguardandoDefinicao,
        ConcluidoNoPrazo, //Automático se DataFim <= DataLimite
        ConcluidaComAtraso //Automático se DataFim > DataLimite
    }

    public enum PrioridadeAtividadesExecucao
    {
        Baixa = 1,
        [Display(Name = "Média")]
        Media,
        Alta,
        [Display(Name = "Crítica")]
        Critica
    }

    public enum CategoriaContato
    {
        Comercial = 1,
        Residencial,
        Pessoal,
        Recado
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var member = type.GetMember(enumValue.ToString());
            var fisrt = member.First();
            var custom = fisrt.GetCustomAttribute<DisplayAttribute>();
            if (custom == null)
                return fisrt.Name;

            var name = custom.GetName();

            return name;

        }
    }
 
}