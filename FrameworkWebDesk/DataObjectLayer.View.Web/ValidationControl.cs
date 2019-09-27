using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataObjectLayer.View.Web
{
    public static class ValidationsControls
    {
        private static void setEnabled(IValidator control)
        {
            if (control is IValidator)
            {
                if (control is RequiredFieldValidator)
                {
                    (control as RequiredFieldValidator).Enabled = true;
                }
                else
                    if (control is RegularExpressionValidator)
                    {
                        (control as RegularExpressionValidator).Enabled = true;
                    }
                    else
                        if (control is CompareValidator)
                        {
                            (control as CompareValidator).Enabled = true;
                        }
                        else
                            if (control is CustomValidator)
                            {
                                (control as CustomValidator).Enabled = true;
                            }
                            else
                                if (control is RangeValidator)
                                {
                                    (control as RangeValidator).Enabled = true;
                                }
            }

        }

        public static bool IsValid(params IValidator[] validatorList)
        {
            foreach (IValidator validator in validatorList)
            {
                setEnabled(validator);

                validator.Validate();

                if (!validator.IsValid)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValid(ValidationSummary validationSummary)
        {
            return IsValid(validationSummary, null);
        }

        public static bool IsValid(ValidationSummary validationSummary, params string[] validationGroupList)
        {
            if (validationGroupList != null && validationGroupList.Length > 0)
            {
                foreach (string validationGroup in validationGroupList)
                {
                    validationSummary.Page.Validate(validationGroup);
                }
            }
            else
            {
                validationSummary.Page.Validate();
            }

            return validationSummary.Page.IsValid;
        }

        public static void Enable(Control parent, bool enable)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    Enable(control, enable);
                }
                else
                    if (control is IValidator)
                    {
                        if (control is RequiredFieldValidator)
                        {
                            (control as RequiredFieldValidator).Enabled = enable;
                        }
                        else
                            if (control is RegularExpressionValidator)
                            {
                                (control as RegularExpressionValidator).Enabled = enable;
                            }
                            else
                                if (control is CompareValidator)
                                {
                                    (control as CompareValidator).Enabled = enable;
                                }
                                else
                                    if (control is CustomValidator)
                                    {
                                        (control as CustomValidator).Enabled = enable;
                                    }
                                    else
                                        if (control is RangeValidator)
                                        {
                                            (control as RangeValidator).Enabled = enable;
                                        }
                    }
            }
        }       
    }
}
