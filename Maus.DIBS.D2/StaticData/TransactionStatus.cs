using System;

namespace DIBS.D2
{
    [Serializable]
    public class TransactionStatus
    {
        public int Code { get; set; }
        public string Description { get; set; }

        private TransactionStatus(int Code, string Description)
        {
            this.Code = Code;
            this.Description = Description;
        }

        public TransactionStatus(int Code)
        {
            this.Code = Code;

            switch (Code)
            {
                case 0:
                    this.Description = "Transaction inserted (not approved).";
                    break;
                case 1:
                    this.Description = "Declined.";
                    break;
                case 2:
                    this.Description = "Authorization approved.";
                    break;
                case 3:
                    this.Description = "Capture sent to acquirer.";
                    break;
                case 4:
                    this.Description = "Capture declined by acquirer.";
                    break;
                case 5:
                    this.Description = "Capture completed.";
                    break;
                case 6:
                    this.Description = "Authorization deleted.";
                    break;
                case 7:
                    this.Description = "Capture balanced.";
                    break;
                case 8:
                    this.Description = "Partially refunded and balanced.";
                    break;
                case 9:
                    this.Description = "Refund sent to acquirer.";
                    break;
                case 10:
                    this.Description = "Refund declined.";
                    break;
                case 11:
                    this.Description = "Refund completed.";
                    break;
                case 12:
                    this.Description = "Capture pending.";
                    break;
                case 13:
                    this.Description = "Ticket transaction.";
                    break;
                case 14:
                    this.Description = "Deleted ticket transaction.";
                    break;
                case 15:
                    this.Description = "Refund pending.";
                    break;
                case 16:
                    this.Description = "Waiting for shop approval.";
                    break;
                case 17:
                    this.Description = "Declined by DIBS.";
                    break;
                case 18:
                    this.Description = "Multicap transaction open.";
                    break;
                case 19:
                    this.Description = "Multicap transaction closed.";
                    break;
                default:
                    this.Code = -1;
                    this.Description = "Unknown status.";
                    break;
            }
        }



        public static TransactionStatus Unknown
        {
            get { return new TransactionStatus(-1, "Unknown status."); }
        }
        public static TransactionStatus Inserted
        {
            get { return new TransactionStatus(0, "Transaction inserted (not approved)."); }
        }
        public static TransactionStatus Declined
        {
            get { return new TransactionStatus(1, "Declined."); }
        }
        public static TransactionStatus AuthorizationApproved
        {
            get { return new TransactionStatus(2, "Authorization approved."); }
        }
        public static TransactionStatus CaptureSent
        {
            get { return new TransactionStatus(3, "Capture sent to acquirer."); }
        }
        public static TransactionStatus CaptureDeclined
        {
            get { return new TransactionStatus(4, "Capture declined by acquirer."); }
        }
        public static TransactionStatus CaptureCompleted
        {
            get { return new TransactionStatus(5, "Capture completed."); }
        }
        public static TransactionStatus AuthorizationDeleted
        {
            get { return new TransactionStatus(6, "Authorization deleted."); }
        }
        public static TransactionStatus CaptureBalanced
        {
            get { return new TransactionStatus(7, "Capture balanced."); }
        }
        public static TransactionStatus PartiallyRefunded
        {
            get { return new TransactionStatus(8, "Partially refunded and balanced."); }
        }
        public static TransactionStatus RefundSent
        {
            get { return new TransactionStatus(9, "Refund sent to acquirer."); }
        }
        public static TransactionStatus RefundDeclined
        {
            get { return new TransactionStatus(10, "Refund declined."); }
        }
        public static TransactionStatus RefundCompleted
        {
            get { return new TransactionStatus(11, "Refund completed."); }
        }
        public static TransactionStatus CapturePending
        {
            get { return new TransactionStatus(12, "Capture pending."); }
        }
        public static TransactionStatus TicketTransaction
        {
            get { return new TransactionStatus(13, "Ticket transaction."); }
        }
        public static TransactionStatus DeletedTicket
        {
            get { return new TransactionStatus(14, "Deleted ticket transaction."); }
        }
        public static TransactionStatus RefundPending
        {
            get { return new TransactionStatus(15, "Refund pending."); }
        }
        public static TransactionStatus ShopApproval
        {
            get { return new TransactionStatus(16, "Waiting for shop approval."); }
        }
        public static TransactionStatus DeclinedByDIBS
        {
            get { return new TransactionStatus(17, "Declined by DIBS."); }
        }
        public static TransactionStatus MulticapOpen
        {
            get { return new TransactionStatus(18, "Multicap transaction open."); }
        }
        public static TransactionStatus MulticapClosed
        {
            get { return new TransactionStatus(19, "Multicap transaction closed."); }
        }



        public static bool operator ==(TransactionStatus obj1, TransactionStatus obj2)
        {
            return (obj1.Code == obj2.Code);
        }

        public static bool operator !=(TransactionStatus obj1, TransactionStatus obj2)
        {
            return (obj1.Code != obj2.Code);
        }
    }
}
