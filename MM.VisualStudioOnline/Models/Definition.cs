using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class Definition
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}