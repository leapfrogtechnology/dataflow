using System;
using System.Collections.Generic;
using System.Linq;
using DataFlow.EdFi.Models.Resources;
using RestSharp;

namespace DataFlow.EdFi.Api.Resources 
{
    public class CourseTranscriptsApi 
    {
        private readonly IRestClient client;

        public CourseTranscriptsApi(IRestClient client)
        {
            this.client = client;
        }
      
        /// <summary>
        /// Retrieves resources based with paging capabilities (using the &quot;Get All&quot; pattern). This GET operation provides access to resources using the &quot;Get All&quot; pattern. In this version of the API there is support for paging.
        /// </summary>
        /// <param name="offset">Indicates how many items should be skipped before returning results.</param>
        /// <param name="limit">Indicates the maximum number of items that should be returned in the results (defaults to 25).</param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse<List<CourseTranscript>> GetCourseTranscriptsAll(int? offset= null, int? limit= null) 
        {
            var request = new RestRequest("/courseTranscripts", Method.GET);
            request.RequestFormat = DataFormat.Json;

            if (offset != null)
                request.AddParameter("offset", offset);
            if (limit != null)
                request.AddParameter("limit", limit);
            var response = client.Execute<List<CourseTranscript>>(request);

            return response;
        }
        /// <summary>
        /// Retrieves resources matching values of an example resource (using the &quot;Get By Example&quot; pattern). This GET operation provides access to resources using the &quot;Get by Example&quot; search pattern.  The values of any properties of the resource that are specified will be used to return all matching results (if it exists).
        /// </summary>
        /// <param name="offset">Indicates how many items should be skipped before returning results.</param>
        /// <param name="limit">Indicates the maximum number of items that should be returned in the results (defaults to 25).</param>
        /// <param name="courseAttemptResultType">The result from the student''s attempt to take the course, for example:  Pass  Fail  Incomplete  Withdrawn  Expelled</param>
        /// <param name="studentUniqueId">A unique alpha-numeric code assigned to a student.</param>
        /// <param name="educationOrganizationId">EducationOrganization Identity Column</param>
        /// <param name="courseEducationOrganizationId">EducationOrganization Identity Column  </param>
        /// <param name="schoolYear">The identifier for the school year.  </param>
        /// <param name="termDescriptor">A unique identifier used as Primary Key, not derived from business logic, when acting as Foreign Key, references the parent table.</param>
        /// <param name="courseCode">TThe actual code that identifies the organization of subject matter and related learning experiences provided for the instruction of students.</param>
        /// <param name="attemptedCreditType">Key for Credit</param>
        /// <param name="attemptedCreditConversion">Conversion factor that when multiplied by the number of credits is equivalent to Carnegie units.</param>
        /// <param name="attemptedCredits">The number of credits attempted for a course.</param>
        /// <param name="earnedCreditType">Key for Credit</param>
        /// <param name="earnedCreditConversion">Conversion factor that when multiplied by the number of credits is equivalent to Carnegie units.</param>
        /// <param name="earnedCredits">The number of credits awarded or earned for the course.</param>
        /// <param name="whenTakenGradeLevelDescriptor">A unique identifier used as Primary Key, not derived from business logic, when acting as Foreign Key, references the parent table.  </param>
        /// <param name="methodCreditEarnedType">The method the credits were earned, for example:  Classroom, Examination, Transfer</param>
        /// <param name="finalLetterGradeEarned">The final indicator of student performance in a class as submitted by the instructor.</param>
        /// <param name="finalNumericGradeEarned">The final indicator of student performance in a class as submitted by the instructor.</param>
        /// <param name="courseRepeatCodeType">Indicates that an academic course has been repeated by a student and how that repeat is to be computed in the student''s academic grade average.</param>
        /// <param name="schoolId">School Identity Column</param>
        /// <param name="courseTitle">The descriptive name given to a course of study offered in a school or other institution or organization. In departmentalized classes at the elementary, secondary, and postsecondary levels (and for staff development activities), this refers to the name by which a course is identified (e.g., American History, English III). For elementary and other non-departmentalized classes, it refers to any portion of the instruction for which a grade or report is assigned (e.g., reading, composition, spelling, and language arts).</param>
        /// <param name="alternativeCourseTitle">The descriptive name given to a course of study offered in the school, if different from the CourseTitle.</param>
        /// <param name="alternativeCourseCode">The local code assigned by the school that identifies the course offering, the code from an external educational organization, or other alternate course code.</param>
        /// <param name="id"></param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse<List<CourseTranscript>> GetCourseTranscriptsAll(int? offset= null, int? limit= null, string courseAttemptResultType= null, string studentUniqueId= null, int? educationOrganizationId= null, int? courseEducationOrganizationId= null, int? schoolYear= null, string termDescriptor= null, string courseCode= null, string attemptedCreditType= null, double? attemptedCreditConversion= null, double? attemptedCredits= null, string earnedCreditType= null, double? earnedCreditConversion= null, double? earnedCredits= null, string whenTakenGradeLevelDescriptor= null, string methodCreditEarnedType= null, string finalLetterGradeEarned= null, double? finalNumericGradeEarned= null, string courseRepeatCodeType= null, int? schoolId= null, string courseTitle= null, string alternativeCourseTitle= null, string alternativeCourseCode= null, string id= null) 
        {
            var request = new RestRequest("/courseTranscripts", Method.GET);
            request.RequestFormat = DataFormat.Json;

            if (offset != null)
                request.AddParameter("offset", offset);
            if (limit != null)
                request.AddParameter("limit", limit);
            if (courseAttemptResultType != null)
                request.AddParameter("courseAttemptResultType", courseAttemptResultType);
            if (studentUniqueId != null)
                request.AddParameter("studentUniqueId", studentUniqueId);
            if (educationOrganizationId != null)
                request.AddParameter("educationOrganizationId", educationOrganizationId);
            if (courseEducationOrganizationId != null)
                request.AddParameter("courseEducationOrganizationId", courseEducationOrganizationId);
            if (schoolYear != null)
                request.AddParameter("schoolYear", schoolYear);
            if (termDescriptor != null)
                request.AddParameter("termDescriptor", termDescriptor);
            if (courseCode != null)
                request.AddParameter("courseCode", courseCode);
            if (attemptedCreditType != null)
                request.AddParameter("attemptedCreditType", attemptedCreditType);
            if (attemptedCreditConversion != null)
                request.AddParameter("attemptedCreditConversion", attemptedCreditConversion);
            if (attemptedCredits != null)
                request.AddParameter("attemptedCredits", attemptedCredits);
            if (earnedCreditType != null)
                request.AddParameter("earnedCreditType", earnedCreditType);
            if (earnedCreditConversion != null)
                request.AddParameter("earnedCreditConversion", earnedCreditConversion);
            if (earnedCredits != null)
                request.AddParameter("earnedCredits", earnedCredits);
            if (whenTakenGradeLevelDescriptor != null)
                request.AddParameter("whenTakenGradeLevelDescriptor", whenTakenGradeLevelDescriptor);
            if (methodCreditEarnedType != null)
                request.AddParameter("methodCreditEarnedType", methodCreditEarnedType);
            if (finalLetterGradeEarned != null)
                request.AddParameter("finalLetterGradeEarned", finalLetterGradeEarned);
            if (finalNumericGradeEarned != null)
                request.AddParameter("finalNumericGradeEarned", finalNumericGradeEarned);
            if (courseRepeatCodeType != null)
                request.AddParameter("courseRepeatCodeType", courseRepeatCodeType);
            if (schoolId != null)
                request.AddParameter("schoolId", schoolId);
            if (courseTitle != null)
                request.AddParameter("courseTitle", courseTitle);
            if (alternativeCourseTitle != null)
                request.AddParameter("alternativeCourseTitle", alternativeCourseTitle);
            if (alternativeCourseCode != null)
                request.AddParameter("alternativeCourseCode", alternativeCourseCode);
            if (id != null)
                request.AddParameter("id", id);
            var response = client.Execute<List<CourseTranscript>>(request);

            return response;
        }
        /// <summary>
        /// Retrieves a specific resource using the values of the resource's natural key (using the &quot;Get By Key&quot; pattern). This GET operation provides access to resources using the &quot;Get by Key&quot; search pattern. The values of the natural key of the resource must be fully specified, and the service will return the matching result (if it exists).
        /// </summary>
        /// <param name="courseAttemptResultType">The result from the student''s attempt to take the course, for example:  Pass  Fail  Incomplete  Withdrawn  Expelled</param>
        /// <param name="studentUniqueId">A unique alpha-numeric code assigned to a student.</param>
        /// <param name="educationOrganizationId">EducationOrganization Identity Column</param>
        /// <param name="courseEducationOrganizationId">EducationOrganization Identity Column  </param>
        /// <param name="schoolYear">The identifier for the school year.  </param>
        /// <param name="termDescriptor">A unique identifier used as Primary Key, not derived from business logic, when acting as Foreign Key, references the parent table.</param>
        /// <param name="courseCode">TThe actual code that identifies the organization of subject matter and related learning experiences provided for the instruction of students.</param>
        /// <param name="IfNoneMatch">The previously returned ETag header value, used here to prevent the unnecessary data transfer of an unchanged resource.</param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse<CourseTranscript> GetCourseTranscriptByKey(string courseAttemptResultType, string studentUniqueId, int? educationOrganizationId, int? courseEducationOrganizationId, int? schoolYear, string termDescriptor, string courseCode, string IfNoneMatch= null) 
        {
            var request = new RestRequest("/courseTranscripts", Method.GET);
            request.RequestFormat = DataFormat.Json;

            // verify required params are set
            if (courseAttemptResultType == null || studentUniqueId == null || educationOrganizationId == null || courseEducationOrganizationId == null || schoolYear == null || termDescriptor == null || courseCode == null ) 
               throw new ArgumentException("API method call is missing required parameters");
            if (courseAttemptResultType != null)
                request.AddParameter("courseAttemptResultType", courseAttemptResultType);
            if (studentUniqueId != null)
                request.AddParameter("studentUniqueId", studentUniqueId);
            if (educationOrganizationId != null)
                request.AddParameter("educationOrganizationId", educationOrganizationId);
            if (courseEducationOrganizationId != null)
                request.AddParameter("courseEducationOrganizationId", courseEducationOrganizationId);
            if (schoolYear != null)
                request.AddParameter("schoolYear", schoolYear);
            if (termDescriptor != null)
                request.AddParameter("termDescriptor", termDescriptor);
            if (courseCode != null)
                request.AddParameter("courseCode", courseCode);
            request.AddHeader("If-None-Match", IfNoneMatch);
            var response = client.Execute<CourseTranscript>(request);

            return response;
        }
        /// <summary>
        /// Creates or updates resources based on the natural key values of the supplied resource. The POST operation can be used to create or update resources. In database terms, this is often referred to as an &quot;upsert&quot; operation (insert + update).  Clients should NOT include the resource &quot;id&quot; in the JSON body because it will result in an error (you must use a PUT operation to update a resource by &quot;id&quot;). The web service will identify whether the resource already exists based on the natural key values provided, and update or create the resource appropriately.
        /// </summary>
        /// <param name="body">The JSON representation of the &quot;courseTranscript&quot; resource to be created or updated.</param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse PostCourseTranscripts(CourseTranscript body) 
        {
            var request = new RestRequest("/courseTranscripts", Method.POST);
            request.RequestFormat = DataFormat.Json;

            // verify required params are set
            if (body == null ) 
               throw new ArgumentException("API method call is missing required parameters");
            request.AddBody(body);
            var response = client.Execute(request);

            var location = response.Headers.FirstOrDefault(x => x.Name == "Location");

            if (location != null && !string.IsNullOrWhiteSpace(location.Value.ToString()))
                body.id = location.Value.ToString().Split('/').Last();
            return response;
        }
        /// <summary>
        /// Retrieves a specific resource using the resource's identifier (using the &quot;Get By Id&quot; pattern). This GET operation retrieves a resource by the specified resource identifier.
        /// </summary>
        /// <param name="id">A resource identifier specifying the resource to be retrieved.</param>
        /// <param name="IfNoneMatch">The previously returned ETag header value, used here to prevent the unnecessary data transfer of an unchanged resource.</param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse<CourseTranscript> GetCourseTranscriptsById(string id, string IfNoneMatch= null) 
        {
            var request = new RestRequest("/courseTranscripts/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;

            request.AddUrlSegment("id", id);
            // verify required params are set
            if (id == null ) 
               throw new ArgumentException("API method call is missing required parameters");
            request.AddHeader("If-None-Match", IfNoneMatch);
            var response = client.Execute<CourseTranscript>(request);

            return response;
        }
        /// <summary>
        /// Updates or creates a resource based on the resource identifier. The PUT operation is used to update or create a resource by identifier.  If the resource doesn't exist, the resource will be created using that identifier.  Additionally, natural key values cannot be changed using this operation, and will not be modified in the database.  If the resource &quot;id&quot; is provided in the JSON body, it will be ignored as well.
        /// </summary>
        /// <param name="id">A resource identifier specifying the resource to be updated.</param>
        /// <param name="IfMatch">The ETag header value used to prevent the PUT from updating a resource modified by another consumer.</param>
        /// <param name="body">The JSON representation of the &quot;courseTranscript&quot; resource to be updated.</param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse PutCourseTranscript(string id, string IfMatch, CourseTranscript body) 
        {
            var request = new RestRequest("/courseTranscripts/{id}", Method.PUT);
            request.RequestFormat = DataFormat.Json;

            request.AddUrlSegment("id", id);
            // verify required params are set
            if (id == null || body == null ) 
               throw new ArgumentException("API method call is missing required parameters");
            request.AddHeader("If-Match", IfMatch);
            request.AddBody(body);
            var response = client.Execute(request);

            var location = response.Headers.FirstOrDefault(x => x.Name == "Location");

            if (location != null && !string.IsNullOrWhiteSpace(location.Value.ToString()))
                body.id = location.Value.ToString().Split('/').Last();
            return response;
        }
        /// <summary>
        /// Deletes an existing resource using the resource identifier. The DELETE operation is used to delete an existing resource by identifier.  If the resource doesn't exist, an error will result (the resource will not be found).
        /// </summary>
        /// <param name="id">A resource identifier specifying the resource to be deleted.</param>
        /// <param name="IfMatch">The ETag header value used to prevent the DELETE from removing a resource modified by another consumer.</param>
        /// <returns>A RestSharp <see cref="IRestResponse"/> instance containing the API response details.</returns>
        public IRestResponse DeleteCourseTranscriptById(string id, string IfMatch= null) 
        {
            var request = new RestRequest("/courseTranscripts/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;

            request.AddUrlSegment("id", id);
            // verify required params are set
            if (id == null ) 
               throw new ArgumentException("API method call is missing required parameters");
            request.AddHeader("If-Match", IfMatch);
            var response = client.Execute(request);

            return response;
        }
        }
    }

