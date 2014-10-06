using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class BuildRequest
    {
        [DataMember(Name = "definition")]
        public Definition Definition { get; set; }

        [DataMember(Name = "reason")]
        public String Reason { get; set; }

        [DataMember(Name = "priority")]
        public String Priority { get; set; }

        [DataMember(Name = "queuePosition")]
        public int QueuePosition { get; set; }

        [DataMember(Name = "queueTime")]
        public DateTime QueueTime { get; set; }

        [DataMember(Name = "requestedBy")]
        public RequestedBy RequestedBy { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "status")]
        public String Status { get; set; }

        [DataMember(Name = "url")]
        public String Url { get; set; }

        [DataMember(Name = "builds")]
        public IEnumerable<Build> Builds { get; set; }
    }
}