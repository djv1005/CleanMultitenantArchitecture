using CleanMultitenantArchitecture.Domain.Entities;
using System;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace CleanMultitenantArchitecture.Infraestructure.Services
{
    public class MigrationTenantScriptService : IMigrationTenantScriptService
    {
        private readonly IConfiguration _config;
        public MigrationTenantScriptService(IConfiguration config)
        {
            _config = config;
        }
        //THIS SCRIPT SHOULD BE CALLED WHEN A NEW ORGANIZATION IS ADDED
        public void runScript(Organization organization)
        {
            string path = _config.GetSection("Params")["path"]?.ToString();

            path = path ?? "C:\\Users\\djv1005\\source\\repos\\CleanMultitenantArchitecture" +
                "\\CleanMultitenantArchitecture.API\\TenantMigrationBashScript.sh";

            string scriptContent = File.ReadAllText(path);
            scriptContent = scriptContent.Replace("{{Database_Tenant_Name}}", organization.Name );
            scriptContent = scriptContent.Replace("{{User_Db_Tenant}}", organization.User);
            scriptContent = scriptContent.Replace("{{Pwd_Db_Tenant}}", organization.Password);
            scriptContent = scriptContent.Replace("{{Server}}", organization.Server);


            string tempScriptPath = Path.GetTempFileName();
            File.WriteAllText(tempScriptPath, scriptContent);

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = tempScriptPath,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            File.Delete(tempScriptPath);



        }

    }
}
