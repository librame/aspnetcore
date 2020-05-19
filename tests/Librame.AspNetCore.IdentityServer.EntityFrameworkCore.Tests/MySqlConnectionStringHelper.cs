using MySql.Data.MySqlClient;
using System;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions.Data.Builders;

    internal class MySqlConnectionStringHelper
    {
        public static string Validate(string connectionString)
        {
            // 手动解密
            connectionString = DataBuilderDependency.DecryptConnectionString(connectionString);

            var csb = new MySqlConnectionStringBuilder(connectionString);
            if (csb.AllowUserVariables != true || csb.UseAffectedRows)
            {
                try
                {
                    csb.AllowUserVariables = true;
                    csb.UseAffectedRows = false;
                }
                catch (MySqlException e)
                {
                    throw new InvalidOperationException("The MySql Connection string used with Pomelo.EntityFrameworkCore.MySql " +
                        "must contain \"AllowUserVariables=true;UseAffectedRows=false\"", e);
                }
            }

            return csb.ConnectionString;
        }

    }
}
