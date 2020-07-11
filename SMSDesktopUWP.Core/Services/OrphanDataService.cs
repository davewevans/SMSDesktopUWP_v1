using SMSDesktopUWP.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace SMSDesktopUWP.Core.Services
{
    public static class OrphanDataService
    {
        #region oldCode
        //public static IEnumerable<Orphan> AllOrphans()
        //{
        //    // List orders from all companies
        //    var orphans = (IEnumerable<Orphan>)GetAllOrphans();
        //    return orphans;
        //}

        //private static IEnumerable<Orphan> GetAllOrphans()
        //{
        //    using (SMSContext inContext = new SMSContext())
        //    {
        //        var inOrphanRecs = (from r in inContext.Orphans select r);

        //        return inOrphanRecs;
        //    }
        //}

        //public static async Task<IEnumerable<Orphan>> GetMasterDetailDataAsync()
        //{
        //    await Task.CompletedTask;
        //    return AllOrphans();
        //}
        #endregion oldCode

        private static string GetConnectionString()
        {
            // Attempt to get the connection string from a config file
            // Learn more about specifying the connection string in a config file at https://docs.microsoft.com/dotnet/api/system.configuration.configurationmanager?view=netframework-4.7.2
            var conStr = ConfigurationManager.ConnectionStrings["SMSCloudConnectionString"]?.ConnectionString;

            if (!string.IsNullOrWhiteSpace(conStr))
            {
                return conStr;
            }
            else
            {
                // If no connection string is specified in a config file, use this as a fallback.
                return @"Data Source=*server*\*instance*;Initial Catalog=*dbname*;Integrated Security=SSPI";
            }
        }

        public static async Task<IEnumerable<Orphan>> AllOrphans()
        {
            // Using a hard-coded SQL statement for now to make it simpler.  Will need to either use API (preferred)
            // or stored procedure

            const string getOrphanQuery = @"SELECT * FROM dbo.Orphans";

            var orphanList = new List<Orphan>();

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = getOrphanQuery;

                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    //Orphan Data
                                    var orphanID = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                                    var firstName = reader.GetString(1);
                                    var middleName = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty;
                                    var lastName = reader.GetString(3);
                                    var fullName = reader.GetString(4);
                                    var gender = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty;
                                    var dateOfBirth = !reader.IsDBNull(6) ? reader.GetDateTime(6) : default(DateTime);
                                    var lcmStatus = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty;
                                    var profileNumber = !reader.IsDBNull(8) ? reader.GetString(8) : string.Empty;
                                    var entryDate = !reader.IsDBNull(9) ? reader.GetDateTime(9) : default(DateTime);
                                    var guardianID = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;

                                    Orphan inOrphan = new Orphan()
                                    {
                                        OrphanID = orphanID,
                                        FirstName = firstName,
                                        MiddleName = middleName,
                                        LastName = lastName,
                                        FullName = fullName,
                                        Gender = gender,
                                        DateOfBirth = dateOfBirth,
                                        LCMStatus = lcmStatus,
                                        ProfileNumber = profileNumber,
                                        EntryDate = entryDate,
                                        GuardianID = guardianID
                                    };

                                    // Add to the List<>
                                    orphanList.Add(inOrphan);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception eSql)
            {
                // Your code may benefit from more robust error handling or logging.
                // This logging is just a reminder that you should handle exceptions when connecting to remote data.
                System.Diagnostics.Debug.WriteLine($"Exception: {eSql.Message} {eSql.InnerException?.Message}");
            }

            return orphanList;

        }


    }
}
