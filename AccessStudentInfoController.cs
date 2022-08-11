using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace GetStudentListAPI.Controllers
{
    public class AccessStudentInfoController : ApiController
    {
        // GET api/values
        public string Get()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache["students"] == null)
            {
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1000);

                Student student = new Student();

                CacheItem cacheItem = new CacheItem("students", student.GetStudents());
                cache.Add(cacheItem, cacheItemPolicy);
            }
            var data = cache.Get("students") as string;
            return data;
        }
    }
}