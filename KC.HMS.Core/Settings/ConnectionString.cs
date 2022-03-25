namespace KC.HMS.Core.Settings
{
    public sealed class ConnectionString
    {
        public ConnectionString(string value) => Value = value;

        public string Value { get; }

        public string Provider { get; set; }
    }
}
