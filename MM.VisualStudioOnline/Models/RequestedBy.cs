using System;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class RequestedBy
    {
        [DataMember(Name = "displayName")]
        public String DisplayName { get; set; }

        [DataMember(Name = "uniqueName")]
        public String UniqueName { get; set; }
    }
}