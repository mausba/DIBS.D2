namespace DIBS.D2
{
    public class HandlingStatus
    {
        public int Code { get; set; }
        public string Description { get; set; }

        private HandlingStatus(int Code, string Description)
        {
            this.Code = Code;
            this.Description = Description;
        }

        public HandlingStatus(int Code)
        {
            this.Code = Code;

            switch (Code)
            {
                case 0: 
                    this.Description = "Accepted.";
                    break;
                case 1:
                    this.Description = "No response from acquirer.";
                    break;
                case 2:
                    this.Description = "Timeout.";
                    break;
                case 3:
                    this.Description = "Credit card expired.";
                    break;
                case 4:
                    this.Description = "Rejected by acquirer.";
                    break;
                case 5:
                    this.Description = "Authorisation older than 7 days.";
                    break;
                case 6:
                    this.Description = "Transaction status on the Nets server does not allow function.";
                    break;
                case 7:
                    this.Description = "Amount too high.";
                    break;
                case 8:
                    this.Description = "Error in the parameters sent to the Nets server. An additional parameter called 'message' is returned, with a value that may help identifying the error.";
                    break;
                case 9:
                    this.Description = "Order number (orderid) does not correspond to the authorisation order number.";
                    break;
                case 10:
                    this.Description = "Re-authorisation of the transaction was rejected.";
                    break;
                case 11:
                    this.Description = "Not able to communicate with the acquier.";
                    break;
                case 12:
                    this.Description = "Confirm request error.";
                    break;
                case 14:
                    this.Description = "Capture is called for a transaction which is pending for batch - i.e. capture was already called.";
                    break;
                case 15:
                    this.Description = "Capture or refund was blocked by Nets.";
                    break;
                default:
                    this.Code = -1;
                    this.Description = "Unknown error.";
                    break;
            }
        }



        public static HandlingStatus Unknown
        {
            get { return new HandlingStatus(-1, "Unknown error."); }
        }
        public static HandlingStatus Accepted
        {
            get { return new HandlingStatus(0, "Accepted."); }
        }
        public static HandlingStatus NoResponse
        {
            get { return new HandlingStatus(1, "No response from acquirer."); }
        }
        public static HandlingStatus Timeout
        {
            get { return new HandlingStatus(2, "Timeout."); }
        }
        public static HandlingStatus CreditCardExpired
        {
            get { return new HandlingStatus(3, "Credit card expired."); }
        }
        public static HandlingStatus Rejected
        {
            get { return new HandlingStatus(4, "Rejected by acquirer."); }
        }
        public static HandlingStatus AuthorisationOld
        {
            get { return new HandlingStatus(5, "Authorisation older than 7 days."); }
        }
        public static HandlingStatus NotAllowedFunction
        {
            get { return new HandlingStatus(6, "Transaction status on the Nets server does not allow function."); }
        }
        public static HandlingStatus AmountTooHigh
        {
            get { return new HandlingStatus(7, "Amount too high."); }
        }
        public static HandlingStatus ParametersError
        {
            get { return new HandlingStatus(8, "Error in the parameters sent to the Nets server. An additional parameter called 'message' is returned, with a value that may help identifying the error."); }
        }
        public static HandlingStatus OrderIdNotValid
        {
            get { return new HandlingStatus(9, "Order number (orderid) does not correspond to the authorisation order number."); }
        }
        public static HandlingStatus ReauthRejected
        {
            get { return new HandlingStatus(10, "Re-authorisation of the transaction was rejected."); }
        }
        public static HandlingStatus NotAbleToCommunicate
        {
            get { return new HandlingStatus(11, "Not able to communicate with the acquier."); }
        }
        public static HandlingStatus ConfirmRequestError
        {
            get { return new HandlingStatus(12, "Confirm request error."); }
        }
        public static HandlingStatus CaptureAlreadyCalled
        {
            get { return new HandlingStatus(14, "Capture is called for a transaction which is pending for batch - i.e. capture was already called."); }
        }
        public static HandlingStatus BlockedByNets
        {
            get { return new HandlingStatus(15, "Capture or refund was blocked by Nets."); }
        }



        public static bool operator ==(HandlingStatus obj1, HandlingStatus obj2)
        {
            return (obj1.Code == obj2.Code);
        }

        public static bool operator !=(HandlingStatus obj1, HandlingStatus obj2)
        {
            return (obj1.Code != obj2.Code);
        }
    }
}