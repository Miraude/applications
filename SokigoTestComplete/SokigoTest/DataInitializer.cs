using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using SokigoTest.Models;

namespace SokigoTest
{
    public class DataInitializer
    {
           private readonly AddressAppContext _dbContext;

        public DataInitializer(AddressAppContext dbContext)
        {
            _dbContext = dbContext; 
        }
    }
}
