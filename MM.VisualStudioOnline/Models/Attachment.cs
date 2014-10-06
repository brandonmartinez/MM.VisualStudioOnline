using System;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class Attachment
    {
        [IgnoreDataMember]
        public string Project { get; set; }

        [IgnoreDataMember]
        public string Area { get; set; }

        [IgnoreDataMember]
        public string Filename { get; set; }

        [IgnoreDataMember]
        public string Text { get; set; }

        [IgnoreDataMember]
        public byte[] Data { get; set; }

        [DataMember(Name = "location")]
        public Guid Id { get; set; }

        [DataMember(Name = "length")]
        public long Length { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }
    }
}