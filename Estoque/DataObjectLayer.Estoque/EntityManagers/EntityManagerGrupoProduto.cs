using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

namespace DataObjectLayer.Estoque
{
    public class EntityManagerGrupoProduto : EntityManager<GrupoProduto>
    {
        private static new EntityManagerGrupoProduto instance = null;

        [Singleton]
        public static new EntityManagerGrupoProduto Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityManagerGrupoProduto();

                return instance;
            }
        }

        private EntityManagerGrupoProduto()
        {
        }
    }
}
