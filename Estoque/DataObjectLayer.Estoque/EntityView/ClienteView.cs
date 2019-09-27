using System;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

namespace DataObjectLayer.Estoque
{
    public static class ClienteView
    {
        public static PropertyViewCollection CadastroClientePropertyList()
        {
            PropertyViewCollection prop = new PropertyViewCollection();

            prop.Add(new PropertyView("Cpf", "CPF", 120, 0));
            prop.Add(new PropertyView("Rg", "RG", 80, 1));
            prop.Add(new PropertyView("Nome", "Nome", 250, 2));

            return prop;
        }
    }
}
