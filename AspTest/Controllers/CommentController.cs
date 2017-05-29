using System.Linq;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;
using System.Collections.Generic;
using AutoMapper;
using DomainModel;
using System.Diagnostics;
namespace WebService.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentDataService _dataService;
        
        public CommentController(ICommentDataService dataService)
        {
            Debug.WriteLine("1");
            _dataService = dataService;
            //Mapper.CreateMap<DomainModel.Post, Models.PostModel>();
            Debug.WriteLine("ici");
            Mapper.Initialize( cfg => {
                //cfg.CreateMap<Source, Dest>();
                cfg.CreateMap<Comment, CommentListModel>();
                cfg.CreateMap<Comment, CommentModel>();
            });
        }

        const int maxPageSize = 20;

        [HttpGet(Name = nameof(GetComments))]
        public IActionResult GetComments(int pageNumber = 1, int pageSize = 5)
        {
            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize;

            var data = _dataService.GetComments(pageNumber, pageSize);

            var result = Mapper.Map<IEnumerable<CommentListModel>>(data);

            foreach ( CommentListModel p in result )
            {
                p.Url = Url.Link(nameof(GetComment), new{ p.Id });
            }
            
            var prevlink = pageNumber > 1
                ? Url.Link(nameof(GetComments), new { pageNumber = pageNumber - 1, pageSize })
                : null;

            var total = _dataService.GetNumberOfComment();

            var totalPages = (int)System.Math.Ceiling(total / (double)pageSize);

            var nextlink = pageNumber < totalPages
                ? Url.Link(nameof(GetComments), new { pageNumber = pageNumber + 1, pageSize })
                : null;

            var curlink = Url.Link(nameof(GetComments), new { pageNumber, pageSize });

            var linkedResult = new
            {
                Result = result,
                Links = new List<object>
                {
                    new { name = "prev", url = prevlink },
                    new { name = "next", url = nextlink },
                    new { name = "cur", url = curlink }
                }
            };

            return Ok(linkedResult);
        }

        [HttpGet("{id}", Name = nameof(GetComment))]
        public IActionResult GetComment(int id)
        {
            var comment = _dataService.GetComment(id);

            if (comment == null) return NotFound();

            var model = Mapper.Map<CommentModel>(comment);


            return Ok(model);
        }
        
        /* 
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostCreateOrUpdateModel model)
        {

            if (model == null) return BadRequest();

            var post = Mapper.Map<Post>(model);

            _dataService.CreatePost(post);

            return CreatedAtRoute(nameof(GetPost), new { id = post.Id }, Mapper.Map<PostModel>(post));
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] PostCreateOrUpdateModel model)
        {
            if (model == null) return BadRequest();

            var post = _dataService.GetPost(id);

            if (post == null) return NotFound();

            Mapper.Map(model, post);

            _dataService.UpdatePost(post);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _dataService.GetPost(id);

            if (post == null) return NotFound();

            _dataService.DeletePost(post);

            return NoContent();
        }

        */



    }
}
