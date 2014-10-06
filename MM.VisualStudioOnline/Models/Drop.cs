using System;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class Drop
    {
        [DataMember(Name = "location")]
        public String Location { get; set; }

        [DataMember(Name = "type")]
        public String Type { get; set; }

        [DataMember(Name = "url")]
        public String Url { get; set; }
    }
}