using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerWebApp.IServices
{
    public interface IEmailService
    {
        List<Email> Gets();
        Email Get(string m);
        List<Email> Save(Email email);
        List<Email> Delete(int eId);
        List<Email> Update();
    }
}
