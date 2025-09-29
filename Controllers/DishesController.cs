using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;

namespace RestaurantApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public DishesController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
    {
        return await _context.Dishes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dish>> GetDish(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);

        if (dish == null)
        {
            return NotFound();
        }

        return dish;
    }

    [HttpPost]
    public async Task<ActionResult<Dish>> CreateDish(Dish dish)
    {
        _context.Dishes.Add(dish);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDish), new { id = dish.Id }, dish);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDish(int id, Dish dish)
    {
        if (id != dish.Id)
        {
            return BadRequest();
        }

        _context.Entry(dish).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DishExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDish(int id)
    {
        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DishExists(int id)
    {
        return _context.Dishes.Any(e => e.Id == id);
    }
}
