using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DashService.Classes
{
    public class ClHtml
    {        
        public class ResultadoHtml
        {     
            public string resultado { get; set; }
        }
        public class resultadoTransito
        {
            public string norte { get; set; }
            public string sul { get; set; }
            public string leste { get; set; }
            public string oeste { get; set; }
            public string centro { get; set; }
            public string total { get; set; }
        }
        public class resultadoRodizio
        {
            public string placa1 { get; set; }
            public string placa2 { get; set; }
            public string status { get; set; }
        }
        public class resultadoClima
        {
            public int tmaxima { get; set; }
            public int tminima { get; set; }
            public int tmomento { get; set; }
            public string vento { get; set; }
            public string velocidadevento { get; set; }
            public string status { get; set; }
            public string detalhe { get; set; }
            public string data { get; set; }
            public int chuva { get; set; }
            public int mm { get; set; }
            public int umidade { get; set; }            
        }

        public class resultadoTransporte
        {
            public string ocorrencia { get; set; }            
        }
    }
}