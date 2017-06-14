using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreateOrder
{
    public class ExistingOrderManagement
    {
        private EntityCollection RetrieveLastOrder(Guid customerId, string businessSegmentCode, IOrganizationService service)
        {
            try
            {
                QueryExpression orderQuery = new QueryExpression();
                orderQuery.EntityName = Constant.Order.EntityName;

                orderQuery.ColumnSet.AddColumn(Constant.Order.OrderDate);

                ConditionExpression customerCondition = new ConditionExpression();
                customerCondition.AttributeName = Constant.Order.CustomerId;
                customerCondition.Operator = ConditionOperator.Equal;
                customerCondition.Values.Add(customerId);

                ConditionExpression orderLockFlagCondition = new ConditionExpression();
                orderLockFlagCondition.AttributeName = Constant.Order.OrderLocked;
                orderLockFlagCondition.Operator = ConditionOperator.Equal;
                orderLockFlagCondition.Values.Add(false);

                ConditionExpression businessSegmentCondition = new ConditionExpression();
                businessSegmentCondition.AttributeName = Constant.Order.BusinessSegment;
                businessSegmentCondition.Operator = ConditionOperator.Equal;
                businessSegmentCondition.Values.Add(businessSegmentCode);

                FilterExpression orderFilter = new FilterExpression();
                orderFilter.AddCondition(orderLockFlagCondition);
                orderFilter.AddCondition(customerCondition);
                orderFilter.AddCondition(businessSegmentCondition);
                orderFilter.FilterOperator = LogicalOperator.And;

                orderQuery.Criteria = orderFilter;

                OrderExpression orderSort = new OrderExpression();
                orderSort.AttributeName = Constant.Order.CreatedOn;
                orderSort.OrderType = OrderType.Descending;

                EntityCollection orderCollection = service.RetrieveMultiple(orderQuery);

                return orderCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LockOrderRecord(Guid orderId, IOrganizationService service)
        {
            try
            {
                Entity orderRecord = new Entity(Constant.Order.EntityName);
                orderRecord.Id = orderId;

                orderRecord[Constant.Order.OrderLocked] = false;

                service.Update(orderRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessOrderWithDate(Entity orderRecord, bool orderLocked, decimal orderCutoffTime, IOrganizationService service)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
