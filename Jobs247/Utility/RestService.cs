using Jobs247.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jobs247.Utility
{
    public class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Job>> GetJobPostings()
        {
            string content = null;
            string addToUrl = "jobs";
            List<JobPosting> jobItems = null;
            Uri uri = new Uri(Constants.Url + addToUrl);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                jobItems = JsonConvert.DeserializeObject<List<JobPosting>>(content);
            }

            addToUrl = "companies";
            List<Company> companyItems = null;
            uri = new Uri(Constants.Url + addToUrl);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                companyItems = JsonConvert.DeserializeObject<List<Company>>(content);
            }

            addToUrl = "positions";
            List<Position> positionItems = null;
            uri = new Uri(Constants.Url + addToUrl);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                positionItems = JsonConvert.DeserializeObject<List<Position>>(content);
            }

            List<Job> Jobs = null;

            if(jobItems != null && companyItems != null && positionItems != null)
            {
                foreach (var item in jobItems)
                {
                    Jobs.Add(new Job
                    {
                        JobId = item.id,
                        Position = positionItems.Where(x => x.id == item.positionId).FirstOrDefault(),
                        Company = companyItems.Where(x => x.id == item.companyId).FirstOrDefault(),
                        Description = item.description
                    });
                }
            }
            

            return Jobs;
        }




    }
}
