using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

namespace DataObjectLayer.Estoque
{
    public class EntityManagerProduto : EntityManager<Produto>
    {
        private static new EntityManagerProduto instance = null;

        [Singleton]
        public static new EntityManagerProduto Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManagerProduto();

                return instance;
            }
        }

        private EntityManagerProduto()
        {
        }

        public IList ProdutoList(Operacao operacao)
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
