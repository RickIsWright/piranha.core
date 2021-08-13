#if DEBUG
/*
 * Copyright (c) .NET Foundation and Contributors
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * http://github.com/piranhacms/piranha.core
 *
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Piranha.Data.EF.MySql
{
   
    /// <summary>
    /// Factory for creating a db context. Only used in dev mode
    /// when creating migrations.
    /// </summary>
    [NoCoverage]
    public class DbFactory : IDesignTimeDbContextFactory<MySqlDb>
    {
        /// <summary>
        /// Creates a new db context.
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns>The db context</returns>
        public MySqlDb CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MySqlDb>();
            string mySqlConnectionString = "Server=localhost; Port=8889; Database=piranha; User=root; Password=root;" ;
            //optional: var serverVersion = new MySqlServerVersion(new System.Version(8, 0, 21));
            builder.UseMySql(ServerVersion.AutoDetect(mySqlConnectionString));

            return new MySqlDb(builder.Options);
        }
    }
}
#endif