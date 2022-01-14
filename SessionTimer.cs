using Authorization;
using System;
using System.Linq;
using Timer = System.Timers.Timer;

namespace ControlStudy
{
    class SessionTimer //Таймер сессии
    {
        public DateTime startTime;
        public DateTime endTime;
        readonly Timer timer = new Timer();
        readonly ControlStudyEntities userContext = new ControlStudyEntities();

        public SessionTimer() //Включение таймера
        {
            timer.Interval = 1000;
            timer.Start();
            startTime = DateTime.Now;
        }

        public void SaveTimeSession(string login) // Сохранение таймера
        {
            endTime = DateTime.Now;
            TimeSpan timeSpan = endTime - startTime;
            DateTime Date = DateTime.Now;

            int codePerson = userContext.Users.Where(c => c.LoginUser == login).Select(c => c.IdPerson).FirstOrDefault();

            Session sessionUser = new Session
            {
                DateSession = Date,
                IdPerson = codePerson,
                Time = Convert.ToString(timeSpan.Hours + ":" + timeSpan.Minutes + ":" + timeSpan.Seconds)
            };

            userContext.Sessions.Add(sessionUser);
            userContext.SaveChanges();
            timer.Stop();
        }
    }
}
