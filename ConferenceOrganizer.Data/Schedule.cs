﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public class Schedule
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public bool Published { get; set; }
        public List<string> Rooms { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }
}