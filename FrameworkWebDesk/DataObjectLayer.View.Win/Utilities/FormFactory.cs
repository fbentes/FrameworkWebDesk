using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataObjectLayer.View.Win
{
    public class FormFactory<T> where T: Form
    {
        public delegate void ExecutarAcoes();

        private T form;

        public T Form
        {
            get { return form; }
        }

        private static FormFactory<T> instance = null;

        public static FormFactory<T> Instance
        {
            get
            {
                if (instance == null)
                    instance = new FormFactory<T>();

                return instance;
            }
        }

        private FormFactory()
        {
        }
        
        public T CreateForm()
        {
            ConstructorInfo construtor = typeof(T).GetConstructor(Type.EmptyTypes);
            form = construtor.Invoke(null) as T;

            return form;
        }

        public void CreateAndShowForm()
        {
            CreateAndShowForm(default(T), null);
        }

        public void CreateAndShowForm(Form parent)
        {
            CreateAndShowForm(parent, null);
        }

        public void CreateAndShowForm(Form parent, ExecutarAcoes executarAcoes)
        {
            form = getFormOpened(parent);

            if (form == default(T))
            {
                form = CreateForm();

                if(parent != default(T))
                    form.MdiParent = parent;

                if (executarAcoes != null)
                    executarAcoes();

                form.WindowState = FormWindowState.Maximized;

                form.Show();
            }
            else
                form.Focus();
        }

        private T getFormOpened(Form parent)
        {
            if (parent == default(T))
                return default(T);

            if (parent.IsMdiContainer && parent.HasChildren)
            {
                foreach (Form mdiChild in parent.MdiChildren)
                {
                    if (mdiChild.GetType() == typeof(T))
                    {
                        return mdiChild as T;
                    }
                }
            }

            return default(T);
        }
    }
}
