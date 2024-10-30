using Exam.DTOs;
using Exam.Models;
using Exam.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController(IUnitOfWork  _unitOfWork) : ControllerBase
    {
        [HttpPost("CreateExam")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exam = new Exam.Models.Exam();
            exam.Id = 0;
            exam.DurationInMinutes = dto.DurationInMinutes; 
            exam.Subject = dto.Subject;
            exam.userId = dto.UserId;
            exam.Type = dto.Type;
            exam.Questions = dto.Q_AModel.Select(q => new Question
            {
                Body = q.Question,
                Answer = new QuestionAnswer
                {
                    Answer = q.Answer
                }
            }).ToList();
            await _unitOfWork._examService.Add(exam);
             _unitOfWork.Complete(); 
            return Ok(exam);
        }
        [HttpGet("TakeTheExam")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> TakeExam(TakeExamDto dto)
        {
            var exam = _unitOfWork._examService.GetRandomExam(dto.Subject);
            
            if(exam == null)
            {
                return BadRequest($"There is No Exams in {dto.Subject} Subject");
            }
            var done = new ExamTaked();
            done.examId = exam.Id;
            done.UserName = dto.UserName;
            await _unitOfWork._examTakedService.Add(done);
            _unitOfWork.Complete();
           var ListOfQuestions = exam.Questions; 

            List<QuestionsForExam>lst = new List<QuestionsForExam>();  

            foreach(var item in ListOfQuestions)
            {
                QuestionsForExam Q = new QuestionsForExam(); 
                Q.Id = item.Id;
                Q.Body = item.Body;
                lst.Add(Q);
            }
            var Exam = new ExamTakedDto();
            Exam.ExamId = exam.Id;
            Exam.ExamQuestions = lst;

            return Ok(Exam);
        }
        [HttpPost("SubmitExam")]
        [Authorize(Roles = "Student")]

        public async Task<IActionResult> SubmitExam(ExamSubmitDto dto)
        {
            var examId = dto.ExamId;
            var username = dto.UserName;
            var Q_A = dto.submitedAnswers; 

            foreach(var item in Q_A)
            {
                var SubmitAnswer = new SubmitedAnswers();
                SubmitAnswer.Id = 0; 
                SubmitAnswer.UserName = username;
                SubmitAnswer.ExamId = examId;
                var Q = _unitOfWork._questionService.GetById(item.QuestionId); 
                SubmitAnswer.QuestionId = item.QuestionId;
                SubmitAnswer.question = Q;
                SubmitAnswer.Answer = item.Answer;
                await _unitOfWork._submittedAnswersService.Add(SubmitAnswer);
                _unitOfWork.Complete();
            }


            return Ok("Your Answers Is Submitted , You will now the result later");
        }

        [HttpPut("GradeTheExam")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GradeExam()
        {
            var WhoNeedGrade = await _unitOfWork._examTakedService.GetExamListIfItisnotGradedYet();

            foreach(var item in WhoNeedGrade)
            {
                var lst = _unitOfWork._submittedAnswersService.GetByUsernameAndExamId(item.UserName, item.examId);
                int cnt = 0; 
                foreach(var i in lst)
                {
                    if (i.Answer == i.question.Answer.Answer) cnt++;
                }
                item.grade = cnt;
                _unitOfWork._examTakedService.Update(item);
                _unitOfWork.Complete();
            }

            return Ok();
        }

        [HttpGet("ShowResults")]
        [Authorize("Student")]

        public async Task<IActionResult> ShowResults(string username , int examId)
        {
            var grade = _unitOfWork._examTakedService.GetExam(username , examId);

            return Ok($"Your Grade Is {grade.grade}");
        }


    }
}
