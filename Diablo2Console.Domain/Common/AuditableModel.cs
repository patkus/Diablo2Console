using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Diablo2Console.Domain.Common
{
    public class AuditableModel
    {
        [XmlIgnore]
        public int CreatedById { get; set; }
        [XmlIgnore]
        public DateTime CreatedDateTime { get; set; }
        [XmlIgnore]
        public int? ModifiedById { get; set; }
        [XmlIgnore]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
