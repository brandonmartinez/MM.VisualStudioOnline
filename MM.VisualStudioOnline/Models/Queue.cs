using System;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class Queue
    {
        [DataMember(Name = "url")]
        public String Url;

        [DataMember(Name = "queueType")]
        public String QueueType { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }
    }
}