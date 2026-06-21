using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Persistence.Contexts;

namespace Restaurant.Persistence.Repositories.Identity;

public class PersonalInformationRepository : Repository<PersonalInformation>, IPersonalInformationRepository
{
    public PersonalInformationRepository(RestaurantDbContext context) : base(context)
    {
    }
}
