using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    /// <summary>
    ///     A common wrapper for Visual Studio Online API Results, as they are generally presented in this wrapper
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    [DataContract]
    public class ApiCollection<T>
    {
        [DataMember(Name = "value")]
        public IEnumerable<T> Value { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}