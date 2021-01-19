using System;

namespace DIBS.D2
{
    [Serializable]
    public class AuthorizationStatus
    {
        public int Code { get; set; }
        public string Description { get; set; }

        private AuthorizationStatus(int Code, string Description) 
        { 
            this.Code = Code;
            this.Description = Description;
        }

        public AuthorizationStatus(int Code)
        {
            this.Code = Code;

            switch (Code)
            {
                case 0:
                    this.Description = "Rejected by acquirer.";
                    break;
                case 1:
                    this.Description = "Communication problems.";
                    break;
                case 2:
                    this.Description = "Error in the parameters sent to the Nets server.";
                    break;
                case 3:
                    this.Description = "Error at the acquirer.";
                    break;
                case 4:
                    this.Description = "Credit card expired.";
                    break;
                case 5:
                    this.Description = "Your shop does not support this credit card type, the credit card type could not be identified, or the credit card number was not modulus correct.";
                    break;
                case 6:
                    this.Description = "Instant capture failed.";
                    break;
                case 7:
                    this.Description = "The order number (orderid) is not unique.";
                    break;
                case 8:
                    this.Description = "There number of amount parameters does not correspond to the number given in the split parameter.";
                    break;
                case 9:
                    this.Description = "Control numbers (cvc) are missing.";
                    break;
                case 10:
                    this.Description = "The credit card does not comply with the credit card type.";
                    break;
                case 11:
                    this.Description = "Declined by Nets Defender.";
                    break;
                case 20:
                    this.Description = "Cancelled by user at 3D Secure authentication step.";
                    break;
                default:
                    this.Code = -1;
                    this.Description = "Unknown error.";
                    break;
            }
        }



        public static AuthorizationStatus Unknown
        {
            get { return new AuthorizationStatus(-1, "Unknown error."); }
        }
        public static AuthorizationStatus Rejected
        { 
            get { return new AuthorizationStatus(0, "Rejected by acquirer."); }
        }
        public static AuthorizationStatus CommunicationProblems
        {
            get { return new AuthorizationStatus(1, "Communication problems."); }
        }
        public static AuthorizationStatus ParametersError
        {
            get { return new AuthorizationStatus(2, "Error in the parameters sent to the Nets server."); }
        }
        public static AuthorizationStatus AcquirerError
        {
            get { return new AuthorizationStatus(3, "Error at the acquirer."); }
        }
        public static AuthorizationStatus CreditCardExpired
        {
            get { return new AuthorizationStatus(4, "Credit card expired."); }
        }
        public static AuthorizationStatus UnsupportedCreditCardType
        {
            get { return new AuthorizationStatus(5, "Your shop does not support this credit card type, the credit card type could not be identified, or the credit card number was not modulus correct."); }
        }
        public static AuthorizationStatus CaptureFailed
        {
            get { return new AuthorizationStatus(6, "Instant capture failed."); }
        }
        public static AuthorizationStatus OrderIdNotUnique
        {
            get { return new AuthorizationStatus(7, "The order number (orderid) is not unique."); }
        }
        public static AuthorizationStatus ParametersNotCorrespond
        {
            get { return new AuthorizationStatus(8, "There number of amount parameters does not correspond to the number given in the split parameter."); }
        }
        public static AuthorizationStatus MissingCVC
        {
            get { return new AuthorizationStatus(9, "Control numbers (cvc) are missing."); }
        }
        public static AuthorizationStatus CreditCardNotValid
        {
            get { return new AuthorizationStatus(10, "The credit card does not comply with the credit card type."); }
        }
        public static AuthorizationStatus Declined
        {
            get { return new AuthorizationStatus(11, "Declined by Nets Defender."); }
        }
        public static AuthorizationStatus Cancelled
        {
            get { return new AuthorizationStatus(20, "Cancelled by user at 3D Secure authentication step."); }
        }



        public static bool operator ==(AuthorizationStatus obj1, AuthorizationStatus obj2)
        {
            if (obj1 is null && obj2 is null) return true;
            if (obj1 is null || obj2 is null) return false;
            return (obj1.Code == obj2.Code);
        }

        public static bool operator !=(AuthorizationStatus obj1, AuthorizationStatus obj2)
        {
            if (obj1 is null && obj2 is null) return false;
            if (obj1 is null || obj2 is null) return true;
            return (obj1.Code != obj2.Code);
        }
    }
}