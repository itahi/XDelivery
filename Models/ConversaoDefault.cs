using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Models
{
   public static class ConversaoDefault
    {
       public static int StrToIntDef(string iValor, int @Default)
       {
           int Numero;
           if (int.TryParse(iValor,out Numero))
           {
               return Numero;
           }

           return @Default;
       }
       public static Boolean BoolToInt(string iValor, Boolean @Default)
       {
           Boolean Numero;
           if (bool.TryParse(iValor, out Numero))
           {
               return Numero;
           }

           return @Default;
       }
       public static string VarToStrDef(string iValor, string @Default)
       {
           string Numero;
           if (string.IsNullOrEmpty(iValor))
           {
             //  return Numero;
           }

           return @Default;
       }

    }
}
