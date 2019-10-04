using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aws_api_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3Controller : ControllerBase
    {
        private IAmazonS3 _S3Client { get; set; }

        public S3Controller(IAmazonS3 s3Client)
        {
            _S3Client = s3Client;
        }

        // GET: api/S3
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            try
            {
                var request = new PutObjectRequest()
                {
                    ContentBody = "this is a test",
                    BucketName = "vp-s3-test",
                    Key = "file1.txt"
                };

                var response = await _S3Client.PutObjectAsync(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }

            return Ok();
        }

        // GET: api/S3/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/S3
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/S3/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
