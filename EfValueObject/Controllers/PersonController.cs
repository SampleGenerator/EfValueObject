using EfValueObject.Data;
using EfValueObject.Dtos;
using EfValueObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfValueObject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PersonController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            [FromQuery] string? query,
            CancellationToken ct
        )
        {
            var trimmedQuery = query?.Trim() ?? string.Empty;
            var persons = await _dbContext
                .Persons
                .Where(p =>
                    p.FirstName.Contains(trimmedQuery) ||
                    p.LastName.Contains(trimmedQuery) ||
                    p.Address.Country.Contains(trimmedQuery) ||
                    p.Address.City.Contains(trimmedQuery) ||
                    p.Address.PostalCode.ToString().Contains(trimmedQuery) ||
                    p.PhoneNumbers.Any(ph => ph.Value.Contains(trimmedQuery)) ||
                    ((string)(object)p.GiftCards).Contains(trimmedQuery)
                )
                .ToListAsync(ct);

            var personDtos = MapToPersonDtos(persons);

            return Ok(personDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command, CancellationToken ct)
        {
            var person = MapToPerson(command);

            var entry = await _dbContext.AddAsync(person, ct);
            _ = await _dbContext.SaveChangesAsync(ct);

            return Ok(entry.Entity);
        }

        private static IEnumerable<PersonDto> MapToPersonDtos(List<Person> persons)
        {
            return persons.Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                PhoneNumbers = p.PhoneNumbers.Select(ph => ph.Value).ToList(),
                GiftCards = p.GiftCards,
            });
        }

        private static Person MapToPerson(CreatePersonCommand command)
        {
            return new Person(
                command.FirstName,
                command.LastName,
                command.Address,
                command.PhoneNumbers.Select(p => new PhoneNumber(p)),
                command.GiftCards
            );
        }
    }
}