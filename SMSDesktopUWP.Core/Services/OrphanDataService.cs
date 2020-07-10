using SMSDesktopUWP.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SMSDesktopUWP.Core.Services
{
    public class OrphanDataService
    {
        public static async Task<IEnumerable<Orphan>> AllOrphans()
        {
            // List orders from all companies
            var companies = await GetAllOrphans();
            return companies;
        }


        private static async Task<IEnumerable<Orphan>> GetAllOrphans()
        {
            using (SMSContext inContext = new SMSContext())
            {
                var inOrphanRecs = (from r in inContext.Orphans select r);

                return inOrphanRecs;
            }
        }


    }
}
