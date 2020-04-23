namespace DIBS.D2
{
    public class Currency
    {
        public string Code { get; set; }
        public string Value { get; set; }

        private Currency(string Code, string Value) 
        {
            this.Code = Code;
            this.Value = Value;
        }

        public Currency(string Value)
        {
            this.Value = Value;

            switch (Value)
            {
                case "977":
                    this.Code = "BAM";
                    break;
                case "208":
                    this.Code = "DKK";
                    break;
                case "978":
                    this.Code = "EUR";
                    break;
                case "840":
                    this.Code = "USD";
                    break;
                default:
                    break;
            }
        }
        


        public static Currency BAM { get { return new Currency("BAM", "977"); } }
        public static Currency DKK { get { return new Currency("DKK", "208"); } }
        public static Currency EUR { get { return new Currency("EUR", "978"); } }
        public static Currency USD { get { return new Currency("USD", "840"); } }



        public static bool operator ==(Currency obj1, Currency obj2)
        {
            return (obj1.Value == obj2.Value || obj1.Code == obj2.Code);
        }

        public static bool operator !=(Currency obj1, Currency obj2)
        {
            return (obj1.Value != obj2.Value && obj1.Code != obj2.Code);
        }
    }
}
