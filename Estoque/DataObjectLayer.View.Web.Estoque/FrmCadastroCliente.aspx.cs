using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataObjectLayer;
using DataObjectLayer.Business;
using DataObjectLayer.View.Web;
using DataObjectLayer.Estoque;
using System.Reflection;

namespace DataObjectLayer.View.Web.Estoque
{
    public partial class FrmCadastroCliente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ClienteView.CadastroClientePropertyList() retorna a lista de campos a serem exibidos 
                // no gridCliente.
                gridCliente.PropertyViewEntities = ClienteView.CadastroClientePropertyList();

                // Habilita todos os controles na tela para edição de um novo registro.
                SetControlEntityEnabled.Execute(this, false);

                // Alimenta a lista de cliente do gridCliente.
                gridCliente.RefreshList();
            }
        }
    }
}