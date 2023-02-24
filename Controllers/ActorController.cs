using InventoryApp.DTO;
using InventoryApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InventoryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ActorController : ControllerBase
    {
        private readonly DBContext DBContext;

        public ActorController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

                [HttpGet("GetActors")]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var List = await DBContext.Actors.Select(
                s => new ActorDTO
                {
                    IdActor = s.IdActor,
                    Name = s.Name,
                    Address = s.Address,
                    Phono = s.Phono,
                    DocumentId = s.DocumentId,
                    Email = s.Email,
                    IdTypeActor = s.IdTypeActor
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetActorById")]
        public async Task<ActionResult<ActorDTO>> GetActorById(int Id)
        {
            ActorDTO Actor = await DBContext.Actors.Select(
                    s => new ActorDTO
                    {
                        IdActor = s.IdActor,
                        Name = s.Name,
                        Address = s.Address,
                        Phono = s.Phono,
                        DocumentId = s.DocumentId,
                        Email = s.Email,
                        IdTypeActor = s.IdTypeActor
                    })
                .FirstOrDefaultAsync(s => s.IdActor == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return Actor;
            }
        }

        [HttpPost("InsertActor")]
        public async Task<HttpStatusCode> InsertActor(ActorDTO Actor)
        {
            var entity = new Actor()
            {
                IdActor = Actor.IdActor,
                Name = Actor.Name,
                Address = Actor.Address,
                Phono = Actor.Phono,
                DocumentId = Actor.DocumentId,
                Email = Actor.Email,
                IdTypeActor = Actor.IdTypeActor
            };

            DBContext.Actors.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateActor")]
        public async Task<HttpStatusCode> UpdateActor(ActorDTO Actor)
        {
            var entity = await DBContext.Actors.FirstOrDefaultAsync(s => s.IdActor == Actor.IdActor);

            entity.Name = Actor.Name;
            entity.Address = Actor.Address;
            entity.Phono = Actor.Phono;
            entity.DocumentId = Actor.DocumentId;
            entity.Email = Actor.Email;
            entity.IdTypeActor = Actor.IdTypeActor;

            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteActor/{Id}")]
        public async Task<HttpStatusCode> DeleteActor(int Id)
        {
            var entity = new Actor()
            {
                IdActor = Id
            };
            DBContext.Actors.Attach(entity);
            DBContext.Actors.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}