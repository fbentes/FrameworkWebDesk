using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace DataObjectLayer.View.Web
{
    public static class GetControl<T> where T:Control
    {
        public static Control Execute(string Id, Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.ID == Id)
                {
                    return (T)control;
                }
                else
                    if (control.Controls.Count > 0)
                    {
                        Control controlFound = Execute(Id, control);

                        if (controlFound != null)
                        {
                            return controlFound;
                        }
                    }
            }

            return null;
        }
    }
}