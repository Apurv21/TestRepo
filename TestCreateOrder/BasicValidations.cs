using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCreateOrder
{
    public class BasicValidations
    {
        

        private string ValidateCustomer(Entity customerRecord)
        {
            try
            {
                if (customerRecord.Contains(Constant.Customer.ProductCustomerFlag) /*&& customerRecord.Contains(Constant.Customer.FOBOFlag)*/
                    && ((bool)customerRecord[Constant.Customer.ProductCustomerFlag]) == false
                    /*&& ((bool)customerRecord[Constant.Customer.FOBOFlag]) == false*/)
                {
                    return "This customer is not a Product Customer.";
                }

                if (customerRecord.Contains(Constant.Customer.Status)
                    && ((OptionSetValue)customerRecord[Constant.Customer.Status]).Value == Constant.Customer.StatusValue.Inactive)
                {
                    return "This customer is inactive.";
                }

                //// As we are checking SND seapratly for customer don't need this validtion
                //if (!customerRecord.Contains(Constant.Customer.PrimaryOrganizationId))
                //{
                //    return "This customer is not assigned to a Product Location.";
                //}

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ValidateSNDMethod(Entity sndMethodRecord)
        {
            try
            {
                if (sndMethodRecord.Contains(Constant.SNDMethod.SalesMethod)
                    && ((EntityReference)sndMethodRecord[Constant.SNDMethod.SalesMethod]).Name.ToString().ToLowerInvariant() != "pepsi direct")
                {
                    return "You have selected an inappropriate Sales and Delivery Method.";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RunBasicValidations(Entity customerRecord, Entity sndMethodRecord, IOrganizationService service)
        {
            try
            {
                string validationMessage = string.Empty;

                //Entity customerRecord = this.RetrieveCustomerData(accountId, service);

                validationMessage = this.ValidateCustomer(customerRecord);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return validationMessage;
                }

                //Entity sndMethodRecord = this.RetrieveSND(sndId, service);

                validationMessage = this.ValidateSNDMethod(sndMethodRecord);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return validationMessage;
                }

                return validationMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
