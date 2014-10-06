using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class ApiQueryResultCollection
    {
        [DataMember(Name = "asOf")]
        public DateTime AsOf { get; set; }

        [DataMember(Name = "query")]
        public ApiQuery Query { get; set; }

        [DataMember(Name = "results")]
        public IEnumerable<ApiQueryResult> Results { get; set; }

        [IgnoreDataMember]
        public IEnumerable<int> ResultIds
        {
            get
            {
                return Results.Select(x => x.Id);
            }
        }
    }

    [DataContract]
    public class ApiQueryResult
    {
        [DataMember(Name = "sourceId")]
        public int Id { get; set; }
    }

    [DataContract]
    public class ApiQuery
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "queryType")]
        public string QueryType { get; set; }

        [DataMember(Name = "wiql")]
        public string QueryString { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "columns")]
        public IEnumerable<string> Columns { get; set; }
    }
}