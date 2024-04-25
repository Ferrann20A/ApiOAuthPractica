using ApiOAuthPractica.Models;
using ApiOAuthPractica.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiOAuthPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> Get()
        {
            return await this.repo.GetDoctoresAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> FindDoctor(int id)
        {
            return await this.repo.FindDoctorAsync(id);
        }
    }
}
