using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreateOrder
{
    public class MSCRMHelper
    {
        public Entity RetrieveCustomerData(Guid accountId, IOrganizationService service)
        {
            try
            {
                ColumnSet accountColumns = new ColumnSet();
                accountColumns.AddColumn(Constant.Customer.ProductCustomerFlag);
                accountColumns.AddColumn(Constant.Customer.Status);

                Entity accountRecord = service.Retrieve(Constant.Customer.EntityName, accountId, accountColumns);

                return accountRecord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entity RetrieveSND(Guid sndId, IOrganizationService service)
        {
            try
            {
                ColumnSet sndColumns = new ColumnSet();
                sndColumns.AddColumn(Constant.SNDMethod.SalesMethod);
                sndColumns.AddColumn(Constant.SNDMethod.RouteNumber);

                Entity sndRecord = service.Retrieve(Constant.SNDMethod.EntityName, sndId, sndColumns);

                return sndRecord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entity RetrieveReroute(Guid oldRouteNumber, Guid customerId, IOrganizationService service)
        {
            try
            {
                QueryExpression reRouteQuery = new QueryExpression();
                reRouteQuery.EntityName = Constant.ReRoute.EntityName;

                ConditionExpression customerIdCondition = new ConditionExpression();
                customerIdCondition.AttributeName = Constant.ReRoute.CustomerId;
                customerIdCondition.Operator = ConditionOperator.Equal;
                customerIdCondition.Values.Add(customerId);

                ConditionExpression oldRouteNumberCondition = new ConditionExpression();
                oldRouteNumberCondition.AttributeName = Constant.ReRoute.OldRouteNumber;
                oldRouteNumberCondition.Operator = ConditionOperator.Equal;
                oldRouteNumberCondition.Values.Add(oldRouteNumber);


                FilterExpression reRouteFilter = new FilterExpression();
                reRouteFilter.AddCondition(customerIdCondition);
                reRouteFilter.AddCondition(oldRouteNumberCondition);
                reRouteFilter.FilterOperator = LogicalOperator.And;

                reRouteQuery.Criteria = reRouteFilter;

                EntityCollection reRouteCollection = service.RetrieveMultiple(reRouteQuery);

                if (reRouteCollection.Entities.Count > 0)
                {
                    return reRouteCollection[0];
                }

                return new Entity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entity RetrieveRoute(string nationalRouteNumber, IOrganizationService service)
        {
            try
            {
                QueryExpression routeQuery = new QueryExpression();
                routeQuery.EntityName = Constant.Route.EntityName;

                ConditionExpression nationalRouteNumberCondition = new ConditionExpression();
                nationalRouteNumberCondition.AttributeName = Constant.Route.NationalRouteNumber;
                nationalRouteNumberCondition.Operator = ConditionOperator.Equal;
                nationalRouteNumberCondition.Values.Add(nationalRouteNumber);

                FilterExpression routeFilter = new FilterExpression();
                routeFilter.AddCondition(nationalRouteNumberCondition);
                routeFilter.FilterOperator = LogicalOperator.And;

                routeQuery.Criteria = routeFilter;

                EntityCollection routeCollection = service.RetrieveMultiple(routeQuery);

                if (routeCollection.Entities.Count > 0)
                {
                    return routeCollection[0];
                }

                return new Entity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckIfCustomerISFOBO(Guid customerId, IOrganizationService service)
        {
            try
            {
                QueryExpression customerQuery = new QueryExpression();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public QueryExpression Convert(IOrganizationService service)
        {
            try
            {
                string fetchXml = "";
                fetchXml += "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>";
                fetchXml += "<entity name='pd_ordershelllineitem'>";
                fetchXml += "<attribute name='pd_ordershelllineitemid' />";
                fetchXml += "<attribute name='pd_name' />";
                fetchXml += "<attribute name='createdon' />";
                fetchXml += "<order attribute='pd_name' descending='false' />";
                fetchXml += "<filter type='and'>";
                fetchXml += "<condition attribute='pd_ordershellheaderid' operator='eq' uiname='test 1 Header' uitype='pd_ordershellheader' value='{9AAC56F7-02F9-E611-8102-FC15B428FAC0}' />";
                fetchXml += "<condition attribute='pd_nonvisiblelineitem' operator='eq' value='0' />";
                fetchXml += "</filter>";
                fetchXml += "<link-entity name='pep_locationproduct' from='pep_locationproductid' to='pd_locationproductid' visible='false' link-type='outer' alias='a_339f5c2437eae611810ac4346bad41fc'>";
                fetchXml += "<attribute name='pd_pdunavailable' />";
                fetchXml += "<attribute name='pep_locationitem' />";
                fetchXml += "</link-entity></entity></fetch>";

                FetchXmlToQueryExpressionRequest convertRequest = new FetchXmlToQueryExpressionRequest();
                convertRequest.FetchXml = fetchXml;

                FetchXmlToQueryExpressionResponse convertResponse = (FetchXmlToQueryExpressionResponse)service.Execute(convertRequest);

                return convertResponse.Query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
