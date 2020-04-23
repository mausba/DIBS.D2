namespace DIBS.D2
{
    /// <summary>
    /// Response returned by Hosted Payment Window.
    /// </summary>
    public class HostedPaymentResponse
    {
        public string Acquirer { get; set; }

        public string Agreement { get; set; }

        public long Amount { get; set; }

        public string ApprovalCode { get; set; }

        public string AuthKey { get; set; }

        public string CardCountry { get; set; }

        public string CardExpDate { get; set; }

        public string CardId { get; set; }

        public string CardNoMask { get; set; }

        public string CardPrefix { get; set; }

        public string Checksum { get; set; }

        public string Currency { get; set; }

        public int Fee { get; set; }

        public string OrderId { get; set; }

        public string PayType { get; set; }

        public string Severity { get; set; }

        public string StatusCode { get; set; }

        public string Suspect { get; set; }

        public string ThreeDStatus { get; set; }

        public long Transact { get; set; }
    }
}
