using System;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class Build
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "status")]
        public String Status { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "drop")]
        public Drop BuildDrop { get; set; }

        [DataMember(Name = "url")]
        public String Url { get; set; }
    }
}