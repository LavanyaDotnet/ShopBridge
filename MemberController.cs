using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThinkBridgeUpdated.Models;

namespace ThinkBridgeUpdated.Controllers
{
    public class MemberController : ApiController
    {
        // GET api/<controller>
        //        public IEnumerable<string> Get()
        //        {
        //            return new string[] { "value1", "value2" };
        //        }

        //        // GET api/<controller>/5
        //        public string Get(int id)
        //        {
        //            return "value";
        //        }

        //        // POST api/<controller>
        //        public void Post([FromBody] string value)
        //        {
        //        }

        //        // PUT api/<controller>/5
        //        public void Put(int id, [FromBody] string value)
        //        {
        //        }

        //        // DELETE api/<controller>/5
        //        public void Delete(int id)
        //        {
        //        }
        //    }
        //}

        //Create instance of Linq-To-Sql class as db  
        MemberDataClassesDataContext db = new MemberDataClassesDataContext();



        //This action method return all members records.  
        // GET api/<controller>  
        public IEnumerable<tblProduct> Get()
        {
            //returning all records of table tblMember.  
            return db.tblProducts.ToList().AsEnumerable();
        }



        //This action method will fetch and filter for specific member id record  
        // GET api/<controller>/5  
        public HttpResponseMessage Get(int id)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.tblProducts where a.ProductId == id select a).FirstOrDefault();


            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {
                //sending response as status code OK with memberdetail entity.  
                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                //sending response as error status code NOT FOUND with meaningful message.  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }
        }


        //To add a new member record  
        // POST api/<controller>  
        public HttpResponseMessage Post([FromBody] tblProduct _member)
        {
            try
            {
                //To add an new member record  
                db.tblProducts.InsertOnSubmit(_member);

                //Save the submitted record  
                db.SubmitChanges();

                //return response status as successfully created with member entity  
                var msg = Request.CreateResponse(HttpStatusCode.Created, _member);

                //Response message with requesturi for check purpose  
                msg.Headers.Location = new Uri(Request.RequestUri + _member.ProductId.ToString());

                return msg;
            }
            catch (Exception ex)
            {

                //return response as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        //To update member record  
        // PUT api/<controller>/5  
        public HttpResponseMessage Put(int id, [FromBody] tblProduct _member)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.tblProducts where a.ProductId == id select a).FirstOrDefault();

            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {
                //set received _member object properties with memberdetail  
                memberdetail.ProductName = _member.ProductName;
                memberdetail.Brand = _member.Brand;
                //save set allocation.  
                db.SubmitChanges();

                //return response status as successfully updated with member entity  
                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                //return response error as NOT FOUND  with message.  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }


        }

        // DELETE api/<controller>/5  
        public HttpResponseMessage Delete(int id)
        {

            try
            {
                //fetching and filter specific member id record   
                var _DeleteMember = (from a in db.tblProducts where a.ProductId == id select a).FirstOrDefault();

                //checking fetched or not with the help of NULL or NOT.  
                if (_DeleteMember != null)
                {

                    db.tblProducts.DeleteOnSubmit(_DeleteMember);
                    db.SubmitChanges();

                    //return response status as successfully deleted with member id  
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                {
                    //return response error as Not Found  with exception message.  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member Not Found or Invalid " + id.ToString());
                }
            }

            catch (Exception ex)
            {

                //return response error as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}