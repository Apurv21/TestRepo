using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;

namespace TestCreateOrder
{
    public class CopyOrderShell
    {
        private Guid CopyOrderShellData(Entity orderShellRecord, Entity customerRecord, EntityReference reRouteReference, DateTime deliveryDate, Entity SNDRecord, IOrganizationService service)
        {
            try
            {
                Guid orderHeaderId = Guid.Empty;

                Entity orderHeaderRecord = new Entity();
                orderHeaderRecord.LogicalName = Constant.Order.EntityName;

                if (deliveryDate != null)
                {
                    orderHeaderRecord[Constant.Order.OrderDate] = deliveryDate;
                }

                orderHeaderRecord[Constant.Order.DoubleOrderPrompted] = "No";
                if(customerRecord.Contains(Constant.Customer.LocationId))
                {
                    orderHeaderRecord[Constant.Order.PrimaryOrganizationId] = (EntityReference)customerRecord[Constant.Customer.LocationId];
                }

                if (SNDRecord.Contains(Constant.SNDMethod.SalesMethod))
                {//// from SNDk
                    orderHeaderRecord[Constant.Order.SalesMethod] = ((EntityReference)SNDRecord[Constant.SNDMethod.SalesMethod]).Name;
                }

                if (SNDRecord.Contains(Constant.SNDMethod.ProductGroup))
                {//// from SND
                    orderHeaderRecord[Constant.Order.ProductGroup] = ((EntityReference)SNDRecord[Constant.SNDMethod.ProductGroup]).Name;
                }

                if (SNDRecord.Contains(Constant.SNDMethod.DeliveryMethod))
                {//// from SND
                    orderHeaderRecord[Constant.Order.DeliveryMethod] = SNDRecord[Constant.SNDMethod.DeliveryMethod];
                }

                if (SNDRecord.Contains(Constant.SNDMethod.RouteNumber))
                {
                    orderHeaderRecord[Constant.Order.RouteNumber] = SNDRecord[Constant.SNDMethod.RouteNumber];
                }

                if (SNDRecord.Contains(Constant.SNDMethod.RouteFrequency))
                {//// from SND
                    orderHeaderRecord[Constant.Order.RouteFrequency] = SNDRecord[Constant.SNDMethod.RouteFrequency];
                }

                if (SNDRecord.Contains(Constant.SNDMethod.Route))
                {//// from SND
                    orderHeaderRecord[Constant.Order.Route] = SNDRecord[Constant.SNDMethod.Route];
                }

                if (reRouteReference.Id != Guid.Empty)
                {//// frp, SND
                    //orderHeaderRecord[Constant.Order.NotCreated_ReRouteId] = reRouteReference;
                }
                
                //// default Open
                orderHeaderRecord[Constant.Order.Status] = new OptionSetValue(Constant.Order.StatusValue.Open);

                //// default Standard
                //orderHeaderRecord[Constant.Order.Type] = new OptionSetValue(Constant.Order.TypeValue.Standard);
                
                //// Default PBG Order
                //orderHeaderRecord[Constant.Order.OrderType] = "PBG Order";
                
                if (customerRecord.Contains(Constant.Customer.ContactName))
                {//// from customer
                    orderHeaderRecord[Constant.Order.OrderContactName] = customerRecord[Constant.Customer.ContactName];
                }

                //// default empty for POC
                //orderHeaderRecord[Constant.Order.Description] = string.Empty;
                //// default value to 'Y'
                //orderHeaderRecord[Constant.Order.Not_CreatedActive] = "Y";
                //// default value to 'APP'
                //orderHeaderRecord[Constant.Order.AppUpdateFlag] = "APP";
                //// Default value as 'N'
                //orderHeaderRecord[Constant.Order.OrderLocked] = false;

                //// Set value as 'N' for order
                //orderHeaderRecord[Constant.Order.OrderDeleteFlag] = false;
                
                //// set empty in order action
                //orderHeaderRecord[Constant.Order.Not_CreatedOrderAction] = string.Empty;


                orderHeaderId = service.Create(orderHeaderRecord);

                return orderHeaderId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopyOrderShellItems(Guid orderShellId, EntityCollection orderShellItemCollection, Entity SNDRecord, Guid orderHeaderId, IOrganizationService service)
        {
            try
            {
                foreach(Entity orderShellItemRecord in orderShellItemCollection.Entities)
                {
                    Entity orderLineItemRecord = new Entity();
                    orderLineItemRecord.LogicalName = Constant.OrderLineItem.EntityName;

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.LineType))
                    //{//// shell line item's product's package
                    //    //// if package == Empties then empties else Products
                    //    orderLineItemRecord[Constant.OrderLineItem.NotCreated_LineType] = orderShellItemRecord[Constant.OrderShellLineItem.LineType];
                    //}

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.ProductId))
                    {//// Product lookup on shell line item
                        orderLineItemRecord[Constant.OrderLineItem.ProductId] = orderShellItemRecord[Constant.OrderShellLineItem.ProductId];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.DeliveryHistry1))
                    {//// from Shell line item
                        orderLineItemRecord[Constant.OrderLineItem.DeliveryHistry1] = orderShellItemRecord[Constant.OrderShellLineItem.DeliveryHistry1];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.DeliveryHistry2))
                    {//// from Shell line item
                        orderLineItemRecord[Constant.OrderLineItem.DeliveryHistry2] = orderShellItemRecord[Constant.OrderShellLineItem.DeliveryHistry2];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.DeliveryHistry3))
                    {//// from Shell line item
                        orderLineItemRecord[Constant.OrderLineItem.DeliveryHistry3] = orderShellItemRecord[Constant.OrderShellLineItem.DeliveryHistry3];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.DeliveryHistry4))
                    {//// from Shell line item
                        orderLineItemRecord[Constant.OrderLineItem.DeliveryHistry4] = orderShellItemRecord[Constant.OrderShellLineItem.DeliveryHistry4];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.DeliveryHistry5))
                    {//// from Shell line item
                        orderLineItemRecord[Constant.OrderLineItem.DeliveryHistry5] = orderShellItemRecord[Constant.OrderShellLineItem.DeliveryHistry5];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.DeliveryHistry6))
                    {//// from Shell line item
                        orderLineItemRecord[Constant.OrderLineItem.DeliveryHistry6] = orderShellItemRecord[Constant.OrderShellLineItem.DeliveryHistry6];
                    }

                    if (SNDRecord.Contains(Constant.SNDMethod.ORganizationIdStored))
                    {//// FROM snd
                        orderLineItemRecord[Constant.OrderLineItem.NotCreated_OrganizationIdStored] = SNDRecord[Constant.SNDMethod.ORganizationIdStored];
                    }

                    if (false)
                    {//// NOT FOUND
                        orderLineItemRecord[Constant.OrderLineItem.NotCreated_ProductOOSFlag] = "";
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.BrandSetProductFlag))
                    {//// from shell line item
                        orderLineItemRecord[Constant.OrderLineItem.BrandSetProductFlag] = orderShellItemRecord[Constant.OrderShellLineItem.BrandSetProductFlag];
                    }

                    if (false)
                    {//// should not be implemented
                        orderLineItemRecord[Constant.OrderLineItem.QuantityRequested] = 0;
                    }

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.RePriceFlag))
                    //{//// from order shell item
                    //    orderLineItemRecord[Constant.OrderLineItem.NotCreated_RepriceFlag] = orderShellItemRecord[Constant.OrderShellLineItem.RePriceFlag];
                    //}

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.NotFound_Package))
                    //{//// shell line item's product's package
                    //    //// if package == Empties then 004 else string.empty
                    //    orderLineItemRecord[Constant.OrderLineItem.NotCreated_ReturnCode] = orderShellItemRecord[Constant.OrderShellLineItem.NotFound_Package];
                    //}

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.ReturnCode))
                    //{//// shell line item's product's package
                    //    //// if package == Empties then empties else string.empty
                    //    orderLineItemRecord[Constant.OrderLineItem.NotCreated_ReturnCodeValue] = orderShellItemRecord[Constant.OrderShellLineItem.ReturnCode];
                    //}

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.BrandSetSubTypeCode))
                    {//// from order shell item entity
                        orderLineItemRecord[Constant.OrderLineItem.BrandSetSubTypeCode] = orderShellItemRecord[Constant.OrderShellLineItem.BrandSetSubTypeCode];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.LastDeliveryDate))
                    {//// from order shell item item
                        orderLineItemRecord[Constant.OrderLineItem.LastDeliveryDate] = orderShellItemRecord[Constant.OrderShellLineItem.LastDeliveryDate];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.BOCode))
                    {//// from order shell item entity
                        orderLineItemRecord[Constant.OrderLineItem.BOCode] = orderShellItemRecord[Constant.OrderShellLineItem.BOCode];
                    }

                    if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.BOQuantity))
                    {//// from order shell item entity
                        orderLineItemRecord[Constant.OrderLineItem.BOQuantity] = orderShellItemRecord[Constant.OrderShellLineItem.BOQuantity];
                    }

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.NotCreated_CampaignId))
                    //{//// not created
                    //    orderLineItemRecord[Constant.OrderLineItem.NotCreated_CampaignId] = orderShellItemRecord[Constant.OrderShellLineItem.NotCreated_CampaignId];
                    //}

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.NotCreated_CampConId))
                    //{//// not created
                    //    orderLineItemRecord[Constant.OrderLineItem.NotCreated_CampConId] = orderShellItemRecord[Constant.OrderShellLineItem.NotCreated_CampConId];
                    //}

                    //if (orderShellItemRecord.Contains(Constant.OrderShellLineItem.BrandSetSubTypeCode)
                    //    && orderShellItemRecord.Contains(Constant.OrderShellLineItem.NotFound_TotalDeliveryHistoryQuantity)
                    //    && orderShellItemRecord.Contains(Constant.OrderShellLineItem.LastDeliveryDate))
                    //{//// from shell item entity
                    //    //// IIf(([Brand Set Sub Type Code] = 'D' or [Brand Set Sub Type Code] = 'L') 
                    //    ////AND [Total Delivery History Qty] = 0, IIf([Last Delivery Date] IS NULL, 'Y',
                    //    ////IIf([Last Delivery Date] < [&DateFilter], 'Y', [Non Visible Line Item])) ,[Non Visible Line Item])
                    //    string visibilityItem = string.Empty;
                    //    if((orderShellItemRecord[Constant.OrderShellLineItem.BrandSetSubTypeCode].ToString() == "D" || orderShellItemRecord[Constant.OrderShellLineItem.BrandSetSubTypeCode].ToString() == "L") 
                    //        && (int)orderShellItemRecord[Constant.OrderShellLineItem.NotFound_TotalDeliveryHistoryQuantity] == 0)
                    //    {
                    //        if(orderShellItemRecord[Constant.OrderShellLineItem.LastDeliveryDate] == null)
                    //        {
                    //            visibilityItem = "Y";
                    //        }
                    //        else if((DateTime)orderShellItemRecord[Constant.OrderShellLineItem.LastDeliveryDate] < DateTime.Now)
                    //        {
                    //            visibilityItem = "Y";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        visibilityItem = "N";
                    //    }
                    //    orderLineItemRecord[Constant.OrderLineItem.NotFound_NonVisibleLineItem] = visibilityItem;
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Entity RetrieveOrderShell(Guid customerId, IOrganizationService service)
        {
            try
            {
                QueryExpression orderShellQuery = new QueryExpression();
                orderShellQuery.EntityName = Constant.OrderShellHeader.EntityName;

                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.IDField);
                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.DelyHistDate1);
                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.DelyHistDate2);
                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.DelyHistDate3);
                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.DelyHistDate4);
                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.DelyHistDate5);
                orderShellQuery.ColumnSet.AddColumn(Constant.OrderShellHeader.DelyHistDate6);

                ConditionExpression customerCondition = new ConditionExpression();
                customerCondition.AttributeName = Constant.OrderShellHeader.CustomerId;
                customerCondition.Operator = ConditionOperator.Equal;
                customerCondition.Values.Add(customerId);

                FilterExpression orderShellFilter = new FilterExpression();
                orderShellFilter.AddCondition(customerCondition);
                orderShellFilter.FilterOperator = LogicalOperator.And;

                orderShellQuery.Criteria = orderShellFilter;

                EntityCollection orderShellCollection = service.RetrieveMultiple(orderShellQuery);

                if (orderShellCollection.Entities.Count > 0)
                {
                    return orderShellCollection[0];
                }
                return new Entity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EntityCollection RetrieveOrderShellLineItems(Guid orderShellHeaderId, string customerLocation, IOrganizationService service)
        {
            try
            {
                EntityCollection orderShellItemCollection = new EntityCollection();                

                QueryExpression orderShellItemQuery = new QueryExpression();
                orderShellItemQuery.EntityName = Constant.OrderShellLineItem.EntityName;
                
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.ProductId);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.DeliveryHistry1);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.DeliveryHistry2);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.DeliveryHistry3);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.DeliveryHistry4);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.DeliveryHistry5);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.DeliveryHistry6);
                //orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.BrandSetProductFlag);
                //orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.NotFound_Package);
                //orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.ReturnCode);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.BrandSetSubTypeCode);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.LastDeliveryDate);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.BOCode);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.BOQuantity);
                orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.BrandSetProductFlag);
                //orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.NotCreated_CampaignId);
                //orderShellItemQuery.ColumnSet.AddColumn(Constant.OrderShellLineItem.NotCreated_CampConId);

                ConditionExpression shellHeaderIdCondition = new ConditionExpression();
                shellHeaderIdCondition.AttributeName = Constant.OrderShellLineItem.HeaderId;
                shellHeaderIdCondition.Operator = ConditionOperator.Equal;
                shellHeaderIdCondition.Values.Add(orderShellHeaderId);

                ConditionExpression nonVisibilityCondition = new ConditionExpression();
                nonVisibilityCondition.AttributeName = Constant.OrderShellLineItem.NonVisbleLineItems;
                nonVisibilityCondition.Operator = ConditionOperator.NotEqual;
                nonVisibilityCondition.Values.Add(true);

                FilterExpression orderShellFilter = new FilterExpression();
                orderShellFilter.AddCondition(shellHeaderIdCondition);
                orderShellFilter.AddCondition(nonVisibilityCondition);
                orderShellFilter.FilterOperator = LogicalOperator.And;

                LinkEntity locationProductLink = new LinkEntity();
                locationProductLink.LinkFromAttributeName = Constant.OrderShellLineItem.ProductLocationId;
                locationProductLink.LinkToAttributeName = Constant.LocationProduct.Id;
                locationProductLink.LinkFromEntityName = Constant.OrderShellLineItem.EntityName;
                locationProductLink.LinkToEntityName = Constant.LocationProduct.EntityName;
                locationProductLink.JoinOperator = JoinOperator.LeftOuter;
                locationProductLink.EntityAlias = "LP";

                locationProductLink.Columns.AddColumn(Constant.LocationProduct.LocationItemInvFlag);
                locationProductLink.Columns.AddColumn(Constant.LocationProduct.PDUnavailable);
                locationProductLink.Columns.AddColumn(Constant.LocationProduct.ProductId);

                LinkEntity productLink = new LinkEntity();
                productLink.LinkFromEntityName = Constant.LocationProduct.EntityName;
                productLink.LinkToEntityName = Constant.Product.EntityName;
                productLink.LinkToAttributeName = Constant.Product.Id;
                productLink.LinkFromAttributeName = Constant.LocationProduct.ProductId;
                productLink.JoinOperator = JoinOperator.LeftOuter;
                productLink.EntityAlias = "P";

                productLink.Columns.AddColumn(Constant.Product.Pacakge);
                productLink.Columns.AddColumn(Constant.Product.ProductMixCode);
                productLink.Columns.AddColumn(Constant.Product.EmptyCo2Flag);

                locationProductLink.LinkEntities.Add(productLink);

                orderShellItemQuery.LinkEntities.Add(locationProductLink);

                orderShellItemQuery.Criteria = orderShellFilter;

                MSCRMHelper crmHelper = new MSCRMHelper();
                QueryExpression convertedQuery = crmHelper.Convert(service);

                orderShellItemCollection = service.RetrieveMultiple(orderShellItemQuery);

                return orderShellItemCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<OrderShellContract> TransformOrderShell(EntityCollection orderShellCollection)
        {
            try
            {
                foreach(Entity orderShellRecord in orderShellCollection.Entities)
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new List<OrderShellContract>();
        }

        public string ManageOrderShellCopy(Entity customerRecord, Entity SNDRecord, EntityReference reRouteReference, DateTime deliveryDate, string productGroup, IOrganizationService service)
        {
            try
            {
                string returnOutput = string.Empty;

                Entity orderShellHeader = this.RetrieveOrderShell(customerRecord.Id, service);
                Guid orderHeaderId = this.CopyOrderShellData(orderShellHeader, customerRecord, reRouteReference, deliveryDate, SNDRecord, service);

                EntityCollection orderShellLineItemsCollection = this.RetrieveOrderShellLineItems(orderShellHeader.Id, "sample", service);

                this.CopyOrderShellItems(orderShellHeader.Id, orderShellLineItemsCollection, SNDRecord, orderHeaderId, service);

                if (orderHeaderId != Guid.Empty)
                {
                    returnOutput = orderHeaderId.ToString();
                }
                return returnOutput;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

