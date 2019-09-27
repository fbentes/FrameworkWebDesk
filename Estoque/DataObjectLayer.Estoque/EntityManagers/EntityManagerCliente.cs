using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

namespace DataObjectLayer.Estoque
{
    public class EntityManagerCliente : EntityManager<Cliente>
    {
        private static new EntityManagerCliente instance = null;

        [Singleton]
        public static new EntityManagerCliente Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManagerCliente();

                return instance;
            }
        }

        private EntityManagerCliente()
        {
        }

        public IList ClienteList(Operacao operacao)
        {
            ListFilterEntity listFilter = null;

            if (operacao == Operacao.NewRegister)
            {
                listFilter = new ListFilterEntity();
                listFilter.Add(new FilterEntity("Ativo", FilterCriteria.Equal, true));
            }

            return this.List(new OrderEntity[] { new OrderEntity("Nome", true) }, listFilter); 
        }
    }
}
