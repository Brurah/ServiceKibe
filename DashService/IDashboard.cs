using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DashService.Classes;
using System.ServiceModel.Activation;

namespace DashService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDashboard" in both code and config file together.
    [ServiceContract]
    public interface IDashboard
    {
        [OperationContract]        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate="/transito")]        
        [return: MessageParameter(Name = "transito")]
        ClHtml.resultadoTransito TransitoSvc();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/rodizio")]
        [return: MessageParameter(Name = "rodizio")]
        ClHtml.resultadoRodizio RodizioSvc();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/clima")]
        [return: MessageParameter(Name = "clima")]
        List<ClHtml.resultadoClima> ClimaSvc();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/ar")]
        [return: MessageParameter(Name = "condicao")]
        string ArSvc();       

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/sptrans")]
        [return: MessageParameter(Name = "transporte")]
        List<ClHtml.resultadoTransporte> SptransSvc();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/metro")]
        [return: MessageParameter(Name = "transporte")]
        List<ClHtml.resultadoTransporte> MetroSvc();

    }
}
