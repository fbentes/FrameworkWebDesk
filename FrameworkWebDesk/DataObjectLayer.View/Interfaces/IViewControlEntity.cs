using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.View
{
    public interface IViewControlEntity
    {
        string EntitySource
        {
            get;
            set;
        }

        string EntityProperty
        {
            set;
            get;
        }

        object Value
        {
            get;
        }

        bool IsSetEntityFromControl
        {
            set;
            get;
        }

        bool Visible
        {
            get;
            set;
        }

        bool Enabled
        {
            set;
            get;
        }

        void SetValueToControl(object value);
    }
}
