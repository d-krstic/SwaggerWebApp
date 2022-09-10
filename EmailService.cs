using SwaggerWebApp.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwaggerWebApp
{
    public class EmailService : IEmailService
    {
        public EmailService() { //konstruktor se klice na vsakem API klicu ???????????
            /*for (int i = 0; i < 3; i++) {
                _emails.Add(new Email(i, "naslov" + i + "@gmai.com"));
            }

            using (StreamWriter sw = File.AppendText("test.txt"))
            {
                sw.WriteLine("This");
            }*/
        }

        public List<Email> Delete(int eId) {
            SqlConn.data.RemoveAll(x => x.Id == eId);
            return SqlConn.data;
        }

        public Email Get(string m) { //pogleda ce je email v cache, ce je, ga pred returnom iz cache izbrise saj predvidevam da uporabnik nebo ponovno iskal emaila za katerega je ze dobil rezultat
            Email ret = SqlConn.checkCache(m);
            if (ret == null) {
                ret = SqlConn.data.SingleOrDefault(x => x.Mail == m);
            }
            else {
                SqlConn.removeFromCache(ret);
            }
            return ret;
        }

        public List<Email> Gets() {
            return SqlConn.data; 
        }

        public List<Email> Save(Email email) {
            SqlConn.data.Add(email);
            return SqlConn.data;
        }

        public List<Email> Update() { //saves current data (one in SqlConn.data) to DB (file for now) and updates that DB
            SqlConn.save();
            return SqlConn.data;
        }
    }
}
