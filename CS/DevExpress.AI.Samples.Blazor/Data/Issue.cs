﻿namespace DevExpress.AI.Samples.Blazor.Data {
    public enum IssueType { Request, Bug }
    public enum IssueStatus { New, Postponed, Fixed, Rejected }
    public enum IssuePriority { Low, Medium, High }
    public partial class Issue {
        public long ID { get; set; }
        public string Name { get; set; }
        public IssueType Type { get; set; }
        public Nullable<long> ProjectID { get; set; }
        public IssuePriority Priority { get; set; }
        public IssueStatus Status { get; set; }
        public Nullable<long> CreatorID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> FixedDate { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
    }
}