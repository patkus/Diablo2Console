using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Diablo2Console.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
    }
}
