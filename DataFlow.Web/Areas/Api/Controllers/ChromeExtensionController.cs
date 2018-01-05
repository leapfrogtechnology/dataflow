using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataFlow.Common.DAL;
using DataFlow.Models;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using DataFlow.Web.Areas.Api.Models;

namespace DataFlow.Web.Areas.Api.Controllers
{
    [AllowAnonymous]
    public class ChromeExtensionController : ApiController
    {
        [HttpPost]
        [Route("api/register")]
        public HttpResponseMessage Register([FromBody] AgentRegistration registration)
        {
            try
            {
                using (var ctx = new DataFlowDbContext())
                {
                    AgentChrome chrome = ctx.AgentChromes.FirstOrDefault(ac => ac.AgentUuid == registration.uuid);

                    if (chrome != null)
                        registration.token = chrome.AccessToken;
                    else
                    {
                        chrome = new AgentChrome();
                        chrome.AgentUuid = registration.uuid;
                        chrome.AccessToken = Guid.NewGuid();
                        chrome.Created = DateTime.Now;
                        ctx.AgentChromes.Add(chrome);
                        ctx.SaveChanges();
                        registration.token = chrome.AccessToken;
                        //TODO: Log success create
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO:  Log error message

                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return Request.CreateResponse((HttpStatusCode)200, registration);
        }

        [HttpPost]
        [Route("api/agents")]
        public List<AgentResponse> Agents([FromBody] AgentRegistration registration)
        {
            List<AgentResponse> response = new List<AgentResponse>();

            try
            {
                using (var ctx = new DataFlowDbContext())
                {
                    AgentChrome chrome = ctx.AgentChromes.Where(ac => ac.AgentUuid == registration.uuid && ac.AccessToken == registration.token).FirstOrDefault();
                    List<AgentAgentChrome> chromes = ctx.AgentAgentChromes.Where(aac => aac.AgentChromeId == chrome.Id).Include(aac => aac.Agent).Include("Agent.AgentSchedules").ToList();


                    foreach (var chm in chromes)
                    {
                        AgentResponse responseAgent = new AgentResponse();
                        responseAgent.agent_id = chm.AgentId;
                        responseAgent.action = chm.Agent.AgentAction;
                        responseAgent.url = chm.Agent.Url;
                        responseAgent.loginUrl = chm.Agent.LoginUrl;
                        responseAgent.schedule = new List<AgentScheduleResponse>();

                        foreach (var sch in chm.Agent.AgentSchedules)
                        {
                            AgentScheduleResponse schedule = new AgentScheduleResponse(sch.Day, sch.Hour, sch.Minute);
                            responseAgent.schedule.Add(schedule);
                        }

                        response.Add(responseAgent);
                    }
                    
                }

            } catch (System.Exception ex)
            {
                //TODO:  Log exception

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        [HttpPost]
        [Route("api/data")]
        public HttpResponseMessage Data()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpPost]
        [Route("api/log")]
        public HttpResponseMessage Log()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
