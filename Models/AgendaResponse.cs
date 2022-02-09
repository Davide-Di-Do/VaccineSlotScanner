using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace vaccine_slot_scanner.Models
{
    public class AgendaResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("minPrice")]
        public double MinPrice { get; set; }
        
        [JsonPropertyName("maxPrice")]
        public double MaxPrice { get; set; }
        
        [JsonPropertyName("shortText")]
        public string ShortText { get; set; }
        
        [JsonPropertyName("longText")]
        public string LongText { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public AgendaType Type { get; set; }

        [JsonPropertyName("requirements")]
        public string Requirements { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("imageURL")]
        public string ImageURL { get; set; }

        [JsonPropertyName("terms")]
        public IEnumerable<AgendaTerm> Terms { get; set; }
    }

    public class AgendaType
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("shortText")]
        public String ShortText { get; set; }

        [JsonPropertyName("longText")]
        public String LongText { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class AgendaTerm
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("courseId")]
        public long CourseId { get; set; }

        [JsonPropertyName("startDate")]
        public long StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public long EndDate { get; set; }

        [JsonPropertyName("numberOfDates")]
        public long NumberOfDates { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; } //FULLY_BOOKED

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("fromAge")]
        public long? FromAge { get; set; }

        [JsonPropertyName("toAge")]
        public long? ToAge { get; set; }

        [JsonPropertyName("bathId")]
        public long BathId { get; set; }

        [JsonPropertyName("shortText")]
        public string ShortText { get; set; }

        [JsonPropertyName("longText")]
        public string LongText { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("termDates")]
        public IEnumerable<AgendaTermDate> TermDates { get; set; }
    }

    public class AgendaTermDate
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("date")]
        public long Date { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("shortText")]
        public string ShortText { get; set; }

        [JsonPropertyName("longText")]
        public string LongText { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}