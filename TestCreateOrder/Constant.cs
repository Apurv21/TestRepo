using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreateOrder
{
    public static class Constant
    {
        public static class NationalAccount
        {
            public static string EntityNAme = "";
            public static string Id = "";

            public static string LockedHours = "";
            public static string CustomerId = "";
        }

        public static class ReRoute
        {
            public static string EntityName = "pd_reroute";
            public static string Id = "pd_rerouteid";

            public static string CustomerId = "pd_customerid";
            public static string OldRouteNumber = "pd_oldroutenumberid";
        }

        public static class Route
        {
            public static string EntityName = "pep_foodserviceroute";
            public static string Id = "pep_foodservicerouteid";
            
            public static string NationalRouteNumber = "pep_fsnationalroute";
        }

        public static class Order
        {
            public static string EntityName = "pd_orderheader";
            public static string Id = "pd_orderheaderid";
                        
            public static string OrderLocked = "pd_orderlocked";
            public static string CustomerId = "pd_customerid";
            public static string Type = "pd_order_type";
            public static string DeliveryMethod = "pd_delymethod";
            public static string OrderType = "pd_order_type";
            public static string OrderDate = "pd_orderdate";
            public static string OrderDeleteFlag = "pd_orderdeleteflag";
            public static string RouteNumber = "pd_routeid";
            public static string NotRequired_FrieghtTermInfo = "";
            public static string NotFound_MaxOrderDate = "";
            public static string FromCustomer_BusinessSegment = "";
            public static string CreatedOn = "createdon";
            public static string BusinessSegmentValue = "039";
            public static string MoreInfo_DispatchedLocked = "";

            public static string DoubleOrderPrompted = "pd_doubleorderprompted";
            public static string PrimaryOrganizationId = "pd_locationid";
            public static string SalesMethod = "pd_salesmethod";
            public static string ProductGroup = "pd_productgroup";            
            public static string NotCreated_RouteNumber = "";
            public static string RouteFrequency = "pd_routefrequency";
            public static string Route = "pd_routeid";
            public static string NotCreated_ReRouteId = "";
            public static string Status = "pd_ordstatus";
            public static string NotFound_Type = "";
            public static string Not_CreatedActive = "";
            public static string AppUpdateFlag = "";
            public static string Not_CreatedOrderAction = "";

            public static string BatchCompareFlag = "";
            public static string BatchMatchDate = "";
            public static string BatchMatchRoute = "";

            public static string OrderContactName = "";
            public static string Description = "";
            public static string BusinessSegment = "";
            public static string DispatchedLocked = "";


            public static class StatusValue
            {
                public static int Open = 899100029;
            }

            public static class TypeValue
            {
                public static int Standard = 899100004;
            }
        }

        public static class Customer
        {
            public static string EntityName = "account";
            public static string Id = "accountid";

            public static string SalesMethod = "";
            public static string ProductCustomerFlag = "pep_productcustomer";
            public static string FOBOFlag = "";
            public static string ActiveCustomer = "";
            public static string PrimaryCustomerId = "";
            public static string Status = "statuscode";
            public static string PrimaryOrganizationId = "pep_productlocationid";
            public static string BusinessSegment = "pep_businesssegmentid";
            public static string LocationId = "";
            public static string ContactName = "";

            public static class StatusValue
            {
                public static int Active = 1;
                public static int Inactive = 2;
            }
        }

        public static class OrderShellHeader
        {
            public static string EntityName = "pd_ordershellheader";
            public static string Id = "pd_ordershellheaderid";

            public static string CustomerId = "pd_customerid";
            public static string IDField = "pd_externalid";
            public static string BatchCompareFlag = "";
            public static string BatchMatchDate = "";
            public static string BatchMatchRoute = "";
            public static string DelyHistDate1 = "pd_delyhistdate1";
            public static string DelyHistDate2 = "pd_delyhistdate2";
            public static string DelyHistDate3 = "pd_delyhistdate3";
            public static string DelyHistDate4 = "pd_delyhistdate4";
            public static string DelyHistDate5 = "pd_delyhistdate5";
            public static string DelyHistDate6 = "pd_delyhistdate6";
        }

        public static class SNDMethod
        {
            public static string EntityName = "pep_sellinganddeliverymethod";
            public static string SalesMethod = "pep_salesmethodid";

            public static string ProductGroup = "pep_productgroupid";
            public static string DeliveryMethod = "pep_Deliverymethodid";
            public static string RouteNumber = "pep_routenumberid";
            public static string RouteFrequency = "pep_routefrequencyid";
            //lookup
            public static string Route = "pep_routenumberid";
            //lookup
            public static string ReRouteId = "";
            // customer location
            public static string ORganizationIdStored = "";

        }

        public static class OrderLineItem
        {
            public static string EntityName = "pd_orderlineitem";
            public static string Id = "pd_orderlineitemid";

            public static string BOCode = "pd_bocode";
            public static string BOQuantity = "pd_boquantity";
            public static string BrandSetProductFlag = "pd_brandsetproductflag";
            public static string BrandSetSubTypeCode = "pd_brandsetsubtypecode";
            public static string NotCreated_CampConId = "";
            public static string NotCreated_CampaignId = "";
            public static string DeliveryHistry1 = "pd_deliveryhistory1";
            public static string DeliveryHistry2 = "pd_deliveryhistory2";
            public static string DeliveryHistry3 = "pd_deliveryhistory3";
            public static string DeliveryHistry4 = "pd_deliveryhistory4";
            public static string DeliveryHistry5 = "pd_deliveryhistory5";
            public static string DeliveryHistry6 = "pd_deliveryhistory6";
            public static string LastDeliveryDate = "pd_lastdeliverydate";
            public static string NotCreated_LineType = "";
            public static string NoteCreated_LineNumber = "";
            public static string NotFound_NonVisibleLineItem = "";
            public static string NotCreated_OrganizationIdStored = "";
            public static string OrderHeaderId = "pd_orderheaderid";
            public static string ProductId = "pd_productid";
            public static string NotCreated_ProductOOSFlag = "";
            public static string QuantityRequested = "pd_quantity";
            public static string NotCreated_RepriceFlag = "";
            public static string NotCreated_ReturnCode = "";
            public static string NotCreated_ReturnCodeValue = "";

            public static string LocationProductId = "pep_locationproductid";
        }

        public static class OrderShellLineItem
        {
            public static string EntityName = "pd_ordershelllineitem";
            public static string Id = "pd_ordershelllineitemid";

            public static string BOCode = "pd_bocode";
            public static string BOQuantity = "pd_boquantity";
            public static string BrandSetProductFlag = "pd_pbg_brandset_prod_type";
            public static string BrandSetSubTypeCode = "pd_brandsetsubtypecode";
            //public static string NotCreated_CampConId = "";
            //public static string NotCreated_CampaignId = "";
            public static string DeliveryHistry1 = "pd_deliveryhistory1";
            public static string DeliveryHistry2 = "pd_deliveryhistory2";
            public static string DeliveryHistry3 = "pd_deliveryhistory3";
            public static string DeliveryHistry4 = "pd_deliveryhistory4";
            public static string DeliveryHistry5 = "pd_deliveryhistory5";
            public static string DeliveryHistry6 = "pd_deliveryhistory6";
            //public static string NotFound_EmptyCO2Flag = "";
            public static string HeaderId = "pd_ordershellheaderid";
            public static string NotFound_InventoryAvailableFlag = "";
            public static string LastDeliveryDate = "pd_lastdeliverydate";
            //public static string NotFound_LOCItemInvenFlag = "";
            //public static string NotFound_Package = "";
            public static string ProductId = "pd_productid";
            //public static string NotFound_ProductMixCode = "";
            public static string QuantityRequired = "pd_qtyreq";
            //public static string NotFound_TotalDeliveryHistoryQuantity = "";
            //public static string NotFound_UnitConversionFactor = "";
            
            //public static string FromSND_ProductGroup = "";
            public static string NonVisbleLineItems = "pd_nonvisiblelineitem";
            //public static string NotCreated_PDUnavailable = "";
            //public static string LineType = "pd_type";
            //public static string RePriceFlag = "pd_repriceflag";

            public static string ProductLocationId = "pd_locationproductid";
        }

        public static class Product
        {
            public static string EntityName = "pep_product";
            public static string Id = "pep_productid";

            public static string ProductMixCode = "pep_productmixcode";
            public static string Pacakge = "pep_package";
            public static string EmptyCo2Flag = "pd_emptyco2flag";
            
        }

        public static class LocationProduct
        {
            public static string EntityName = "pep_locationproduct";
            public static string Id = "pep_locationproductid";

            public static string LocationItemInvFlag = "pep_locationitem";
            public static string PDUnavailable = "pd_pdunavailable";
            public static string ProductId = "pep_productid";
        }
    }
}
