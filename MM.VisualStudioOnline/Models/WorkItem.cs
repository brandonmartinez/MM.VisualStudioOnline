using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;

namespace MM.VisualStudioOnline.Models
{
    [DataContract]
    public class WorkItem
    {
        public WorkItem()
        {
            Fields = new List<WorkItemField>();
            Resources = new List<WorkItemResource>();
            Attachments = new List<Attachment>();
            AdditionalMetaData = new ExpandoObject();
        }

        [DataMember(Name = "updatesUrl")]
        public string UpdateUrl { get; set; }

        [DataMember(Name = "webUrl")]
        public string WebUrl { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "rev")]
        public int Revision { get; set; }

        [DataMember(Name = "resourceLinks")]
        public List<WorkItemResource> Resources { get; set; }

        [IgnoreDataMember]
        public List<Attachment> Attachments { get; set; }

        [IgnoreDataMember]
        public dynamic AdditionalMetaData { get; set; }

        [IgnoreDataMember]
        public long? AreaId
        {
            get
            {
                return getFieldValue("System.AreaId");
            }
            set
            {
                setFieldValue("System.AreaId", value);
            }
        }

        [IgnoreDataMember]
        public double? BacklogPriority
        {
            get
            {
                return getFieldValue("Microsoft.VSTS.Common.BacklogPriority");
            }
            set
            {
                setFieldValue("Microsoft.VSTS.Common.BacklogPriority", value);
            }
        }

        [IgnoreDataMember]
        public long? IterationId
        {
            get
            {
                return getFieldValue("System.IterationId");
            }
            set
            {
                setFieldValue("System.IterationId", value);
            }
        }

        [IgnoreDataMember]
        public decimal? RemainingWork
        {
            get
            {
                return getFieldValue("Microsoft.VSTS.Scheduling.RemainingWork");
            }
            set
            {
                setFieldValue("Microsoft.VSTS.Scheduling.RemainingWork", value);
            }
        }

        [IgnoreDataMember]
        public string AreaPath
        {
            get
            {
                return getFieldValue("System.AreaPath");
            }
            set
            {
                setFieldValue("System.AreaPath", value);
            }
        }

        [IgnoreDataMember]
        public string AreaLevel1
        {
            get
            {
                return getFieldValue("System.AreaLevel1");
            }
            set
            {
                setFieldValue("System.AreaLevel1", value);
            }
        }

        [IgnoreDataMember]
        public string AreaLevel2
        {
            get
            {
                return getFieldValue("System.AreaLevel2");
            }
            set
            {
                setFieldValue("System.AreaLevel2", value);
            }
        }

        [IgnoreDataMember]
        public string IterationPath
        {
            get
            {
                return getFieldValue("System.IterationPath");
            }
            set
            {
                setFieldValue("System.IterationPath", value);
            }
        }

        [IgnoreDataMember]
        public string IterationLevel1
        {
            get
            {
                return getFieldValue("System.IterationLevel1");
            }
            set
            {
                setFieldValue("System.IterationLevel1", value);
            }
        }

        [IgnoreDataMember]
        public string IterationLevel2
        {
            get
            {
                return getFieldValue("System.IterationLevel2");
            }
            set
            {
                setFieldValue("System.IterationLevel2", value);
            }
        }

        [IgnoreDataMember]
        public string IterationLevel3
        {
            get
            {
                return getFieldValue("System.IterationLevel3");
            }
            set
            {
                setFieldValue("System.IterationLevel3", value);
            }
        }

        [IgnoreDataMember]
        public string WorkItemType
        {
            get
            {
                return getFieldValue("System.WorkItemType");
            }
            set
            {
                setFieldValue("System.WorkItemType", value);
            }
        }

        [IgnoreDataMember]
        public string State
        {
            get
            {
                return getFieldValue("System.State");
            }
            set
            {
                setFieldValue("System.State", value);
            }
        }

        [IgnoreDataMember]
        public string Reason
        {
            get
            {
                return getFieldValue("System.Reason");
            }
            set
            {
                setFieldValue("System.Reason", value);
            }
        }

        [IgnoreDataMember]
        public string AssignedTo
        {
            get
            {
                return getFieldValue("System.AssignedTo");
            }
            set
            {
                setFieldValue("System.AssignedTo", value);
            }
        }

        [IgnoreDataMember]
        public string CreatedBy
        {
            get
            {
                return getFieldValue("System.CreatedBy");
            }
            set
            {
                setFieldValue("System.CreatedBy", value);
            }
        }

        [IgnoreDataMember]
        public string Title
        {
            get
            {
                return getFieldValue("System.Title");
            }
            set
            {
                setFieldValue("System.Title", value);
            }
        }

        [IgnoreDataMember]
        public string Description
        {
            get
            {
                return getFieldValue("System.Description");
            }
            set
            {
                setFieldValue("System.Description", value);
            }
        }

        [IgnoreDataMember]
        public DateTime? CreatedDate
        {
            get
            {
                return getFieldValue("System.CreatedDate");
            }
            set
            {
                setFieldValue("System.CreatedDate", value);
            }
        }

        [IgnoreDataMember]
        public DateTime? ClosedDate
        {
            get
            {
                return getFieldValue("Microsoft.VSTS.Common.ClosedDate");
            }
            set
            {
                setFieldValue("Microsoft.VSTS.Common.ClosedDate", value);
            }
        }

        [IgnoreDataMember]
        public string Project
        {
            get
            {
                return getFieldValue("System.NodeName");
            }
            set
            {
                setFieldValue("System.NodeName", value);
            }
        }

        [DataMember(Name = "fields")]
        public List<WorkItemField> Fields { get; set; }

        [IgnoreDataMember]
        public WorkItemField this[string referenceName]
        {
            get
            {
                if(Fields == null)
                {
                    return null;
                }

                var field =
                    Fields.FirstOrDefault(x => x.ReferenceName.ToLowerInvariant() == referenceName.ToLowerInvariant());

                return field;
            }
        }

        private dynamic getFieldValue(string fieldName)
        {
            var field = this[fieldName];
            return field != null ? field.Value : null;
        }

        private void setFieldValue(string fieldName, dynamic value)
        {
            var field = this[fieldName];
            if(field == null)
            {
                field = new WorkItemField
                {
                    ReferenceName = fieldName,
                    Value = value
                };
                Fields.Add(field);
            }
            else
            {
                field.Value = value;
            }
        }
    }

    [DataContract]
    public class WorkItemField
    {
        [DataMember(Name = "value")]
        public dynamic Value;

        public WorkItemField()
        {
            Metadata = new WorkItemFieldMetadata();
        }

        [DataMember(Name = "field")]
        public WorkItemFieldMetadata Metadata { get; set; }

        [IgnoreDataMember]
        public int? Id
        {
            get
            {
                return Metadata != null ? Metadata.Id : null;
            }
            set
            {
                if(Metadata != null)
                {
                    Metadata.Id = value;
                }
            }
        }

        [IgnoreDataMember]
        public string Name
        {
            get
            {
                return Metadata != null ? Metadata.Name : null;
            }
            set
            {
                if(Metadata != null)
                {
                    Metadata.Name = value;
                }
            }
        }

        [IgnoreDataMember]
        public string ReferenceName
        {
            get
            {
                return Metadata != null ? Metadata.ReferenceName : null;
            }
            set
            {
                if(Metadata != null)
                {
                    Metadata.ReferenceName = value;
                }
            }
        }
    }

    [DataContract]
    public class WorkItemFieldMetadata
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "refName")]
        public string ReferenceName { get; set; }
    }

    [DataContract]
    public class UpdateWorkItem
    {
        public UpdateWorkItem()
        {
            Fields = new List<UpdateWorkItemField>();
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "rev")]
        public int Revision { get; set; }

        [DataMember(Name = "fields")]
        public List<UpdateWorkItemField> Fields { get; set; }
    }

    [DataContract]
    public class UpdateWorkItemField
    {
        [DataMember(Name = "value")]
        public dynamic Value;

        public UpdateWorkItemField()
        {
            Metadata = new UpdateWorkItemFieldMetadata();
        }

        [DataMember(Name = "field")]
        public UpdateWorkItemFieldMetadata Metadata { get; set; }
    }

    [DataContract]
    public class UpdateWorkItemFieldMetadata
    {
        [DataMember(Name = "refName")]
        public string ReferenceName { get; set; }
    }

    [DataContract]
    public class WorkItemResourceSource
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "webUrl")]
        public string WebUrl { get; set; }
    }

    [DataContract]
    public class WorkItemResource
    {
        [DataMember(Name = "resourceId")]
        public int Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "location")]
        public Guid Location { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "creationDate")]
        public DateTime CreationDate { get; set; }

        [DataMember(Name = "lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "source")]
        public WorkItemResourceSource Source { get; set; }
    }
}