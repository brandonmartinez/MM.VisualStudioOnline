using System;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class BuildDefinition
    {
        [DataMember(Name = "uri")]
        public String Uri { get; set; }

        [DataMember(Name = "queue")]
        public Queue Queue { get; set; }

        [DataMember(Name = "triggerType")]
        public String TriggerType { get; set; }

        [DataMember(Name = "defaultDropLocation")]
        public String DefaultDropLocation { get; set; }

        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "definitionType")]
        public String DefinitionType { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "url")]
        public String Url { get; set; }
    }
}