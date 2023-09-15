namespace OnlineBookstore.Data
{
    public class DbConfig
    {
        public DbConfig(string connection_string)
        {
            Connection_String = connection_string;
        }

        public string Connection_String { get; set; }
        public string Database_Name { get; set; }
        public string Books_Collection_Name { get; set; }
        public string Users_Collection_Name { get; set; }
    }
}
