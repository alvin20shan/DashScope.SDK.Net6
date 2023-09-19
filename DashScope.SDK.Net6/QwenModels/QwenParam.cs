using DashScope.SDK.Net6.Models;
using DashScope.SDK.Net6.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.QwenModels
{
    public class QwenParam : ChatRequestParam
    {

        public double TopP { get; set; }


        public double TopK { get; set; }

        public bool EnableSearch { get; set; } = false;

        public int Seed { get; set; }
         
        public override ChatRequestParam BuildParam()
        {
            if (TopP > 0) 
            {
                if (!base.Parameters!.ContainsKey(FieldKeyNames.TOP_P))
                {
                    base.Parameters.Add(FieldKeyNames.TOP_P, TopP);
                }
            }
            if (TopK > 0)
            {
                if (!base.Parameters!.ContainsKey(FieldKeyNames.TOP_K))
                {
                    base.Parameters.Add(FieldKeyNames.TOP_K, TopK);
                }
            }
            if (!base.Parameters!.ContainsKey(FieldKeyNames.ENABLESEARCH))
            {
                Parameters.Add(FieldKeyNames.ENABLESEARCH, EnableSearch);
            }
            if (!base.Parameters.ContainsKey(FieldKeyNames.RESULTFORMAT))
            {
                Parameters.Add(FieldKeyNames.RESULTFORMAT, ResultFormat);
            }
            return this;
        }

       
    }
}
