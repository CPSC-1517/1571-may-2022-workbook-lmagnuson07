using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class RegionServices
    {
        #region setup the context connection variable and class constructor 
        //variable to hold an instance of the context class
        private readonly WestWindContext _context;

        // constructor to create an instance of the registered context class
        internal RegionServices(WestWindContext regcontext)
        {
            _context = regcontext;
        }
        #endregion

        #region Service: Queries

        // obtain all the data within the sql entity to display on the web page. This means a collection (List<T>) will be genereated and returned by the following query (method). The query will be called from outside the class library project, therefore the method access level MUST be public
        public List<Region> Region_List()
        {
            ///////////////////////////////////////////////////////////////////
            ///Example coding 1
            // var datatype is resolved during execution. 

            //var info = _context.Regions;

            // return the retrieved database data to whatever called this method. NOTE: the data collection MUST be returned as a List<T> 
            // .ToList() will convert the collection into a List<T>
            // 
            //return info.ToList(); 

            /////////////////////////////////////////////////////////////////
            ///Example coding 2
            // The collection datatype is "strongly" typed at creation time. Storgly typed data is resolved at compile time
            // the datatype from a query will be either IEnumerable<T> or IQueryable<T>
            // data within memory(local memory) of your program IEnumerable
            // data within memory of sql(database) IQueryable

            //IEnumerable<Region> info = _context.Regions;
            //return info.ToList();

            /////////////////////////////////////////////////////////////////
            ///Example coding 3
            // return the data converted all within one statement 
            return _context.Regions.ToList();
        }

        // looking up a record on a table via the primary key value 
        public Region Region_GetByID(int id)
        {
            // Example 1 using the extension method .Find(pkeyvalue)
            //return _context.Regions.Find(id);

            // example 2 uses the Linq method syntax 
            // Linq: Language Integrated query (more in DMIT2018)
            // what we are using is Linq to Entities
            // default returns 0, 1, more records.
            // in our case, since we are looking up by the pkey we expect only one record (or none) to be returned
            // add an additional extension which specifies the number of expected records to be returned: .FirstOrDefault()
            // First the first record found that matches the filter (where) 
            // OR 
            // Default the default for an object (null) if no match found.
            return _context.Regions
                .Where(x => x.RegionID == id)
                .FirstOrDefault();
        }
        #endregion
    }
}
