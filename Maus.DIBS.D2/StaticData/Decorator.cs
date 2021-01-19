using System;

namespace DIBS.D2
{
    [Serializable]
    public class Decorator
    {
        public string Value { get; set; }
        private Decorator(string Value) { this.Value = Value; }



        public static Decorator Default { get { return new Decorator("default"); } }
        public static Decorator Basal { get { return new Decorator("basal"); } }
        public static Decorator Rich { get { return new Decorator("rich"); } }
        public static Decorator Responsive { get { return new Decorator("responsive"); } }
    }
}