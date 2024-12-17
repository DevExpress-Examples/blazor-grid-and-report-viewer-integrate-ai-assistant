namespace DevExpress.AI.Samples.Blazor.Data {
    public partial class User {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}