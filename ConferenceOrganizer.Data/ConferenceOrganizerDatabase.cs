﻿using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        CFP GetCFPStatus();
        IEnumerable<string> GetSpeakers();
        IEnumerable<Proposal> GetProposalsBySpeaker(string name);
        void PutCFP(string id, CFP cfp);
        IEnumerable<Proposal> GetProposals();
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void PutProposal(string id, Proposal proposal);
        void DeleteProposal(string id);
    }

    public class ConferenceOrganizerDatabase : IConferenceOrganizerDatabase
    {
        IMongoCollection<Proposal> collection;
        IMongoDatabase database;

        public ConferenceOrganizerDatabase()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<Proposal>("proposals");
        }

        public IEnumerable<string> GetSpeakers()
        {
            var result = collection.AsQueryable<Proposal>()
                                   .Select(p => p.speakerName)
                                   .Distinct();
            return result;
        }

        public IEnumerable<Proposal> GetProposalsBySpeaker(string name)
        {
            var filterName = name.Replace("-", " ");
            var filter = Builders<Proposal>.Filter.Eq("speakerName", filterName);
            return collection.Find(filter).ToListAsync().Result;
        }

        public void PutCFP(string id, CFP cfp)
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            var filter = Builders<CFP>.Filter.Eq("id", id);
            cfpCollection.FindOneAndReplace(filter, cfp);
        }

        public CFP GetCFPStatus()
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            return cfpCollection.Find(x => true).ToListAsync().Result[0];
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return collection.Find(x => true).ToListAsync().Result;
        }

        public Proposal FindProposal(string id)
        {
            return collection.Find(x => x.id == id).First();
        }

        public void PostProposal(Proposal proposal)
        {
            collection.InsertOne(proposal);
        }

        public void PutProposal(string id, Proposal proposal)
        {
            var filter = Builders<Proposal>.Filter.Eq("id", id);
            collection.FindOneAndReplace(filter, proposal);
        }

        public void DeleteProposal(string id)
        {
            collection.DeleteOne(X=> X.id == id);
        }
    }
}
