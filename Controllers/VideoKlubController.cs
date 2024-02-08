using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoKlubAPI.Models;

namespace VideoKlubAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoKlubController : ControllerBase
    {
         public VideoKlubContext Context { get; set; }
        public VideoKlubController(VideoKlubContext context)
        {
            Context = context;
        }

        [Route("UpisiVideoKlub")]
        [HttpPost]
        public async Task UpisiVideoKlub([FromBody] VideoKlub klub)
        {
            Context.VideoKlubovi.Add(klub);
            await Context.SaveChangesAsync();
        }
       
       [Route("PreuzmiVideoKlubove")]
        [HttpGet]
        public async Task<List<VideoKlub>> PreuzmiVideoKlubove()
        {
            return await Context.VideoKlubovi.Include(p => p.Odeljenja).ToListAsync();
        }


        [Route("IzbrisiVideoKlub/{id}")]
        [HttpDelete]
        public async Task IzbrisiVideoKlub(int id)
        {
            var klub = await Context.VideoKlubovi.FindAsync(id);
            Context.Remove(klub);
            await Context.SaveChangesAsync();
        }



        [Route("UpisiOdeljenje/{idVideoKluba}")]
        [HttpPost]
        public async Task<IActionResult> UpisiOdeljenje(int idVideoKluba, [FromBody] Odeljenje odeljenje)
        {
            var vklub = await Context.VideoKlubovi.FindAsync(idVideoKluba);
            odeljenje.Klub = vklub;                   
            var odlj = Context.Odeljenja.Where(o=>o.Naziv==odeljenje.Naziv).FirstOrDefault();
            if(odlj!=null)
            {
                return StatusCode(406);
            }
            else if(odeljenje.BrojRedova<1||odeljenje.BrojPolicaPoRedu<1)
            {
                return StatusCode(407);
            }
            else
            {
                Context.Odeljenja.Add(odeljenje);  //dodali smo lokalno, ali nam odeljenje ne postoji u bazi
                await Context.SaveChangesAsync();  // na ovaj nacin novo odeljenje upisujemo i u bazu
                return Ok();
            }
                     
        }

        [Route("PreuzmiOdeljenja/{idVideoKluba}")]
        [HttpGet]
        public async Task<List<Odeljenje>> PreuzmiOdeljenja(int idVideoKluba)
        {
            return await Context.Odeljenja.Where(odeljenje=>odeljenje.Klub.ID==idVideoKluba).Include(odlj=>odlj.Filmovi).ToListAsync();
        }

        [Route("UpisiFilm/{idOdeljenja}")]
        [HttpPost]
        public async Task<IActionResult> UpisiFilm(int idOdeljenja, [FromBody] Film film)
        {
            var odlj = await Context.Odeljenja.FindAsync(idOdeljenja);
            film.Odeljenje = odlj;          
            if (Context.Filmovi.Any(p => p.NazivFilma == film.NazivFilma && (p.Red != film.Red || p.PozURedu != film.PozURedu)))
            {
                var xy = Context.Filmovi.Where(p => p.NazivFilma == film.NazivFilma).FirstOrDefault();
                return BadRequest(new { X = xy?.Red, Y = xy?.PozURedu });//film postoji na drugoj lokaciji
            }
            var pozicija = Context.Filmovi.Where(p => p.Odeljenje.ID==idOdeljenja && p.Red == film.Red && p.PozURedu == film.PozURedu).FirstOrDefault();

            if (pozicija != null)
            {
                if (pozicija.NazivFilma != film.NazivFilma)
                {
                    return StatusCode(406);//na ovoj lokaciji je drugi film, probajte na nekoj drugoj
                }
                else
                {
                    return StatusCode(0);//ovde je film, mozete da azurirate kolicinu
                }
            }
            
            if(film.Kolicina<1)
                return StatusCode(410);

            Context.Filmovi.Add(film);
            await Context.SaveChangesAsync();
            return Ok();                     
        } 
        [Route("IzmeniFilm/{idOdeljenja}")]
        [HttpPut]
        public async Task<IActionResult> IzmeniFilm(int idOdeljenja,[FromBody] Film film)
        {
            if(film.Kolicina<1)
                return StatusCode(406);
            var k = Context.Filmovi.Where(p=>p.Odeljenje.ID==idOdeljenja && p.Red==film.Red && p.PozURedu==film.PozURedu).FirstOrDefault();
            if(k!=null)
            {
                k.Kolicina=film.Kolicina;
                Context.Update<Film>(k);
                await Context.SaveChangesAsync();
                return Ok();
            }
            else
                return StatusCode(404);
            
        }       
        [Route("ObrisiFilm/{idOdeljenja}")]
        [HttpDelete]
        public async Task<IActionResult> ObrisiFilm(int idOdeljenja,[FromBody]Film film)
        {
             var k = Context.Filmovi.Where(p=>p.Odeljenje.ID==idOdeljenja && p.Red==film.Red && p.PozURedu==film.PozURedu).FirstOrDefault();
            if(k!=null)
            {
                Context.Filmovi.Remove(k);    
                await Context.SaveChangesAsync();
                return Ok();
            }
            else
                return StatusCode(404);
        }

        [Route("PreuzmiFilmove")]
        [HttpGet]
        public async Task<List<Film>> PreuzmiFilmove()
        {
            return await Context.Filmovi.ToListAsync();
            
        }    



        

    }
}
