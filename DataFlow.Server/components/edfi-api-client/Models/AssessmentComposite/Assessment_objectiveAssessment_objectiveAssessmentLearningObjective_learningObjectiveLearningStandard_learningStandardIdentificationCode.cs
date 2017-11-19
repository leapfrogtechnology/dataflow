using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace EdFi.OdsApi.Models.AssessmentComposite 
{
    public class Assessment_objectiveAssessment_objectiveAssessmentLearningObjective_learningObjectiveLearningStandard_learningStandardIdentificationCode 
    {
        /// <summary>
        /// A unique number or alphanumeric code assigned to a space, room, site, building, individual, organization, program, or institution by a school, school system, a state, or other agency or entity.
        /// </summary>
        public string identificationCode { get; set; }

        /// <summary>
        /// The name of the content standard, for example Common Core.
        /// </summary>
        public string contentStandardName { get; set; }

        }
}
