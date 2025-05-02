using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private const string _data = @"{
              'user': {
                'id': 12345,
                'profile': {
                  'name': 'Alice',
                  'contact': {
                    'email': 'alice@example.com',
                    'phone': {
                      'home': '123-456-7890',
                      'mobile': '987-654-3210'
                    },
                    'addresses': [
                      {
                        'type': 'home',
                        'location': {
                          'street': '123 Main St',
                          'city': 'Springfield',
                          'country': {
                            'name': 'USA',
                            'code': 'US'
                          }
                        }
                      },
                      {
                        'type': 'work',
                        'location': {
                          'street': '456 Work Blvd',
                          'city': 'Metropolis',
                          'country': {
                            'name': 'USA',
                            'code': 'US'
                          }
                        }
                      }
                    ]
                  }
                }
              }
            }";

        [HttpGet("UserCountryName")]
        public IActionResult GetName()
        {
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(_data);

            return Ok(root.User.Profile.Contact.Addresses.First().Location.Country.Name);
        }

        public class Address
        {
            public Location Location { get; set; }
            public string Type { get; set; }
        }

        public class Country
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class Location
        {
            public string City { get; set; }
            public Country Country { get; set; }
            public string Street { get; set; }
        }

        public class Profile
        {
            public Contact Contact { get; set; }
            public string Name { get; set; }
        }

        public class Contact
        {
            public Address[] Addresses { get; set; }            
        }

        public class Root
        {
            public User User { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public Profile Profile { get; set; }
        }
    }
}
