using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;

namespace TestCreateOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.BasicProcessing();
        }

        public void BasicProcessing()
        {
            try
            {
                IOrganizationService service = this.GetCRMService("https://farahtrial.crm4.dynamics.com", "apurv.deshmukh@farahtrial.onmicrosoft.com", "India@123");

                this.CheckConnectivity(service);

                Guid nonProductCustomer = new Guid("C01AE383-543F-E511-A93E-6C3BE5A8D044");
                //Guid productCustomer = new Guid("FEE6C9C7-6878-4666-A67A-C4DE175B6B10");
                Guid productCustomer = new Guid("F8DE8DD0-F738-48D3-81B8-C310AB8509F2");
                Guid sndPepsiDirect = new Guid("9A81A8A6-2AAC-E611-810D-3863BB2EB148");
                Guid sndOthers = new Guid("66E6849A-CE28-E611-80FA-3863BB2E83E8");


                MSCRMHelper crmHelper = new MSCRMHelper();
                //crmHelper.Convert(service);
                Entity nonProductCustomerRecord =  crmHelper.RetrieveCustomerData(nonProductCustomer, service);

                Entity productCustomerRecord = crmHelper.RetrieveCustomerData(productCustomer, service);

                Entity sndOtherRecord = crmHelper.RetrieveSND(sndOthers, service);

                Entity sndPepsiDirectRecord = crmHelper.RetrieveSND(sndPepsiDirect, service);

                Entity reRouteRecord = new Entity();
                Entity routeRecord = new Entity();

                if (sndPepsiDirectRecord.Contains(Constant.SNDMethod.RouteNumber))
                {
                    reRouteRecord = crmHelper.RetrieveReroute(((EntityReference)sndPepsiDirectRecord[Constant.SNDMethod.RouteNumber]).Id, productCustomerRecord.Id, service);
                }                

                BasicValidations baseValidation = new BasicValidations();
                string validationMessage1 = baseValidation.RunBasicValidations(nonProductCustomerRecord, new Entity(), service);
                Console.WriteLine("Non Product Validation Message: " + validationMessage1);
                string validationMessage2 = baseValidation.RunBasicValidations(productCustomerRecord, sndOtherRecord, service);
                Console.WriteLine("InCorrect SND Mehtod Validation: " + validationMessage2);
                string validationMessage3 = baseValidation.RunBasicValidations(productCustomerRecord, sndPepsiDirectRecord, service);
                Console.WriteLine("All Correct: " + validationMessage3);

                LockedOrderValidation validateLock = new LockedOrderValidation();
                Guid existingOrderId = validateLock.RunLockedOrderValidations(productCustomer, true, service);

                if (existingOrderId == Guid.Empty)
                {
                    CopyOrderShell orderShell = new CopyOrderShell();
                    orderShell.ManageOrderShellCopy(productCustomerRecord, sndPepsiDirectRecord, reRouteRecord.ToEntityReference(), DateTime.Now, string.Empty, service);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckConnectivity(IOrganizationService service)
        {
            try
            {
                WhoAmIRequest identityRequest = new WhoAmIRequest();
                WhoAmIResponse identityResponse = (WhoAmIResponse)service.Execute(identityRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IOrganizationService GetCRMService(string orgURL, string userName, string password)
        {
            try
            {
                string serverUrl = orgURL;
                Uri Url = new Uri(serverUrl + @"/XRMServices/2011/Organization.svc", UriKind.Absolute);// In production code, abstract this to configuration.
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = userName;
                clientCredentials.UserName.Password = password;
                OrganizationServiceProxy service = new OrganizationServiceProxy(Url, null, clientCredentials, null);
                return ((IOrganizationService)service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
