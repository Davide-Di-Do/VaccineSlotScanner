using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace vaccine_slot_scanner.Models
{
    public class AgendaResponse
    {
        [JsonPropertyName("availabilities")]
        public IEnumerable<Availability> Availabilities { get; set; }
        
        [JsonPropertyName("total")]
        public long Total { get; set; }
        
        [JsonPropertyName("nextSlot")]
        public string NextSlot { get; set; }
        
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class Availability
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        
        [JsonPropertyName("slots")]
        public IEnumerable<string> Slots { get; set; }
    }
}