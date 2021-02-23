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
    //Class to connect to the server
    public class RestService
    {
        HttpClient Client { get; set; }
        List<Job> Jobs { get; set; }

        public RestService()
        {
            Client = new HttpClient();
            Jobs = new List<Job>();
        }

        public async Task<List<Job>> GetJobPostings()
        {
            try
            {
                //Getting every job position from Endpoint /jobs
                string content = null;
                string addToUrl = "jobs";
                List<JobPosting> jobItems = null;
                Uri uri = new Uri(Constants.Url + addToUrl);

                HttpResponseMessage response = await Client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    jobItems = JsonConvert.DeserializeObject<List<JobPosting>>(content);
                }

                //Getting every company from Endpoint /companies
                addToUrl = "companies";
                List<Company> companyItems = null;
                uri = new Uri(Constants.Url + addToUrl);
                response = await Client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    companyItems = JsonConvert.DeserializeObject<List<Company>>(content);
                }

                //Getting every position from Endpoint /positions
                addToUrl = "positions";
                List<Position> positionItems = null;
                uri = new Uri(Constants.Url + addToUrl);
                response = await Client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    positionItems = JsonConvert.DeserializeObject<List<Position>>(content);
                }
                
                //Combining the company, job and position data in the object of type Job
                if (jobItems != null && companyItems != null && positionItems != null)
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Jobs;
        }
    }
}
