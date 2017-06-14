using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Crm.Sdk.Messages;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;


namespace TestCreateOrder
{
    public class LockedOrderValidation
    {
        private string ValidateSalesMethod(Entity orderRecord)
        {
            try
            {
                if (orderRecord.Contains(Constant.Customer.SalesMethod))
                {
                    if (((EntityReference)orderRecord[Constant.SNDMethod.SalesMethod]).Name != "")
                    {
                        return "You have selected an inappropriate Sales and Delivery Method.";
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        private bool ValidateLockedOrder(Entity orderRecord, bool pbgDateLocked, IOrganizationService service)
        {
            try
            {
                bool orderLocked = false;
                bool orderDispatchedLocked = false;

                if (orderRecord.Contains(Constant.Order.OrderLocked)
                    && ((bool)orderRecord[Constant.Order.OrderLocked]))
                {
                    orderLocked = true;
                }

                if (orderRecord.Contains(Constant.Order.DispatchedLocked)
                    && ((bool)orderRecord[Constant.Order.DispatchedLocked]))
                {
                    orderDispatchedLocked = true;
                }

                if ((pbgDateLocked || orderDispatchedLocked) && !orderLocked)
                {
                    //// update order status as locked
                    this.LockOrderRecord(orderRecord.Id, service);
                    return true;
                }
                else if (!pbgDateLocked && orderLocked)
                {
                    //// update order status as not locked
                    this.UnlockOrderRecord(orderRecord.Id, service);
                    return false;
                }


                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CountWeekEnds(DateTime startDate, DateTime endDate)
        {
            int weekEndCount = 0;
            if (startDate > endDate)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }
            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);
                if (testDate.DayOfWeek == DayOfWeek.Saturday || testDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekEndCount += 1;
                }
            }
            return weekEndCount;
        }

        private bool ValidateFOBOCustomerOrderLock(DateTime deliveryDate, int lockDays, Guid orderId, IOrganizationService service)
        {
            try
            {
                double daysToDelivery = (deliveryDate - DateTime.Now).TotalDays;

                int weekendDays = this.CountWeekEnds(DateTime.Now, deliveryDate);

                int val =(int) daysToDelivery - weekendDays - lockDays;

                if (val > 0)
                {
                    this.UnlockOrderRecord(orderId, service);
                    return false;
                }

                this.LockOrderRecord(orderId, service);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private EntityCollection RetrievePastOrder(Guid customerId, IOrganizationService service)
        {
            try
            {
                QueryExpression orderQuery = new QueryExpression();
                orderQuery.EntityName = Constant.Order.EntityName;

                orderQuery.ColumnSet.AddColumn(Constant.Order.CustomerId);
                orderQuery.ColumnSet.AddColumn(Constant.Order.Type);
                orderQuery.ColumnSet.AddColumn(Constant.Order.OrderDate);
                //orderQuery.ColumnSet.AddColumn(Constant.Order.DispatchedLocked);
                orderQuery.ColumnSet.AddColumn(Constant.Order.OrderLocked);

                ConditionExpression customerIdCondition = new ConditionExpression();
                customerIdCondition.AttributeName = Constant.Order.CustomerId;
                customerIdCondition.Operator = ConditionOperator.Equal;
                customerIdCondition.Values.Add(customerId);

                ConditionExpression typeCondition = new ConditionExpression();
                typeCondition.AttributeName = Constant.Order.Type;
                typeCondition.Operator = ConditionOperator.Equal;
                typeCondition.Values.Add(Constant.Order.TypeValue.Standard);

                ConditionExpression statusCondition = new ConditionExpression();
                statusCondition.AttributeName = Constant.Order.Status;
                statusCondition.Operator = ConditionOperator.Equal;
                statusCondition.Values.Add(Constant.Order.StatusValue.Open);

                ConditionExpression orderDeleteFlagCondition = new ConditionExpression();
                orderDeleteFlagCondition.AttributeName = Constant.Order.OrderDeleteFlag;
                orderDeleteFlagCondition.Operator = ConditionOperator.Equal;
                orderDeleteFlagCondition.Values.Add(false);

                ConditionExpression orderLockedCondition = new ConditionExpression();
                orderLockedCondition.AttributeName = Constant.Order.OrderLocked;
                orderLockedCondition.Operator = ConditionOperator.Equal;
                orderLockedCondition.Values.Add(false);

                ConditionExpression orderDateCondition = new ConditionExpression();
                orderDateCondition.AttributeName = Constant.Order.OrderDate;
                orderDateCondition.Operator = ConditionOperator.OnOrAfter;
                orderDateCondition.Values.Add(DateTime.Now.AddDays(1));

                ConditionExpression businessSegmentCondition = new ConditionExpression();
                businessSegmentCondition.AttributeName = Constant.Order.BusinessSegment;
                businessSegmentCondition.Operator = ConditionOperator.Equal;
                businessSegmentCondition.Values.Add(Constant.Order.BusinessSegmentValue);

                FilterExpression orderFilter1 = new FilterExpression();
                orderFilter1.AddCondition(customerIdCondition);
                orderFilter1.AddCondition(typeCondition);
                orderFilter1.AddCondition(statusCondition);
                orderFilter1.AddCondition(orderDeleteFlagCondition);
                orderFilter1.FilterOperator = LogicalOperator.And;

                FilterExpression orderFilter2_1 = new FilterExpression();
                orderFilter2_1.AddCondition(orderLockedCondition);
                orderFilter2_1.AddCondition(orderDateCondition);
                orderFilter2_1.FilterOperator = LogicalOperator.Or;

                FilterExpression orderFilter2_2 = new FilterExpression();
                //orderFilter2_2.AddCondition(businessSegmentCondition);
                orderFilter2_2.FilterOperator = LogicalOperator.And;

                FilterExpression orderFilter2 = new FilterExpression();
                orderFilter2.AddFilter(orderFilter2_1);
                orderFilter2.AddFilter(orderFilter2_2);
                orderFilter2.FilterOperator = LogicalOperator.And;

                FilterExpression orderFilter3 = new FilterExpression();
                orderFilter3.AddCondition(orderLockedCondition);
                //orderFilter3.AddCondition(businessSegmentCondition);
                orderFilter3.FilterOperator = LogicalOperator.And;

                FilterExpression orderFilter = new FilterExpression();
                orderFilter.AddFilter(orderFilter1);
                orderFilter.AddFilter(orderFilter2);
                orderFilter.AddFilter(orderFilter3);

                orderQuery.Criteria = orderFilter;
                orderQuery.AddOrder(Constant.Order.CreatedOn, OrderType.Descending);

                EntityCollection orderCollection = service.RetrieveMultiple(orderQuery);

                return orderCollection;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool PBGDateCheckLock(Guid orderId, IOrganizationService service)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid RunLockedOrderValidations(Guid customerId, bool isFoboCustomer, IOrganizationService service)
        {
            try
            {
                EntityCollection pastOrderCollection = this.RetrievePastOrder(customerId, service);

                foreach(Entity orderRecord in pastOrderCollection.Entities)
                {   
                    bool isLocked = true;
                    bool pbgDateLockFlag = true;

                    if (orderRecord.Contains(Constant.Order.OrderLocked))
                    {                       
                        pbgDateLockFlag = this.PBGDateCheckLock(orderRecord.Id, service);

                        isLocked = this.ValidateLockedOrder(orderRecord, pbgDateLockFlag, service);

                        if (isFoboCustomer && orderRecord.Contains(Constant.Order.OrderDate))
                        {
                            isLocked = this.ValidateFOBOCustomerOrderLock(((DateTime)orderRecord[Constant.Order.OrderDate]), 3, orderRecord.Id, service);
                        }

                        if (!isLocked)
                        {
                            return orderRecord.Id;
                        }
                    }
                }

                return Guid.Empty;
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

                orderRecord[Constant.Order.OrderLocked] = true;

                service.Update(orderRecord);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UnlockOrderRecord(Guid orderId, IOrganizationService service)
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
    }
}
