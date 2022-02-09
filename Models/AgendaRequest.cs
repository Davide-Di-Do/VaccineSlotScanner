using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace vaccine_slot_scanner.Models
{
    internal class AgendaRequest
    {
        [JsonPropertyName("courseTypeId")]
        public long? CourseTypeId { get; set; }

        [JsonPropertyName("bathId")]
        public long? BathId{ get; set; }

        [JsonPropertyName("courseId")]
        public long CourseId { get; set; }

        [JsonPropertyName("from")]
        public string FromDate { get; set; }

        [JsonPropertyName("to")]
        public string ToDate { get; set; }
    }
}
