using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SyncedPomodoro.Controllers
{
    public class TimerController : ApiController
    {
        private const int RestingSeconds = 60*5;
        private const int TotalSprintSeconds = 60*30;

        [HttpGet]
        [Route("api/timer")]
        public TimerState Get()
        {
            var secondsInCurrentSprint = (DateTime.Now.Minute % 30) * 60 + DateTime.Now.Second;
            var resting = false;
            var secondsLeftUntilNextSprint = TotalSprintSeconds - secondsInCurrentSprint;
            if (secondsLeftUntilNextSprint < RestingSeconds)
            {
                resting = true;
            }
            else
            {
                secondsLeftUntilNextSprint -= RestingSeconds;
            }
            return new TimerState
            {
                SecondsLeft = secondsLeftUntilNextSprint,
                Resting = resting
            };
        }
    }

    public class TimerState
    {
        public int SecondsLeft { get; set; }
        public bool Resting { get; set; }
    }
}
