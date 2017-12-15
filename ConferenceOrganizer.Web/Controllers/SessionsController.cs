using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("sessions")]
    public class SessionsController : Controller
    {
        IConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public SessionsController(IConferenceOrganizerDatabase conferenceOrganizerDatabase)
        {
            this.conferenceOrganizerDatabase = conferenceOrganizerDatabase;
        }

        [HttpGet]
        public IEnumerable<Session> Get()
        {
            return conferenceOrganizerDatabase.GetSessions();
        }

        [HttpGet("{id}")]
        public Session Get(string id)
        {
            return conferenceOrganizerDatabase.GetSession(id);
        }

        [HttpPost]
        public PostResponseMessage Post([FromBody]Session session)
        {
            conferenceOrganizerDatabase.PostSession(session);
            return new PostResponseMessage("Session successfully added");
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Session session)
        {
            conferenceOrganizerDatabase.PutSession(id, session);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            conferenceOrganizerDatabase.DeleteSession(id);
        }
    }
}
