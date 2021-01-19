using System;

namespace DIBS.D2
{
    [Serializable]
    public class Language
    {
        public string Value { get; set; }
        private Language(string Value) { this.Value = Value; }



        public static Language Danish { get { return new Language("da"); } }
        public static Language Dutch { get { return new Language("nl"); } }
        public static Language English { get { return new Language("en"); } }
        public static Language Faroese { get { return new Language("fo"); } }
        public static Language Finnish { get { return new Language("fi"); } }
        public static Language French { get { return new Language("fr"); } }
        public static Language German { get { return new Language("de"); } }
        public static Language Greenlandic { get { return new Language("kl"); } }
        public static Language Italian { get { return new Language("it"); } }
        public static Language Norwegian { get { return new Language("no"); } }
        public static Language Polish { get { return new Language("pl"); } }
        public static Language Spanish { get { return new Language("es"); } }
        public static Language Swedish { get { return new Language("sv"); } }



        public static bool operator ==(Language obj1, Language obj2)
        {
            return (obj1.Value == obj2.Value);
        }

        public static bool operator !=(Language obj1, Language obj2)
        {
            return (obj1.Value != obj2.Value);
        }
    }
}