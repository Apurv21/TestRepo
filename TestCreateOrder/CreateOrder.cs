using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Crm.Sdk.Messages;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Workflow;

namespace TestCreateOrder
{
    public class CreateOrder /*: CodeActivity*/
    {
        //protected sealed override void Execute(CodeActivityContext executionContext)
        //{
        //    try
        //    {
        //        //Create the tracing service
        //        ITracingService tracingService = executionContext.GetExtension<ITracingService>();

        //        //Create the context
        //        IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
        //        IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
        //        IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

        //        CreateOrderValidation basicValidations = new CreateOrderValidation();
                
        //        Entity customerRecord = this.RetrieveCustomerData(Guid.Empty, service);

        //        string customerBasicValidations = basicValidations.ValidateCustomer(customerRecord);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        

        

        

        private Guid CopyOrderShell(Guid customerId, IOrganizationService service)
        {
            try
            {
                Guid orderHeaderId = Guid.Empty;

                return orderHeaderId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopyOrderShellItems(Guid orderShellId, Guid orderHeaderId, IOrganizationService service)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        

        private Entity RetrieveNationalAccount(Guid customerID, IOrganizationService service)
        {
            try
            {
                QueryExpression nationalAccountQuery = new QueryExpression();
                nationalAccountQuery.EntityName = Constant.NationalAccount.EntityNAme;

                nationalAccountQuery.ColumnSet.AddColumn(Constant.NationalAccount.LockedHours);

                ConditionExpression customerIdCondition = new ConditionExpression();
                customerIdCondition.AttributeName = Constant.NationalAccount.CustomerId;
                customerIdCondition.Operator = ConditionOperator.Equal;
                customerIdCondition.Values.Add(customerID);


                FilterExpression nationalAccountFilter = new FilterExpression();
                nationalAccountFilter.AddCondition(customerIdCondition);
                nationalAccountFilter.FilterOperator = LogicalOperator.And;

                nationalAccountQuery.Criteria = nationalAccountFilter;

                EntityCollection nationalAccountCollection = service.RetrieveMultiple(nationalAccountQuery);

                if (nationalAccountCollection.Entities.Count > 0)
                {
                    return nationalAccountCollection[0];
                }

                return new Entity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

