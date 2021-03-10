using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsAPI.Models;

namespace CarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarInventoryContext _context;

        public CarsController(CarInventoryContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Search by car make
        [HttpGet("make/{make}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetMake(string make)
        {
            var cars = await _context.Cars.Where(x => x.Make.ToLower() == make).ToListAsync();
            
            if(cars.Count == 0)
            {
                return NotFound();
            }

            return cars;
        }

        //Search by car model
        [HttpGet("model/{model}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetModel(string model)
        {
            var cars = await _context.Cars.Where(x => x.Model.ToLower() == model).ToListAsync();

            if(cars.Count == 0)
            {
                return NotFound();
            }

            return cars;
        }

        //Search by car year
        [HttpGet("year/{year}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetYear(int year)
        {
            var cars = await _context.Cars.Where(x => x.Year == year).ToListAsync();

            if(cars.Count == 0)
            {
                return NotFound();
            }

            return cars;
        }

        //Search by car color
        [HttpGet("color/{color}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetColor(string color)
        {
            var cars = await _context.Cars.Where(x => x.Color.ToLower() == color).ToListAsync();

            if(cars.Count == 0)
            {
                return NotFound();
            }

            return cars;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
