using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreateOrder
{
    public class OrderShellContract
    {
        //direct shell line items
        private Guid productId = Guid.Empty;
        private int deliveryHistory1 = 0;
        private int deliveryHistory2 = 0;
        private string organizationIdStored = string.Empty;
        private Guid id = Guid.Empty;
        private int deliveryHistory3 = 0;
        private int deliveryHistory4 = 0;
        private int deliveryHistory5 = 0;
        private int deliveryHistory6 = 0;
        private int brandSetSubTypeCode = 0;
        private DateTime lastDeliveryDate = new DateTime(1900, 1, 1);
        private string boCode = string.Empty;
        private string boQuantity = string.Empty;
        // Related entity fields
        private string package = string.Empty;
        private bool inventoryavailableflag = false;
        private bool pbgqtyNonZero = false;
        private bool brandSetOverride = false;
        private string brandSetProdFlg = string.Empty;
        private int requestedQuantity = 0;
        
        //Conditino fields
        private Guid orderShellHeaderId = Guid.Empty;
        private bool locationItemInventoryFlag = false;
        private int totalDeliveryHistoryQuantity = 0;
        private int productMixCode = 0;
        private bool nonVisibleLineItem = false;
        private bool emptyCo2Flag = false;
        private bool pdUnavailable = false;


        public Guid ProductId { get { return this.productId; } set { this.productId = value; } }
        public int DeliveryHistory1 { get { return this.deliveryHistory1; } set { this.deliveryHistory1 = value; } }
        public int DeliveryHistory2 { get { return this.deliveryHistory2; } set { this.deliveryHistory2 = value; } }
        public string OrganizationIdStored { get { return this.organizationIdStored; } set { this.organizationIdStored = value; } }
        public Guid Id { get { return this.id; } set { this.id = value; } }
        public int DeliveryHistory3 { get { return this.deliveryHistory3; } set { this.deliveryHistory3 = value; } }
        public int DeliveryHistory4 { get { return this.deliveryHistory4; } set { this.deliveryHistory4 = value; } }
        public int DeliveryHistory5 { get { return this.deliveryHistory5; } set { this.deliveryHistory5 = value; } }
        public int DeliveryHistory6 { get { return this.deliveryHistory6; } set { this.deliveryHistory6 = value; } }
        public int BrandSetSubTypeCode { get { return this.brandSetSubTypeCode; } set { this.brandSetSubTypeCode = value; } }
        public DateTime LastDeliveryDate { get { return this.lastDeliveryDate; } set { this.lastDeliveryDate = value; } }
        public string BOCode { get { return this.boCode; } set { this.boCode = value; } }
        public string BOQuantity { get { return this.boQuantity; } set { this.boQuantity = value; } }
        public string Package { get { return this.package; } set { this.package = value; } }
        public bool Inventoryavailableflag { get { return this.inventoryavailableflag; } set { this.inventoryavailableflag = value; } }
        public bool PBFqtyNonZero { get { return this.pbgqtyNonZero; } set { this.pbgqtyNonZero = value; } }
        public bool BrandSetOverride { get { return this.brandSetOverride; } set { this.brandSetOverride = value; } }
        public string BrandSetProdFlg { get { return this.brandSetProdFlg; } set { this.brandSetProdFlg = value; } }
        public Guid OrderShellHeaderId { get { return this.orderShellHeaderId; } set { this.orderShellHeaderId = value; } }
        public bool LocationItemInventoryFlag { get { return this.locationItemInventoryFlag; } set { this.locationItemInventoryFlag = value; } }
        public int TotalDeliveryHistoryQuantity { get { return this.totalDeliveryHistoryQuantity; } set { this.totalDeliveryHistoryQuantity = value; } }
        public int ProductMixCode { get { return this.productMixCode; } set { this.productMixCode = value; } }
        public bool NonVisibleLineItem { get { return this.nonVisibleLineItem; } set { this.nonVisibleLineItem = value; } }
        public bool EmptyCo2Flag { get { return this.emptyCo2Flag; } set { this.emptyCo2Flag = value; } }
        public bool PdUnavailable { get { return this.pdUnavailable; } set { this.pdUnavailable = value; } }

        //// Calculated fields
        public string LineType { get { return this.GetLineType(); } }
        public string ProductOOSFlag { get { return this.GetProductOOSFlag(); } }
        public string BrandSetProductFlag { get { return this.GetBrandSetProductGalg(); }}
        public int QuantityRequested { get { return this.GetRequestedQuantity(); } }
        public bool RepriceFlag { get { return this.GetRepriceFlag(); } }
        public string ReturnCodeValue { get { return this.GetReturnCodeValue(); } }
        //public string CampaignId { get { return this.campaignId; } }
        //public string CampConId { get { return this.campConId; } }
        //public bool NonVisibleLineItem { get { return this.nonVisibleLineItem; } }
        public string ReturnCode { get { return this.GetReturnCode(); } }

        private string GetLineType()
        {
            try
            {
                if (this.package.ToLowerInvariant() == "empties")
                {
                    return "Empties";
                }

                return "Products";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetProductOOSFlag()
        {
            try
            {
                if (this.inventoryavailableflag == false && this.requestedQuantity != 0 && this.requestedQuantity != null)
                {
                    return "O";
                }

                return this.brandSetProdFlg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetBrandSetProductGalg()
        {
            try
            {
                if (this.inventoryavailableflag == false && this.pbgqtyNonZero == true
                    && this.brandSetProdFlg == "P" && this.brandSetOverride == true)
                {
                    return "O";
                }

                return this.brandSetProdFlg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetRequestedQuantity()
        {
            if ((this.brandSetOverride == true && this.brandSetProdFlg == "P") || this.brandSetProdFlg == "P")
            {
                return this.requestedQuantity;
            }

            return 0;
        }

        private bool GetRepriceFlag()
        {
            try
            {
                if (this.brandSetProdFlg == "P" && this.brandSetOverride == true && this.requestedQuantity > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetReturnCodeValue()
        {
            try
            {
                if (this.package.ToLowerInvariant() == "empties")
                {
                    return "Empties";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetReturnCode()
        {
            try
            {
                if (this.package.ToLowerInvariant() == "empties")
                {
                    return "004";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
